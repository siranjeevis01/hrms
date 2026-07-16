import { Component, OnInit, signal, computed, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AttendanceService } from '../attendance.service';
import { AttendanceRecord, AttendanceSummary } from '../attendance.models';

@Component({
  selector: 'app-attendance-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './attendance-dashboard.component.html',
  styleUrl: './attendance-dashboard.component.scss',
})
export class AttendanceDashboardComponent implements OnInit, OnDestroy {
  todayAttendance = signal<AttendanceRecord | null>(null);
  weeklyAttendance = signal<AttendanceRecord[]>([]);
  summary = signal<AttendanceSummary | null>(null);
  loading = signal(true);
  error = signal<string | null>(null);
  isCheckedIn = signal(false);
  currentTime = signal(new Date());
  workDuration = signal('00:00:00');

  private timerInterval: ReturnType<typeof setInterval> | null = null;

  statusColor = computed(() => {
    const record = this.todayAttendance();
    if (!record) return '#6b7280';
    switch (record.status) {
      case 'Present': return '#10b981';
      case 'Late': return '#f59e0b';
      case 'WFH': return '#3b82f6';
      case 'Absent': return '#ef4444';
      default: return '#6b7280';
    }
  });

  averageHours = computed(() => {
    const records = this.weeklyAttendance().filter(r => r.totalHours > 0);
    if (records.length === 0) return 0;
    return records.reduce((sum, r) => sum + r.totalHours, 0) / records.length;
  });

  onTimePercentage = computed(() => {
    const records = this.weeklyAttendance();
    if (records.length === 0) return 0;
    const onTime = records.filter(r => r.status !== 'Late' && r.status !== 'Absent').length;
    return Math.round((onTime / records.length) * 100);
  });

  weekDays = computed(() => {
    const days = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    const today = new Date();
    const dayOfWeek = today.getDay();
    const monday = new Date(today);
    monday.setDate(today.getDate() - ((dayOfWeek + 6) % 7));

    return days.map((day, index) => {
      const date = new Date(monday);
      date.setDate(monday.getDate() + index);
      const dateStr = date.toISOString().split('T')[0];
      const record = this.weeklyAttendance().find(r => r.date === dateStr);
      return {
        day,
        date: date.getDate(),
        isToday: dateStr === new Date().toISOString().split('T')[0],
        record,
        status: record?.status || 'No Data',
        isPast: date < new Date(new Date().setHours(0, 0, 0, 0)),
      };
    });
  });

  constructor(private attendanceService: AttendanceService) {}

  ngOnInit(): void {
    this.loadTodayAttendance();
    this.loadWeeklyAttendance();
    this.loadMonthlySummary();
    this.startTimer();
  }

  ngOnDestroy(): void {
    if (this.timerInterval) clearInterval(this.timerInterval);
  }

  startTimer(): void {
    this.timerInterval = setInterval(() => {
      this.currentTime.set(new Date());
      this.updateWorkDuration();
    }, 1000);
  }

  updateWorkDuration(): void {
    const record = this.todayAttendance();
    if (!record?.checkIn) {
      this.workDuration.set('00:00:00');
      return;
    }
    const checkIn = new Date(record.checkIn);
    const now = record.checkOut ? new Date(record.checkOut) : new Date();
    const diff = now.getTime() - checkIn.getTime();
    const hours = Math.floor(diff / 3600000);
    const minutes = Math.floor((diff % 3600000) / 60000);
    const seconds = Math.floor((diff % 60000) / 1000);
    this.workDuration.set(
      `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`,
    );
  }

  loadTodayAttendance(): void {
    const today = new Date().toISOString().split('T')[0];
    this.attendanceService.getMyAttendance(today).subscribe({
      next: (record) => {
        this.todayAttendance.set(record);
        this.isCheckedIn.set(!!record.checkIn && !record.checkOut);
        this.updateWorkDuration();
      },
      error: () => {
        this.todayAttendance.set(null);
        this.isCheckedIn.set(false);
      },
    });
  }

  loadWeeklyAttendance(): void {
    const today = new Date();
    const dayOfWeek = today.getDay();
    const monday = new Date(today);
    monday.setDate(today.getDate() - ((dayOfWeek + 6) % 7));
    const sunday = new Date(monday);
    sunday.setDate(monday.getDate() + 6);

    this.attendanceService
      .getAttendance({
        page: 1,
        pageSize: 7,
        startDate: monday.toISOString().split('T')[0],
        endDate: sunday.toISOString().split('T')[0],
      })
      .subscribe({
        next: (result) => {
          this.weeklyAttendance.set(result.items);
          this.loading.set(false);
        },
        error: (err) => {
          this.error.set('Failed to load weekly attendance');
          this.loading.set(false);
        },
      });
  }

  loadMonthlySummary(): void {
    const now = new Date();
    this.attendanceService.getAttendanceSummary(now.getMonth() + 1, now.getFullYear()).subscribe({
      next: (summary) => this.summary.set(summary),
      error: () => {},
    });
  }

  handleCheckIn(): void {
    this.attendanceService
      .markCheckIn({ checkInTime: new Date().toISOString(), workMode: 'Office' })
      .subscribe({
        next: (record) => {
          this.todayAttendance.set(record);
          this.isCheckedIn.set(true);
          this.updateWorkDuration();
        },
        error: (err) => this.error.set('Failed to check in'),
      });
  }

  handleCheckOut(): void {
    this.attendanceService.markCheckOut().subscribe({
      next: () => {
        this.loadTodayAttendance();
        this.isCheckedIn.set(false);
      },
      error: () => this.error.set('Failed to check out'),
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Present': return 'bg-emerald-100 text-emerald-700';
      case 'Late': return 'bg-amber-100 text-amber-700';
      case 'WFH': return 'bg-blue-100 text-blue-700';
      case 'Absent': return 'bg-red-100 text-red-700';
      case 'WeekOff': return 'bg-gray-100 text-gray-500';
      case 'Holiday': return 'bg-purple-100 text-purple-700';
      default: return 'bg-gray-50 text-gray-400';
    }
  }
}
