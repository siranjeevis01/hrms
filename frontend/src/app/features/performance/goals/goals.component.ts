import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { Goal, GoalStatus, GoalCategory } from '../performance.models';

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatSelectModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatChipsModule, MatRippleModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './goals.component.html',
  styleUrl: './goals.component.scss',
})
export class GoalsComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private router = inject(Router);

  goals = signal<Goal[]>([]);
  loading = signal(true);
  filterStatus = '';
  filterCategory = '';
  viewFilter = 'all';

  filteredGoals = computed(() => {
    let result = this.goals();
    if (this.filterStatus) result = result.filter(g => g.status === this.filterStatus);
    if (this.filterCategory) result = result.filter(g => g.category === this.filterCategory);
    return result;
  });

  ngOnInit(): void {
    this.loadGoals();
  }

  loadGoals(): void {
    this.loading.set(true);
    this.performanceService.getGoals().subscribe({
      next: (goals) => { this.goals.set(goals); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  navigateTo(path: string): void {
    this.router.navigate(['/performance', path]);
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = {
      not_started: 'bg-gray-100 text-gray-800', in_progress: 'bg-blue-100 text-blue-800',
      completed: 'bg-green-100 text-green-800', cancelled: 'bg-red-100 text-red-800',
    };
    return c[status] || 'bg-gray-100 text-gray-800';
  }

  getPriorityColor(priority: string): string {
    const c: Record<string, string> = {
      low: 'bg-gray-100 text-gray-600', medium: 'bg-blue-100 text-blue-600',
      high: 'bg-orange-100 text-orange-600', critical: 'bg-red-100 text-red-600',
    };
    return c[priority] || 'bg-gray-100 text-gray-600';
  }

  getProgressColor(progress: number): string {
    if (progress >= 75) return '#10b981';
    if (progress >= 50) return '#f59e0b';
    return '#3b82f6';
  }
}
