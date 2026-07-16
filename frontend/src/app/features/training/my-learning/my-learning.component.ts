import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatePipe } from '@angular/common';
import { TrainingService } from '../training.service';
import { Enrollment } from '../training.models';

@Component({
  selector: 'app-my-learning',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatTabsModule,
    MatProgressBarModule, MatProgressSpinnerModule, DatePipe,
  ],
  templateUrl: './my-learning.component.html',
  styleUrl: './my-learning.component.scss',
})
export class MyLearningComponent implements OnInit {
  private trainingService = inject(TrainingService);
  private router = inject(Router);

  inProgress = signal<Enrollment[]>([]);
  completed = signal<Enrollment[]>([]);
  bookmarks = signal<Enrollment[]>([]);
  loading = signal(true);
  activeTab = 0;

  ngOnInit(): void { this.loadEnrollments(); }

  loadEnrollments(): void {
    this.loading.set(true);
    this.trainingService.getMyEnrollments({ status: 'in_progress' }).subscribe({
      next: (e) => { this.inProgress.set(e); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.trainingService.getMyEnrollments({ status: 'completed' }).subscribe({
      next: (e) => this.completed.set(e), error: () => {},
    });
    this.trainingService.getMyEnrollments({ status: 'bookmarked' }).subscribe({
      next: (e) => this.bookmarks.set(e), error: () => {},
    });
  }

  continueLearning(courseId: string): void {
    this.router.navigate(['/training/courses', courseId]);
  }

  getProgressColor(progress: number): string {
    if (progress >= 75) return '#10b981';
    if (progress >= 50) return '#f59e0b';
    return '#3b82f6';
  }
}
