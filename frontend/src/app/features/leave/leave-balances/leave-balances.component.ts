import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveBalance } from '../leave.models';

@Component({
  selector: 'app-leave-balances',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './leave-balances.component.html',
  styleUrl: './leave-balances.component.scss',
})
export class LeaveBalancesComponent implements OnInit {
  balances = signal<LeaveBalance[]>([]);
  loading = signal(true);

  constructor(private leaveService: LeaveService) {}

  ngOnInit(): void {
    this.loadBalances();
  }

  loadBalances(): void {
    this.leaveService.getLeaveBalances('current-user').subscribe({
      next: (balances) => {
        this.balances.set(balances);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  getProgressPercent(balance: LeaveBalance): number {
    if (balance.entitled === 0) return 0;
    return Math.round((balance.taken / balance.entitled) * 100);
  }

  getBarColor(balance: LeaveBalance): string {
    const pct = this.getProgressPercent(balance);
    if (pct >= 90) return 'bg-red-500';
    if (pct >= 70) return 'bg-amber-500';
    return 'bg-emerald-500';
  }

  getBarBg(balance: LeaveBalance): string {
    const pct = this.getProgressPercent(balance);
    if (pct >= 90) return 'bg-red-100';
    if (pct >= 70) return 'bg-amber-100';
    return 'bg-emerald-100';
  }

  getTotalEntitled(): number {
    return this.balances().reduce((s, b) => s + b.entitled, 0);
  }

  getTotalTaken(): number {
    return this.balances().reduce((s, b) => s + b.taken, 0);
  }

  getTotalBalance(): number {
    return this.balances().reduce((s, b) => s + b.balance, 0);
  }
}
