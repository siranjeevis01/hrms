import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { CalibrationSession } from '../performance.models';

@Component({
  selector: 'app-calibration',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatProgressSpinnerModule, MatSnackBarModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './calibration.component.html',
  styleUrl: './calibration.component.scss',
})
export class CalibrationComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  sessions = signal<CalibrationSession[]>([]);
  selectedSession = signal<CalibrationSession | null>(null);
  loading = signal(true);
  editingRating: string | null = null;
  newCalibratedRating = 0;
  discussion = '';

  ngOnInit(): void { this.loadSessions(); }

  loadSessions(): void {
    this.loading.set(true);
    this.performanceService.getCalibrationSessions().subscribe({
      next: (sessions) => { this.sessions.set(sessions); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  selectSession(session: CalibrationSession): void {
    this.selectedSession.set(session);
  }

  editRating(employeeId: string, currentRating: number): void {
    this.editingRating = employeeId;
    this.newCalibratedRating = currentRating;
    this.discussion = '';
  }

  saveRating(employeeId: string): void {
    const s = this.selectedSession();
    if (!s) return;
    this.performanceService.updateCalibrationRating(s.id, employeeId, this.newCalibratedRating, this.discussion).subscribe({
      next: (updated) => {
        this.selectedSession.set(updated);
        this.editingRating = null;
        this.snackBar.open('Rating updated', 'Close', { duration: 2000 });
      },
      error: () => this.snackBar.open('Failed', 'Close', { duration: 2000 }),
    });
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = { scheduled: 'bg-blue-100 text-blue-800', in_progress: 'bg-amber-100 text-amber-800', completed: 'bg-green-100 text-green-800' };
    return c[status] || 'bg-gray-100 text-gray-800';
  }
}
