import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { DatePipe, TitleCasePipe, DecimalPipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Job } from '../recruitment.models';

@Component({
  selector: 'app-job-detail',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatChipsModule,
    MatProgressSpinnerModule, MatSnackBarModule, MatMenuModule,
    MatDividerModule, DatePipe, TitleCasePipe, DecimalPipe,
  ],
  templateUrl: './job-detail.component.html',
  styleUrl: './job-detail.component.scss',
})
export class JobDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private recruitmentService = inject(RecruitmentService);
  private snackBar = inject(MatSnackBar);

  job = signal<Job | null>(null);
  loading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadJob(id);
  }

  loadJob(id: string): void {
    this.loading.set(true);
    this.recruitmentService.getJob(id).subscribe({
      next: (job) => { this.job.set(job); this.loading.set(false); },
      error: () => { this.error.set('Failed to load job'); this.loading.set(false); },
    });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      draft: 'bg-gray-100 text-gray-800', published: 'bg-green-100 text-green-800',
      on_hold: 'bg-yellow-100 text-yellow-800', closed: 'bg-red-100 text-red-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  updateStatus(status: string): void {
    const j = this.job();
    if (!j) return;
    this.recruitmentService.updateJobStatus(j.id, status).subscribe({
      next: (updated) => {
        this.job.set(updated);
        this.snackBar.open(`Job ${status.replace('_', ' ')}`, 'Close', { duration: 3000 });
      },
      error: () => this.snackBar.open('Failed to update status', 'Close', { duration: 3000 }),
    });
  }

  navigateToApplications(): void {
    const j = this.job();
    if (j) this.router.navigate(['/recruitment/jobs', j.id, 'applications']);
  }

  goBack(): void {
    this.router.navigate(['/recruitment/jobs']);
  }
}
