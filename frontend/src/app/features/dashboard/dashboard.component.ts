import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatRippleModule } from '@angular/material/core';
import { BaseChartDirective } from 'ng2-charts';
import { DashboardService } from './dashboard.service';
import {
  DashboardStats,
  Activity,
  Event,
  ChartData,
} from './dashboard.models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatRippleModule,
    BaseChartDirective,
    NgClass,
    DatePipe,
    TitleCasePipe,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  private dashboardService = inject(DashboardService);
  private router = inject(Router);

  stats = signal<DashboardStats | null>(null);
  activities = signal<Activity[]>([]);
  events = signal<Event[]>([]);
  employeeChartData = signal<ChartData | null>(null);
  attendanceChartData = signal<ChartData | null>(null);
  loading = signal(true);
  error = signal<string | null>(null);

  pieChartData: any = null;
  lineChartData: any = null;

  pieChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { position: 'right' as const },
    },
  };

  lineChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { position: 'top' as const },
    },
    scales: {
      y: { beginAtZero: true },
    },
  };

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading.set(true);
    this.error.set(null);

    this.dashboardService.getDashboardStats().subscribe({
      next: (data) => this.stats.set(data),
      error: () => this.error.set('Failed to load dashboard stats'),
    });

    this.dashboardService.getRecentActivities().subscribe({
      next: (data) => this.activities.set(data),
      error: () => {},
    });

    this.dashboardService.getUpcomingEvents().subscribe({
      next: (data) => this.events.set(data),
      error: () => {},
    });

    this.dashboardService.getEmployeeChart().subscribe({
      next: (data) => {
        this.employeeChartData.set(data);
        this.pieChartData = {
          labels: data.labels,
          datasets: data.datasets.map((ds) => ({
            ...ds,
            backgroundColor: [
              '#3b82f6', '#10b981', '#f59e0b', '#ef4444',
              '#8b5cf6', '#ec4899', '#06b6d4', '#84cc16',
            ],
          })),
        };
      },
      error: () => {},
    });

    this.dashboardService.getAttendanceChart().subscribe({
      next: (data) => {
        this.attendanceChartData.set(data);
        this.lineChartData = {
          labels: data.labels,
          datasets: data.datasets.map((ds) => ({
            ...ds,
            borderColor: '#3b82f6',
            backgroundColor: 'rgba(59, 130, 246, 0.1)',
            fill: true,
            tension: 0.4,
          })),
        };
      },
      error: () => {},
    });

    this.dashboardService.getDashboardStats().subscribe({
      next: () => this.loading.set(false),
      error: () => this.loading.set(false),
    });
  }

  navigateTo(path: string): void {
    this.router.navigate([path]);
  }

  getActivityIcon(type: string): string {
    const icons: Record<string, string> = {
      hire: 'person_add',
      leave: 'event_busy',
      promotion: 'trending_up',
      transfer: 'swap_horiz',
      attendance: 'fact_check',
      payroll: 'payments',
    };
    return icons[type] || 'info';
  }

  getEventTypeColor(type: string): string {
    const colors: Record<string, string> = {
      holiday: 'bg-red-100 text-red-800',
      meeting: 'bg-blue-100 text-blue-800',
      deadline: 'bg-yellow-100 text-yellow-800',
      training: 'bg-green-100 text-green-800',
    };
    return colors[type] || 'bg-gray-100 text-gray-800';
  }
}
