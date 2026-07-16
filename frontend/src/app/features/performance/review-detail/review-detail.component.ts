import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSliderModule } from '@angular/material/slider';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PerformanceService } from '../performance.service';
import { Review, ReviewRating } from '../performance.models';

@Component({
  selector: 'app-review-detail',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSliderModule, MatDividerModule, MatProgressSpinnerModule, MatSnackBarModule,
  ],
  templateUrl: './review-detail.component.html',
  styleUrl: './review-detail.component.scss',
})
export class ReviewDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  review = signal<Review | null>(null);
  loading = signal(true);
  saving = signal(false);
  selfAssessment = '';
  managerAssessment = '';
  overallComments = '';
  ratings: ReviewRating[] = [];

  criteria = ['Communication', 'Teamwork', 'Problem Solving', 'Leadership', 'Technical Skills', 'Initiative'];

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadReview(id);
    this.ratings = this.criteria.map(c => ({ criterion: c, rating: 3, comment: '' }));
  }

  loadReview(id: string): void {
    this.loading.set(true);
    this.performanceService.getReview(id).subscribe({
      next: (review) => {
        this.review.set(review);
        if (review.ratings.length) this.ratings = review.ratings;
        if (review.selfAssessment) this.selfAssessment = review.selfAssessment;
        if (review.managerAssessment) this.managerAssessment = review.managerAssessment;
        if (review.overallComments) this.overallComments = review.overallComments;
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  saveDraft(): void { this.saveReview('draft'); }
  submit(): void { this.saveReview('completed'); }

  private saveReview(status: string): void {
    const r = this.review();
    if (!r) return;
    this.saving.set(true);
    this.performanceService.updateReview(r.id, {
      ratings: this.ratings,
      selfAssessment: this.selfAssessment,
      managerAssessment: this.managerAssessment,
      overallComments: this.overallComments,
      status: status as any,
    }).subscribe({
      next: () => {
        this.snackBar.open('Review saved', 'Close', { duration: 3000 });
        this.saving.set(false);
        if (status === 'completed') this.router.navigate(['/performance/reviews']);
      },
      error: () => { this.saving.set(false); this.snackBar.open('Failed to save', 'Close', { duration: 3000 }); },
    });
  }

  updateRating(index: number, value: number): void {
    this.ratings[index].rating = value;
  }

  getStarArray(rating: number): number[] { return Array.from({ length: 5 }, (_, i) => i + 1); }

  goBack(): void { this.router.navigate(['/performance/reviews']); }
}
