import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { Review, ReviewCycle } from '../performance.models';

@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatTabsModule, MatProgressSpinnerModule, DatePipe, TitleCasePipe],
  templateUrl: './reviews.component.html',
  styleUrl: './reviews.component.scss',
})
export class ReviewsComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private router = inject(Router);

  reviewCycles = signal<ReviewCycle[]>([]);
  myReviews = signal<Review[]>([]);
  pendingReviews = signal<Review[]>([]);
  loading = signal(true);

  ngOnInit(): void { this.loadData(); }

  loadData(): void {
    this.loading.set(true);
    this.performanceService.getReviewCycles().subscribe({
      next: (cycles) => { this.reviewCycles.set(cycles); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.performanceService.getReviews({ status: 'pending' }).subscribe({
      next: (reviews) => this.pendingReviews.set(reviews), error: () => {},
    });
    this.performanceService.getReviews().subscribe({
      next: (reviews) => this.myReviews.set(reviews), error: () => {},
    });
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = {
      draft: 'bg-gray-100 text-gray-800', pending: 'bg-amber-100 text-amber-800',
      in_progress: 'bg-blue-100 text-blue-800', completed: 'bg-green-100 text-green-800',
    };
    return c[status] || 'bg-gray-100 text-gray-800';
  }

  openReview(id: string): void {
    this.router.navigate(['/performance/reviews', id]);
  }

  startReview(): void {
    // Create new review from latest cycle
  }
}
