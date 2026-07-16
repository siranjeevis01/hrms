import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { ExpensesService } from '../expenses.service';
import { ExpenseClaim, ExpenseStatus } from '../expenses.models';

@Component({
  selector: 'app-my-expenses',
  standalone: true,
  imports: [NgClass, DatePipe, TitleCasePipe, MatTabsModule, MatCardModule, MatButtonModule, MatIconModule, MatMenuModule, MatProgressSpinnerModule],
  templateUrl: './my-expenses.component.html',
  styleUrl: './my-expenses.component.scss',
})
export class MyExpensesComponent implements OnInit {
  private expensesService = inject(ExpensesService);
  protected router = inject(Router);

  expenses = signal<ExpenseClaim[]>([]);
  loading = signal(true);
  activeTab = signal<ExpenseStatus>('draft');

  ngOnInit(): void {
    this.loadExpenses();
  }

  loadExpenses(): void {
    this.loading.set(true);
    this.expensesService.getMyExpenses(this.activeTab()).subscribe({
      next: (expenses) => { this.expenses.set(expenses); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  onTabChange(status: string): void {
    this.activeTab.set(status as ExpenseStatus);
    this.loadExpenses();
  }

  cancelExpense(id: string): void {
    this.expensesService.cancelExpense(id).subscribe({ next: () => this.loadExpenses() });
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount);
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = { draft: 'bg-gray-100 text-gray-700', submitted: 'bg-blue-100 text-blue-700', approved: 'bg-green-100 text-green-700', rejected: 'bg-red-100 text-red-700', reimbursed: 'bg-purple-100 text-purple-700' };
    return colors[status] || 'bg-gray-100 text-gray-700';
  }
}
