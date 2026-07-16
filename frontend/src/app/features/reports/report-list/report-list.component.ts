import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { NgClass, TitleCasePipe, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReportsService } from '../reports.service';
import { ReportTemplate, ReportInstance } from '../reports.models';

@Component({
  selector: 'app-report-list',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatProgressSpinnerModule, NgClass, TitleCasePipe, DatePipe],
  templateUrl: './report-list.component.html',
  styleUrl: './report-list.component.scss',
})
export class ReportListComponent implements OnInit {
  private reportsService = inject(ReportsService);
  protected router = inject(Router);

  templates = signal<ReportTemplate[]>([]);
  recentReports = signal<ReportInstance[]>([]);
  loading = signal(true);
  selectedCategory = signal('');

  categoryIcons: Record<string, string> = {
    hr: 'people', payroll: 'payments', attendance: 'fact_check', leave: 'event_busy',
    recruitment: 'work', expense: 'receipt_long', travel: 'flight', project: 'folder',
  };

  categoryColors: Record<string, string> = {
    hr: 'bg-blue-50 text-blue-600', payroll: 'bg-green-50 text-green-600',
    attendance: 'bg-yellow-50 text-yellow-600', leave: 'bg-red-50 text-red-600',
    recruitment: 'bg-purple-50 text-purple-600', expense: 'bg-orange-50 text-orange-600',
    travel: 'bg-cyan-50 text-cyan-600', project: 'bg-pink-50 text-pink-600',
  };

  ngOnInit(): void {
    this.reportsService.getTemplates().subscribe({
      next: (t) => { this.templates.set(t); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.reportsService.getInstances().subscribe({
      next: (r) => this.recentReports.set(r.slice(0, 5)),
    });
  }

  navigateToGenerate(templateId: string): void { this.router.navigate(['/reports/generate', templateId]); }

  get filteredTemplates(): ReportTemplate[] {
    if (!this.selectedCategory()) return this.templates();
    return this.templates().filter((t) => t.category === this.selectedCategory());
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = { pending: 'bg-yellow-100 text-yellow-700', processing: 'bg-blue-100 text-blue-700', completed: 'bg-green-100 text-green-700', failed: 'bg-red-100 text-red-700' };
    return c[status] || 'bg-gray-100 text-gray-700';
  }
}
