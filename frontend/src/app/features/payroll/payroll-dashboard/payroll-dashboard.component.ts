import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PayrollService } from '../payroll.service';
import { PayrollSummary } from '../payroll.models';

@Component({
  selector: 'app-payroll-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './payroll-dashboard.component.html',
  styleUrl: './payroll-dashboard.component.scss',
})
export class PayrollDashboardComponent implements OnInit {
  summary = signal<PayrollSummary | null>(null);
  loading = signal(true);
  monthlyTrend = signal<{ month: string; amount: number }[]>([]);
  departmentData = signal<{ name: string; amount: number; color: string }[]>([]);

  constructor(private payrollService: PayrollService) {}

  ngOnInit(): void {
    this.loadSummary();
  }

  loadSummary(): void {
    this.payrollService.getPayrollSummary().subscribe({
      next: (summary) => {
        this.summary.set(summary);
        this.buildChartData(summary);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  buildChartData(_summary: PayrollSummary): void {
    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'];
    this.monthlyTrend.set(
      months.map((m, i) => ({
        month: m,
        amount: 400000 + Math.floor(Math.random() * 200000) + i * 15000,
      })),
    );

    const colors = ['#6366f1', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6'];
    const depts = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];
    this.departmentData.set(
      depts.map((d, i) => ({
        name: d,
        amount: 300000 + Math.floor(Math.random() * 300000),
        color: colors[i],
      })),
    );
  }

  maxTrendValue(): number {
    return Math.max(...this.monthlyTrend().map((m) => m.amount), 1);
  }

  maxDeptValue(): number {
    return Math.max(...this.departmentData().map((d) => d.amount), 1);
  }

  formatCurrency(value: number): string {
    if (value >= 100000) return `${(value / 100000).toFixed(1)}L`;
    if (value >= 1000) return `${(value / 1000).toFixed(0)}K`;
    return value.toString();
  }
}
