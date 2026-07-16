import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveRequest, LeaveStatus } from '../leave.models';

@Component({
  selector: 'app-my-leaves',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './my-leaves.component.html',
  styleUrl: './my-leaves.component.scss',
})
export class MyLeavesComponent implements OnInit {
  leaves = signal<LeaveRequest[]>([]);
  loading = signal(true);
  activeTab = signal(0);
  displayedColumns = ['leaveType', 'dates', 'days', 'status', 'appliedOn', 'actions'];

  tabFilters: (LeaveStatus | undefined)[] = [undefined, 'Pending', 'Approved', 'Rejected'];

  constructor(
    private leaveService: LeaveService,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    this.loadLeaves();
  }

  loadLeaves(): void {
    this.loading.set(true);
    const status = this.tabFilters[this.activeTab()];
    this.leaveService
      .getMyLeaves({
        page: 1,
        pageSize: 50,
        status,
      })
      .subscribe({
        next: (result) => {
          this.leaves.set(result.items);
          this.loading.set(false);
        },
        error: () => this.loading.set(false),
      });
  }

  onTabChange(index: number): void {
    this.activeTab.set(index);
    this.loadLeaves();
  }

  cancelLeave(id: string): void {
    this.leaveService.cancelLeave(id).subscribe({
      next: () => {
        this.snackBar.open('Leave cancelled successfully', 'Close', { duration: 3000 });
        this.loadLeaves();
      },
      error: () => {
        this.snackBar.open('Failed to cancel leave', 'Close', { duration: 3000 });
      },
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending': return 'bg-amber-100 text-amber-700';
      case 'Approved': return 'bg-emerald-100 text-emerald-700';
      case 'Rejected': return 'bg-red-100 text-red-700';
      case 'Cancelled': return 'bg-gray-100 text-gray-600';
      default: return 'bg-gray-100 text-gray-600';
    }
  }

  formatDateRange(start: string, end: string): string {
    const s = new Date(start);
    const e = new Date(end);
    const options: Intl.DateTimeFormatOptions = { month: 'short', day: 'numeric' };
    return `${s.toLocaleDateString('en-US', options)} - ${e.toLocaleDateString('en-US', options)}`;
  }
}
