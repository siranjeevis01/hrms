import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Application, ApplicationStatus } from '../recruitment.models';

interface KanbanColumn {
  status: ApplicationStatus;
  label: string;
  color: string;
  applications: Application[];
}

@Component({
  selector: 'app-job-applications',
  standalone: true,
  imports: [
    DragDropModule, MatCardModule, MatButtonModule, MatIconModule,
    MatProgressSpinnerModule, MatChipsModule, MatSnackBarModule, DatePipe,
  ],
  templateUrl: './job-applications.component.html',
  styleUrl: './job-applications.component.scss',
})
export class JobApplicationsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private recruitmentService = inject(RecruitmentService);
  private snackBar = inject(MatSnackBar);

  jobId = signal<string>('');
  loading = signal(true);
  columns = signal<KanbanColumn[]>([]);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.jobId.set(id);
      this.loadApplications(id);
    }
  }

  loadApplications(jobId: string): void {
    this.loading.set(true);
    this.recruitmentService.getApplications(jobId).subscribe({
      next: (applications) => {
        this.buildColumns(applications);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  private buildColumns(applications: Application[]): void {
    const stageConfig: { status: ApplicationStatus; label: string; color: string }[] = [
      { status: 'applied', label: 'Applied', color: '#3b82f6' },
      { status: 'screening', label: 'Screening', color: '#8b5cf6' },
      { status: 'shortlisted', label: 'Shortlisted', color: '#f59e0b' },
      { status: 'interview', label: 'Interview', color: '#06b6d4' },
      { status: 'offered', label: 'Offered', color: '#10b981' },
      { status: 'hired', label: 'Hired', color: '#059669' },
      { status: 'rejected', label: 'Rejected', color: '#ef4444' },
    ];

    this.columns.set(stageConfig.map(config => ({
      ...config,
      applications: applications.filter(a => a.status === config.status),
    })));
  }

  onDrop(event: CdkDragDrop<Application[]>): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const app = event.previousContainer.data[event.previousIndex];
      const newStatus = this.columns().find(c => c.applications === event.container.data)?.status;
      if (newStatus) {
        transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
        this.recruitmentService.updateApplicationStatus(app.id, newStatus).subscribe({
          next: () => this.snackBar.open('Status updated', 'Close', { duration: 2000 }),
          error: () => {
            transferArrayItem(event.container.data, event.previousContainer.data, event.currentIndex, event.previousIndex);
            this.snackBar.open('Failed to update status', 'Close', { duration: 2000 });
          },
        });
      }
    }
  }

  viewCandidate(candidateId: string): void {
    this.router.navigate(['/recruitment/candidates', candidateId]);
  }

  goBack(): void {
    this.router.navigate(['/recruitment/jobs', this.jobId()]);
  }
}
