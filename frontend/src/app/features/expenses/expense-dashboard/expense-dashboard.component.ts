import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BaseChartDirective } from 'ng2-charts';
import { ExpensesService } from '../expenses.service';
import { ExpenseDashboardStats, ExpenseClaim } from '../expenses.models';

@Component({
  selector: 'app-expense-dashboard',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule,
    MatListModule,
    MatProgressSpinnerModule,
    BaseChartDirective,
    NgClass,
    DatePipe,
    TitleCasePipe,
  ],
  templateUrl: './expense-dashboard.component.html',
  styleUrl: './expense-dashboard.component.scss',
})
export class ExpenseDashboardComponent implements OnInit {
  private expensesService = inject(ExpensesService);
  private router = inject(Router);

  stats = signal<ExpenseDashboardStats | null>(null);
  recentExpenses = signal<ExpenseClaim[]>([]);
  loading = signal(true);
  pieChartData: any = null;
  pieChartOptions = { responsive: true, maintainAspectRatio: false, plugins: { legend: { position: 'right' as const } } };

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.expensesService.getDashboardStats().subscribe({
      next: (stats) => {
        this.stats.set(stats);
        this.pieChartData = {
          labels: stats.categoryBreakdown.map((c) => c.category.replace('_', ' ')),
          datasets: [{ data: stats.categoryBreakdown.map((c) => c.amount), backgroundColor: ['#3b82f6', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6', '#ec4899', '#06b6d4', '#84cc16'] }],
        };
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
    this.expensesService.getMyExpenses().subscribe({
      next: (expenses) => this.recentExpenses.set(expenses.slice(0, 5)),
      error: () => {},
    });
  }

  navigateTo(path: string): void {
    this.router.navigate(['/expenses', path]);
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount);
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = { draft: 'bg-gray-100 text-gray-700', submitted: 'bg-blue-100 text-blue-700', approved: 'bg-green-100 text-green-700', rejected: 'bg-red-100 text-red-700', reimbursed: 'bg-purple-100 text-purple-700' };
    return colors[status] || 'bg-gray-100 text-gray-700';
  }
}
