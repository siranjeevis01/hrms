import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { Appraisal } from '../performance.models';

@Component({
  selector: 'app-appraisals',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatTabsModule, MatProgressSpinnerModule, MatSnackBarModule, DatePipe, TitleCasePipe],
  templateUrl: './appraisals.component.html',
  styleUrl: './appraisals.component.scss',
})
export class AppraisalsComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  myAppraisals = signal<Appraisal[]>([]);
  teamAppraisals = signal<Appraisal[]>([]);
  loading = signal(true);

  ngOnInit(): void { this.loadData(); }

  loadData(): void {
    this.loading.set(true);
    this.performanceService.getAppraisals().subscribe({
      next: (a) => { this.myAppraisals.set(a); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.performanceService.getAppraisals({ role: 'manager' }).subscribe({
      next: (a) => this.teamAppraisals.set(a), error: () => {},
    });
  }

  updateAppraisal(id: string, data: Partial<Appraisal>): void {
    this.performanceService.updateAppraisal(id, data).subscribe({
      next: () => { this.snackBar.open('Updated', 'Close', { duration: 2000 }); this.loadData(); },
      error: () => this.snackBar.open('Failed', 'Close', { duration: 2000 }),
    });
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = {
      pending: 'bg-amber-100 text-amber-800', in_progress: 'bg-blue-100 text-blue-800',
      completed: 'bg-green-100 text-green-800', appealed: 'bg-red-100 text-red-800',
    };
    return c[status] || 'bg-gray-100 text-gray-800';
  }
}
