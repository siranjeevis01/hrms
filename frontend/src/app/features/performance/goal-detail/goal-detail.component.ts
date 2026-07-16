import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { Goal, KeyResult } from '../performance.models';

@Component({
  selector: 'app-goal-detail',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatListModule, MatDividerModule,
    MatSnackBarModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './goal-detail.component.html',
  styleUrl: './goal-detail.component.scss',
})
export class GoalDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  goal = signal<Goal | null>(null);
  loading = signal(true);
  updatingKr = signal<string | null>(null);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadGoal(id);
  }

  loadGoal(id: string): void {
    this.loading.set(true);
    this.performanceService.getGoal(id).subscribe({
      next: (goal) => { this.goal.set(goal); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  updateKeyResultProgress(keyResult: KeyResult, newCurrent: number): void {
    const g = this.goal();
    if (!g) return;
    this.updatingKr.set(keyResult.id);
    this.performanceService.updateKeyResult(g.id, keyResult.id, newCurrent).subscribe({
      next: (updated) => {
        this.goal.set(updated);
        this.updatingKr.set(null);
        this.snackBar.open('Progress updated', 'Close', { duration: 2000 });
      },
      error: () => { this.updatingKr.set(null); this.snackBar.open('Failed to update', 'Close', { duration: 2000 }); },
    });
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = {
      not_started: 'bg-gray-100 text-gray-800', in_progress: 'bg-blue-100 text-blue-800',
      completed: 'bg-green-100 text-green-800', cancelled: 'bg-red-100 text-red-800',
    };
    return c[status] || 'bg-gray-100 text-gray-800';
  }

  goBack(): void { this.router.navigate(['/performance/goals']); }
}
