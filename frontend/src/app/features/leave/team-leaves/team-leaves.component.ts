import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveRequest } from '../leave.models';

@Component({
  selector: 'app-team-leaves',
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
    MatTableModule,
    MatDialogModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './team-leaves.component.html',
  styleUrl: './team-leaves.component.scss',
})
export class TeamLeavesComponent implements OnInit {
  pendingLeaves = signal<LeaveRequest[]>([]);
  allLeaves = signal<LeaveRequest[]>([]);
  loading = signal(true);
  filterStatus = signal('');
  filterDepartment = signal('');
  actionComments = signal('');
  processingId = signal<string | null>(null);

  departments = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];
  statuses = ['Pending', 'Approved', 'Rejected'];
  displayedColumns = ['employeeName', 'leaveType', 'dates', 'days', 'reason', 'status', 'actions'];

  constructor(
    private leaveService: LeaveService,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    this.loadPendingLeaves();
    this.loadAllLeaves();
  }

  loadPendingLeaves(): void {
    this.leaveService.getTeamLeaves({ page: 1, pageSize: 50, status: 'Pending' }).subscribe({
      next: (result) => this.pendingLeaves.set(result.items),
      error: () => {},
    });
  }

  loadAllLeaves(): void {
    this.loading.set(true);
    this.leaveService
      .getTeamLeaves({
        page: 1,
        pageSize: 50,
        status: (this.filterStatus() as any) || undefined,
      })
      .subscribe({
        next: (result) => {
          this.allLeaves.set(result.items);
          this.loading.set(false);
        },
        error: () => this.loading.set(false),
      });
  }

  approveLeave(id: string): void {
    this.processingId.set(id);
    this.leaveService.approveLeave(id, this.actionComments()).subscribe({
      next: () => {
        this.snackBar.open('Leave approved', 'Close', { duration: 3000 });
        this.actionComments.set('');
        this.processingId.set(null);
        this.loadPendingLeaves();
        this.loadAllLeaves();
      },
      error: () => {
        this.snackBar.open('Failed to approve leave', 'Close', { duration: 3000 });
        this.processingId.set(null);
      },
    });
  }

  rejectLeave(id: string): void {
    this.processingId.set(id);
    this.leaveService.rejectLeave(id, this.actionComments()).subscribe({
      next: () => {
        this.snackBar.open('Leave rejected', 'Close', { duration: 3000 });
        this.actionComments.set('');
        this.processingId.set(null);
        this.loadPendingLeaves();
        this.loadAllLeaves();
      },
      error: () => {
        this.snackBar.open('Failed to reject leave', 'Close', { duration: 3000 });
        this.processingId.set(null);
      },
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending': return 'bg-amber-100 text-amber-700';
      case 'Approved': return 'bg-emerald-100 text-emerald-700';
      case 'Rejected': return 'bg-red-100 text-red-700';
      default: return 'bg-gray-100 text-gray-600';
    }
  }

  formatDateRange(start: string, end: string): string {
    const s = new Date(start);
    const e = new Date(end);
    return `${s.toLocaleDateString('en-US', { month: 'short', day: 'numeric' })} - ${e.toLocaleDateString('en-US', { month: 'short', day: 'numeric' })}`;
  }

  getInitials(name: string): string {
    if (!name) return '';
    return name.split(' ').map(n => n[0]).join('');
  }
}
