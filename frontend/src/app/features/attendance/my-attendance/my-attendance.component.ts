import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AttendanceService } from '../attendance.service';
import { AttendanceRecord, AttendanceSummary } from '../attendance.models';

@Component({
  selector: 'app-my-attendance',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatButtonToggleModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './my-attendance.component.html',
  styleUrl: './my-attendance.component.scss',
})
export class MyAttendanceComponent implements OnInit {
  viewMode = signal<'calendar' | 'list'>('calendar');
  selectedDate = signal(new Date());
  calendarRecords = signal<AttendanceRecord[]>([]);
  listRecords = signal<AttendanceRecord[]>([]);
  summary = signal<AttendanceSummary | null>(null);
  loading = signal(true);
  currentMonth = signal(new Date());

  calendarDays = signal<{ date: Date; record?: AttendanceRecord; isCurrentMonth: boolean }[]>([]);

  displayedColumns = ['date', 'checkIn', 'checkOut', 'hours', 'status', 'workMode'];

  constructor(private attendanceService: AttendanceService) {}

  ngOnInit(): void {
    this.loadMonth();
  }

  loadMonth(): void {
    this.loading.set(true);
    const month = this.currentMonth();
    const year = month.getFullYear();
    const m = month.getMonth() + 1;
    const startDate = new Date(year, m - 1, 1).toISOString().split('T')[0];
    const endDate = new Date(year, m, 0).toISOString().split('T')[0];

    this.attendanceService
      .getAttendance({ page: 1, pageSize: 31, startDate, endDate })
      .subscribe({
        next: (result) => {
          this.listRecords.set(result.items);
          this.buildCalendar(result.items);
          this.loading.set(false);
        },
        error: () => {
          this.loading.set(false);
          this.buildCalendar([]);
        },
      });

    this.attendanceService.getAttendanceSummary(m, year).subscribe({
      next: (s) => this.summary.set(s),
      error: () => {},
    });
  }

  buildCalendar(records: AttendanceRecord[]): void {
    const month = this.currentMonth();
    const year = month.getFullYear();
    const m = month.getMonth();
    const firstDay = new Date(year, m, 1);
    const lastDay = new Date(year, m + 1, 0);
    const startOffset = (firstDay.getDay() + 6) % 7;
    const days: { date: Date; record?: AttendanceRecord; isCurrentMonth: boolean }[] = [];

    for (let i = startOffset - 1; i >= 0; i--) {
      const d = new Date(year, m, -i);
      days.push({ date: d, isCurrentMonth: false });
    }

    for (let d = 1; d <= lastDay.getDate(); d++) {
      const date = new Date(year, m, d);
      const dateStr = date.toISOString().split('T')[0];
      const record = records.find((r) => r.date === dateStr);
      days.push({ date, record, isCurrentMonth: true });
    }

    const remaining = 42 - days.length;
    for (let i = 1; i <= remaining; i++) {
      const d = new Date(year, m + 1, i);
      days.push({ date: d, isCurrentMonth: false });
    }

    this.calendarDays.set(days);
  }

  prevMonth(): void {
    const curr = this.currentMonth();
    this.currentMonth.set(new Date(curr.getFullYear(), curr.getMonth() - 1, 1));
    this.loadMonth();
  }

  nextMonth(): void {
    const curr = this.currentMonth();
    this.currentMonth.set(new Date(curr.getFullYear(), curr.getMonth() + 1, 1));
    this.loadMonth();
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'Present': return 'bg-emerald-500';
      case 'Late': return 'bg-amber-500';
      case 'WFH': return 'bg-blue-500';
      case 'Absent': return 'bg-red-500';
      case 'HalfDay': return 'bg-purple-500';
      case 'Holiday': return 'bg-indigo-500';
      case 'WeekOff': return 'bg-gray-300';
      default: return 'bg-gray-200';
    }
  }

  getStatusBg(status: string): string {
    switch (status) {
      case 'Present': return 'bg-emerald-50';
      case 'Late': return 'bg-amber-50';
      case 'WFH': return 'bg-blue-50';
      case 'Absent': return 'bg-red-50';
      case 'HalfDay': return 'bg-purple-50';
      case 'Holiday': return 'bg-indigo-50';
      case 'WeekOff': return 'bg-gray-50';
      default: return '';
    }
  }

  exportData(): void {
    const records = this.listRecords();
    const headers = ['Date', 'Check In', 'Check Out', 'Hours', 'Status', 'Work Mode'];
    const rows = records.map((r) => [
      r.date,
      r.checkIn || '',
      r.checkOut || '',
      r.totalHours.toString(),
      r.status,
      r.workMode,
    ]);
    const csv = [headers, ...rows].map((row) => row.join(',')).join('\n');
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `attendance-${this.currentMonth().toISOString().slice(0, 7)}.csv`;
    a.click();
    URL.revokeObjectURL(url);
  }
}
