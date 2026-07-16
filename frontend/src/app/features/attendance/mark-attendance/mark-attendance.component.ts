import { Component, OnInit, signal, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AttendanceService } from '../attendance.service';
import { AttendanceRecord, WorkMode } from '../attendance.models';

@Component({
  selector: 'app-mark-attendance',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './mark-attendance.component.html',
  styleUrl: './mark-attendance.component.scss',
})
export class MarkAttendanceComponent implements OnInit, OnDestroy {
  todayRecord = signal<AttendanceRecord | null>(null);
  isCheckedIn = signal(false);
  currentTime = signal(new Date());
  workDuration = signal('00:00:00');
  workMode = signal<WorkMode>('Office');
  location = signal<string | null>(null);
  latitude = signal<number | null>(null);
  longitude = signal<number | null>(null);
  loading = signal(false);
  capturingLocation = signal(false);
  error = signal<string | null>(null);
  success = signal<string | null>(null);

  private timerInterval: ReturnType<typeof setInterval> | null = null;

  workModes: WorkMode[] = ['Office', 'WFH', 'Hybrid'];

  constructor(private attendanceService: AttendanceService) {}

  ngOnInit(): void {
    this.loadTodayRecord();
    this.startTimer();
  }

  ngOnDestroy(): void {
    if (this.timerInterval) clearInterval(this.timerInterval);
  }

  startTimer(): void {
    this.timerInterval = setInterval(() => {
      this.currentTime.set(new Date());
      this.updateDuration();
    }, 1000);
  }

  updateDuration(): void {
    const record = this.todayRecord();
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

  loadTodayRecord(): void {
    const today = new Date().toISOString().split('T')[0];
    this.attendanceService.getMyAttendance(today).subscribe({
      next: (record) => {
        this.todayRecord.set(record);
        this.isCheckedIn.set(!!record.checkIn && !record.checkOut);
        if (record.workMode) this.workMode.set(record.workMode);
        this.updateDuration();
      },
      error: () => {
        this.todayRecord.set(null);
        this.isCheckedIn.set(false);
      },
    });
  }

  captureLocation(): void {
    this.capturingLocation.set(true);
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.latitude.set(position.coords.latitude);
          this.longitude.set(position.coords.longitude);
          this.location.set(`${position.coords.latitude.toFixed(4)}, ${position.coords.longitude.toFixed(4)}`);
          this.capturingLocation.set(false);
        },
        () => {
          this.location.set('Location unavailable');
          this.capturingLocation.set(false);
        },
      );
    } else {
      this.location.set('Geolocation not supported');
      this.capturingLocation.set(false);
    }
  }

  handleCheckIn(): void {
    this.loading.set(true);
    this.error.set(null);
    this.success.set(null);
    this.captureLocation();

    setTimeout(() => {
      this.attendanceService
        .markCheckIn({
          checkInTime: new Date().toISOString(),
          workMode: this.workMode(),
          location: this.location() || undefined,
          latitude: this.latitude() || undefined,
          longitude: this.longitude() || undefined,
        })
        .subscribe({
          next: (record) => {
            this.todayRecord.set(record);
            this.isCheckedIn.set(true);
            this.success.set('Checked in successfully!');
            this.loading.set(false);
            this.updateDuration();
          },
          error: () => {
            this.error.set('Failed to check in. Please try again.');
            this.loading.set(false);
          },
        });
    }, 500);
  }

  handleCheckOut(): void {
    this.loading.set(true);
    this.error.set(null);
    this.success.set(null);
    this.attendanceService.markCheckOut().subscribe({
      next: () => {
        this.success.set('Checked out successfully!');
        this.loadTodayRecord();
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to check out. Please try again.');
        this.loading.set(false);
      },
    });
  }

  clearMessages(): void {
    this.error.set(null);
    this.success.set(null);
  }
}
