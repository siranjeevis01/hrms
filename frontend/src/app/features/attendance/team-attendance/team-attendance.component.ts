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
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AttendanceService } from '../attendance.service';
import { AttendanceRecord } from '../attendance.models';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-team-attendance',
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
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './team-attendance.component.html',
  styleUrl: './team-attendance.component.scss',
})
export class TeamAttendanceComponent implements OnInit {
  selectedDate = signal(new Date());
  records = signal<AttendanceRecord[]>([]);
  loading = signal(true);
  filterDepartment = signal('');
  filterStatus = signal('');
  displayedColumns = ['employeeName', 'department', 'checkIn', 'checkOut', 'hours', 'status', 'actions'];

  departments = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];
  statuses = ['Present', 'Absent', 'Late', 'WFH'];

  filteredRecords = signal<AttendanceRecord[]>([]);

  totalPresent = signal(0);
  totalAbsent = signal(0);
  totalLate = signal(0);
  avgHours = signal(0);

  constructor(private attendanceService: AttendanceService, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadTeamAttendance();
  }

  loadTeamAttendance(): void {
    this.loading.set(true);
    const dateStr = this.selectedDate().toISOString().split('T')[0];
    const managerId = this.authService.getCurrentUser()?.id ?? '';
    this.attendanceService.getTeamAttendance(managerId, dateStr).subscribe({
      next: (records) => {
        this.records.set(records);
        this.applyFilters();
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      },
    });
  }

  applyFilters(): void {
    let filtered = this.records();
    if (this.filterDepartment()) {
      filtered = filtered.filter((r) => r.department === this.filterDepartment());
    }
    if (this.filterStatus()) {
      filtered = filtered.filter((r) => r.status === this.filterStatus());
    }
    this.filteredRecords.set(filtered);
    this.computeSummary(filtered);
  }

  computeSummary(records: AttendanceRecord[]): void {
    this.totalPresent.set(records.filter((r) => r.status === 'Present').length);
    this.totalAbsent.set(records.filter((r) => r.status === 'Absent').length);
    this.totalLate.set(records.filter((r) => r.status === 'Late').length);
    const hours = records.filter((r) => r.totalHours > 0).map((r) => r.totalHours);
    this.avgHours.set(hours.length ? hours.reduce((a, b) => a + b, 0) / hours.length : 0);
  }

  onDateChange(date: Date | null): void {
    if (date) {
      this.selectedDate.set(date);
      this.loadTeamAttendance();
    }
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Present': return 'bg-emerald-100 text-emerald-700';
      case 'Late': return 'bg-amber-100 text-amber-700';
      case 'WFH': return 'bg-blue-100 text-blue-700';
      case 'Absent': return 'bg-red-100 text-red-700';
      default: return 'bg-gray-100 text-gray-600';
    }
  }
}
