import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { OKRCycle } from '../performance.models';

@Component({
  selector: 'app-okrs',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatDatepickerModule, MatNativeDateModule, MatTabsModule, MatProgressBarModule,
    MatProgressSpinnerModule, MatSnackBarModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './okrs.component.html',
  styleUrl: './okrs.component.scss',
})
export class OKRComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  cycles = signal<OKRCycle[]>([]);
  loading = signal(true);
  showCreateForm = signal(false);
  minDate = new Date();

  newCycle = { name: '', startDate: new Date(), endDate: new Date() };

  ngOnInit(): void { this.loadCycles(); }

  loadCycles(): void {
    this.loading.set(true);
    this.performanceService.getOKRCycles().subscribe({
      next: (cycles) => { this.cycles.set(cycles); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  createCycle(): void {
    if (!this.newCycle.name) return;
    this.performanceService.createOKRCycle(this.newCycle).subscribe({
      next: () => { this.snackBar.open('Cycle created', 'Close', { duration: 3000 }); this.showCreateForm.set(false); this.loadCycles(); },
      error: () => this.snackBar.open('Failed to create', 'Close', { duration: 3000 }),
    });
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = { active: 'bg-green-100 text-green-800', completed: 'bg-gray-100 text-gray-800', upcoming: 'bg-blue-100 text-blue-800' };
    return c[status] || 'bg-gray-100 text-gray-800';
  }
}
