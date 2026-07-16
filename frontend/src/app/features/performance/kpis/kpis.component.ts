import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PerformanceService } from '../performance.service';
import { KPI } from '../performance.models';

@Component({
  selector: 'app-kpis',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatProgressSpinnerModule, MatSnackBarModule,
  ],
  templateUrl: './kpis.component.html',
  styleUrl: './kpis.component.scss',
})
export class KPIComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  kpis = signal<KPI[]>([]);
  loading = signal(true);
  showCreateForm = signal(false);
  filterStatus = '';

  newKpi = { name: '', metric: '', target: 0, current: 0, status: 'active' as const };

  ngOnInit(): void { this.loadKPIs(); }

  loadKPIs(): void {
    this.loading.set(true);
    this.performanceService.getKPIs().subscribe({
      next: (kpis) => { this.kpis.set(kpis); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  createKPI(): void {
    if (!this.newKpi.name) return;
    this.performanceService.createKPI(this.newKpi).subscribe({
      next: () => { this.snackBar.open('KPI created', 'Close', { duration: 3000 }); this.showCreateForm.set(false); this.loadKPIs(); this.resetForm(); },
      error: () => this.snackBar.open('Failed', 'Close', { duration: 3000 }),
    });
  }

  resetForm(): void { this.newKpi = { name: '', metric: '', target: 0, current: 0, status: 'active' }; }

  getScoreColor(score: number): string {
    if (score >= 80) return 'text-green-600';
    if (score >= 60) return 'text-amber-600';
    return 'text-red-600';
  }

  get filteredKPIs(): KPI[] {
    if (!this.filterStatus) return this.kpis();
    return this.kpis().filter(k => k.status === this.filterStatus);
  }
}
