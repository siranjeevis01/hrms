import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { CdkDragDrop, DragDropModule, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Application, ApplicationStatus } from '../recruitment.models';

interface PipelineColumn {
  status: ApplicationStatus;
  label: string;
  color: string;
  applications: Application[];
}

@Component({
  selector: 'app-recruitment-pipeline',
  standalone: true,
  imports: [
    DragDropModule, FormsModule, MatCardModule, MatButtonModule, MatIconModule,
    MatInputModule, MatProgressSpinnerModule, DatePipe,
  ],
  templateUrl: './recruitment-pipeline.component.html',
  styleUrl: './recruitment-pipeline.component.scss',
})
export class RecruitmentPipelineComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private router = inject(Router);

  loading = signal(true);
  searchQuery = '';
  columns = signal<PipelineColumn[]>([]);

  private stages: { status: ApplicationStatus; label: string; color: string }[] = [
    { status: 'applied', label: 'Applied', color: '#3b82f6' },
    { status: 'screening', label: 'Screening', color: '#8b5cf6' },
    { status: 'shortlisted', label: 'Shortlisted', color: '#f59e0b' },
    { status: 'interview', label: 'Interview', color: '#06b6d4' },
    { status: 'offered', label: 'Offered', color: '#10b981' },
    { status: 'hired', label: 'Hired', color: '#059669' },
  ];

  ngOnInit(): void {
    this.loadPipeline();
  }

  loadPipeline(): void {
    this.loading.set(true);
    this.recruitmentService.getApplications().subscribe({
      next: (applications) => {
        const filtered = this.searchQuery
          ? applications.filter(a =>
              `${a.candidate?.firstName} ${a.candidate?.lastName}`.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
              a.job?.title?.toLowerCase().includes(this.searchQuery.toLowerCase())
            )
          : applications;

        this.columns.set(this.stages.map(s => ({
          ...s,
          applications: filtered.filter(a => a.status === s.status),
        })));
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  onDrop(event: CdkDragDrop<Application[]>): void {
    if (event.previousContainer === event.container) return;
    const app = event.previousContainer.data[event.previousIndex];
    const newStatus = this.columns().find(c => c.applications === event.container.data)?.status;
    if (newStatus) {
      transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
      this.recruitmentService.updateApplicationStatus(app.id, newStatus).subscribe({ error: () => this.loadPipeline() });
    }
  }

  viewCandidate(id: string): void {
    this.router.navigate(['/recruitment/candidates', id]);
  }

  onSearch(): void {
    this.loadPipeline();
  }
}
