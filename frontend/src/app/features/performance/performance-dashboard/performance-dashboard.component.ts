import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRippleModule } from '@angular/material/core';
import { PerformanceService } from '../performance.service';
import { PerformanceDashboardStats } from '../performance.models';

@Component({
  selector: 'app-performance-dashboard',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatRippleModule],
  templateUrl: './performance-dashboard.component.html',
  styleUrl: './performance-dashboard.component.scss',
})
export class PerformanceDashboardComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private router = inject(Router);

  stats = signal<PerformanceDashboardStats | null>(null);
  loading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.loading.set(true);
    this.performanceService.getDashboardStats().subscribe({
      next: (data) => { this.stats.set(data); this.loading.set(false); },
      error: () => { this.error.set('Failed to load dashboard'); this.loading.set(false); },
    });
  }

  navigateTo(path: string): void {
    this.router.navigate(['/performance', path]);
  }

  getProgressColor(progress: number): string {
    if (progress >= 75) return '#10b981';
    if (progress >= 50) return '#f59e0b';
    if (progress >= 25) return '#f97316';
    return '#ef4444';
  }

  getBarColor(index: number): string {
    const colors = ['#3b82f6', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6'];
    return colors[index % colors.length];
  }
}
