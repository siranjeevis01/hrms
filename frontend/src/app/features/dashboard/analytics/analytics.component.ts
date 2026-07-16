import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BaseChartDirective } from 'ng2-charts';
import { DashboardService } from '../dashboard.service';
import { ChartData, PayrollSummary } from '../dashboard.models';

@Component({
  selector: 'app-analytics',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatProgressSpinnerModule,
    BaseChartDirective,
  ],
  templateUrl: './analytics.component.html',
  styleUrl: './analytics.component.scss',
})
export class AnalyticsComponent {
  private dashboardService = inject(DashboardService);

  selectedTab = signal(0);
  loading = signal(false);

  dateFrom = signal<Date | null>(null);
  dateTo = signal<Date | null>(null);
  selectedDepartment = signal<string>('');

  departments = signal<string[]>([
    'Engineering',
    'HR',
    'Finance',
    'Marketing',
    'Sales',
    'Operations',
  ]);

  employeeChartData = signal<ChartData | null>(null);
  attendanceChartData = signal<ChartData | null>(null);
  payrollSummary = signal<PayrollSummary | null>(null);

  displayedColumns = ['department', 'employeeCount', 'totalSalary'];

  pieChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: { legend: { position: 'right' as const } },
  };

  barChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: { legend: { position: 'top' as const } },
    scales: { y: { beginAtZero: true } },
  };

  employeePieData: any = null;
  attendanceBarData: any = null;

  ngOnInit(): void {
    this.loadAnalytics();
  }

  loadAnalytics(): void {
    this.loading.set(true);

    this.dashboardService.getEmployeeChart().subscribe({
      next: (data) => {
        this.employeeChartData.set(data);
        this.employeePieData = {
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
        this.attendanceBarData = {
          labels: data.labels,
          datasets: data.datasets.map((ds) => ({
            ...ds,
            backgroundColor: '#3b82f6',
            borderColor: '#2563eb',
            borderWidth: 1,
          })),
        };
      },
      error: () => {},
    });

    this.dashboardService.getPayrollSummary().subscribe({
      next: (data) => this.payrollSummary.set(data),
      error: () => {},
    });

    setTimeout(() => this.loading.set(false), 500);
  }

  applyFilters(): void {
    this.loadAnalytics();
  }
}
