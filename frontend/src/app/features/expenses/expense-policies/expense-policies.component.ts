import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { TitleCasePipe } from '@angular/common';
import { ExpensesService } from '../expenses.service';
import { ExpensePolicy } from '../expenses.models';

@Component({
  selector: 'app-expense-policies',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, TitleCasePipe],
  templateUrl: './expense-policies.component.html',
  styleUrl: './expense-policies.component.scss',
})
export class ExpensePoliciesComponent implements OnInit {
  private expensesService = inject(ExpensesService);
  policies = signal<ExpensePolicy[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.expensesService.getPolicies().subscribe({
      next: (policies) => { this.policies.set(policies); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount);
  }
}
