import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRippleModule } from '@angular/material/core';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { TrainingService } from '../training.service';
import { TrainingDashboardStats } from '../training.models';

@Component({
  selector: 'app-training-dashboard',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatProgressBarModule,
    MatProgressSpinnerModule, MatRippleModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './training-dashboard.component.html',
  styleUrl: './training-dashboard.component.scss',
})
export class TrainingDashboardComponent implements OnInit {
  private trainingService = inject(TrainingService);
  private router = inject(Router);

  stats = signal<TrainingDashboardStats | null>(null);
  loading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void { this.loadDashboard(); }

  loadDashboard(): void {
    this.loading.set(true);
    this.trainingService.getDashboardStats().subscribe({
      next: (data) => { this.stats.set(data); this.loading.set(false); },
      error: () => { this.error.set('Failed to load dashboard'); this.loading.set(false); },
    });
  }

  navigateTo(path: string): void { this.router.navigate(['/training', path]); }

  getRatingStars(rating: number): number[] { return Array.from({ length: 5 }, (_, i) => i < Math.round(rating) ? 1 : 0); }
}
