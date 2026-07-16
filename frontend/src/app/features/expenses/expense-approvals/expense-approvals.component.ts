import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ExpensesService } from '../expenses.service';
import { ExpenseClaim } from '../expenses.models';

@Component({
  selector: 'app-expense-approvals',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatExpansionModule, MatDialogModule, FormsModule, RouterLink, MatProgressSpinnerModule, NgClass, DatePipe, TitleCasePipe],
  templateUrl: './expense-approvals.component.html',
  styleUrl: './expense-approvals.component.scss',
})
export class ExpenseApprovalsComponent implements OnInit {
  private expensesService = inject(ExpensesService);
  claims = signal<ExpenseClaim[]>([]);
  loading = signal(true);
  expandedClaim = signal<string | null>(null);
  rejectComment = signal('');
  selectedClaimId = signal<string | null>(null);

  ngOnInit(): void { this.loadClaims(); }

  loadClaims(): void {
    this.loading.set(true);
    this.expensesService.getPendingApprovals().subscribe({
      next: (claims) => { this.claims.set(claims); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  approve(claimId: string): void {
    this.expensesService.approveExpense({ expenseId: claimId, approved: true, comments: '' }).subscribe({ next: () => this.loadClaims() });
  }

  reject(claimId: string): void {
    this.expensesService.approveExpense({ expenseId: claimId, approved: false, comments: this.rejectComment() }).subscribe({
      next: () => { this.rejectComment.set(''); this.selectedClaimId.set(null); this.loadClaims(); },
    });
  }

  showRejectForm(claimId: string): void { this.selectedClaimId.set(claimId); }
  hideRejectForm(): void { this.selectedClaimId.set(null); this.rejectComment.set(''); }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount);
  }
}
