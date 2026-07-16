import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveType, LeaveBalance } from '../leave.models';

@Component({
  selector: 'app-apply-leave',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './apply-leave.component.html',
  styleUrl: './apply-leave.component.scss',
})
export class ApplyLeaveComponent implements OnInit {
  leaveTypes = signal<LeaveType[]>([]);
  balances = signal<LeaveBalance[]>([]);
  selectedLeaveType = signal('');
  startDate = signal<Date | null>(null);
  endDate = signal<Date | null>(null);
  reason = signal('');
  totalDays = signal(0);
  selectedBalance = signal<LeaveBalance | null>(null);
  loading = signal(false);
  submitting = signal(false);

  minDate = new Date();

  constructor(
    private leaveService: LeaveService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loadLeaveTypes();
    this.loadBalances();
  }

  loadLeaveTypes(): void {
    this.leaveService.getLeaveTypes().subscribe({
      next: (types) => this.leaveTypes.set(types),
      error: () => {},
    });
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

  onLeaveTypeChange(): void {
    const balance = this.balances().find(
      (b) => b.leaveType === this.selectedLeaveType(),
    );
    this.selectedBalance.set(balance || null);
  }

  onDateChange(): void {
    if (this.startDate() && this.endDate()) {
      const start = new Date(this.startDate()!);
      const end = new Date(this.endDate()!);
      if (end >= start) {
        let count = 0;
        const current = new Date(start);
        while (current <= end) {
          const day = current.getDay();
          if (day !== 0 && day !== 6) count++;
          current.setDate(current.getDate() + 1);
        }
        this.totalDays.set(count);
      } else {
        this.totalDays.set(0);
      }
    }
  }

  onSubmit(): void {
    if (!this.selectedLeaveType() || !this.startDate() || !this.endDate() || !this.reason()) {
      this.snackBar.open('Please fill in all required fields', 'Close', { duration: 3000 });
      return;
    }

    const balance = this.selectedBalance();
    if (balance && this.totalDays() > balance.balance) {
      this.snackBar.open(`Insufficient balance. Available: ${balance.balance} days`, 'Close', {
        duration: 3000,
      });
      return;
    }

    this.submitting.set(true);
    this.leaveService
      .applyLeave({
        leaveType: this.selectedLeaveType(),
        startDate: this.startDate()!.toISOString().split('T')[0],
        endDate: this.endDate()!.toISOString().split('T')[0],
        reason: this.reason(),
      })
      .subscribe({
        next: () => {
          this.snackBar.open('Leave applied successfully!', 'Close', { duration: 3000 });
          this.router.navigate(['/leave/my-leaves']);
        },
        error: () => {
          this.snackBar.open('Failed to apply leave. Please try again.', 'Close', { duration: 3000 });
          this.submitting.set(false);
        },
      });
  }

  clearForm(): void {
    this.selectedLeaveType.set('');
    this.startDate.set(null);
    this.endDate.set(null);
    this.reason.set('');
    this.totalDays.set(0);
    this.selectedBalance.set(null);
  }
}
