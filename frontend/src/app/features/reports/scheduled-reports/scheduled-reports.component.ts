import { Component, OnInit, inject, signal } from '@angular/core';
import { NgClass, TitleCasePipe, UpperCasePipe, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReportsService } from '../reports.service';
import { ScheduledReport } from '../reports.models';

@Component({
  selector: 'app-scheduled-reports',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatSlideToggleModule, MatProgressSpinnerModule, NgClass, TitleCasePipe, UpperCasePipe, DatePipe],
  templateUrl: './scheduled-reports.component.html',
  styleUrl: './scheduled-reports.component.scss',
})
export class ScheduledReportsComponent implements OnInit {
  private reportsService = inject(ReportsService);
  scheduled = signal<ScheduledReport[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.reportsService.getScheduledReports().subscribe({
      next: (s) => { this.scheduled.set(s); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  toggleActive(id: string, isActive: boolean): void {
    this.reportsService.toggleScheduledReport(id, isActive).subscribe();
  }

  deleteReport(id: string): void {
    this.reportsService.deleteScheduledReport(id).subscribe({ next: () => {
      this.scheduled.update((s) => s.filter((r) => r.id !== id));
    }});
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = { daily: 'bg-blue-100 text-blue-700', weekly: 'bg-green-100 text-green-700', monthly: 'bg-purple-100 text-purple-700', quarterly: 'bg-orange-100 text-orange-700' };
    return c[status] || 'bg-gray-100 text-gray-700';
  }
}
