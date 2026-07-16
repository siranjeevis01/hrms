import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveBalance, LeaveRequest, Holiday } from '../leave.models';

@Component({
  selector: 'app-leave-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './leave-dashboard.component.html',
  styleUrl: './leave-dashboard.component.scss',
})
export class LeaveDashboardComponent implements OnInit {
  balances = signal<LeaveBalance[]>([]);
  recentLeaves = signal<LeaveRequest[]>([]);
  teamOnLeave = signal<LeaveRequest[]>([]);
  holidays = signal<Holiday[]>([]);
  loading = signal(true);

  constructor(private leaveService: LeaveService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    const employeeId = 'current-user';
    this.leaveService.getLeaveBalances(employeeId).subscribe({
      next: (b) => this.balances.set(b),
      error: () => {},
    });

    this.leaveService.getMyLeaves({ page: 1, pageSize: 5 }).subscribe({
      next: (r) => {
        this.recentLeaves.set(r.items);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });

    this.leaveService.getTeamLeaves({ page: 1, pageSize: 5, status: 'Approved' }).subscribe({
      next: (r) => this.teamOnLeave.set(r.items),
      error: () => {},
    });

    this.leaveService.getHolidays(new Date().getFullYear()).subscribe({
      next: (h) => {
        const upcoming = h
          .filter((holiday) => new Date(holiday.date) >= new Date())
          .slice(0, 5);
        this.holidays.set(upcoming);
      },
      error: () => {},
    });
  }

  getProgressPercent(balance: LeaveBalance): number {
    if (balance.entitled === 0) return 0;
    return Math.round((balance.taken / balance.entitled) * 100);
  }

  getProgressColor(balance: LeaveBalance): string {
    const pct = this.getProgressPercent(balance);
    if (pct >= 90) return '#ef4444';
    if (pct >= 70) return '#f59e0b';
    return '#10b981';
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending': return 'bg-amber-100 text-amber-700';
      case 'Approved': return 'bg-emerald-100 text-emerald-700';
      case 'Rejected': return 'bg-red-100 text-red-700';
      default: return 'bg-gray-100 text-gray-600';
    }
  }

  getInitials(name: string): string {
    if (!name) return '';
    return name.split(' ').map(n => n[0]).join('');
  }
}
