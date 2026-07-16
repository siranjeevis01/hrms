import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Candidate, Application, Interview } from '../recruitment.models';

@Component({
  selector: 'app-candidate-detail',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatTabsModule, MatDividerModule,
    MatProgressSpinnerModule, MatChipsModule, MatListModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './candidate-detail.component.html',
  styleUrl: './candidate-detail.component.scss',
})
export class CandidateDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private recruitmentService = inject(RecruitmentService);

  candidate = signal<Candidate | null>(null);
  applications = signal<Application[]>([]);
  interviews = signal<Interview[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadCandidate(id);
  }

  loadCandidate(id: string): void {
    this.loading.set(true);
    this.recruitmentService.getCandidate(id).subscribe({
      next: (candidate) => {
        this.candidate.set(candidate);
        this.loadApplications(id);
        this.loadInterviews(id);
      },
      error: () => this.loading.set(false),
    });
  }

  private loadApplications(candidateId: string): void {
    this.recruitmentService.getApplications().subscribe({
      next: (apps) => {
        this.applications.set(apps.filter(a => a.candidateId === candidateId));
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  private loadInterviews(candidateId: string): void {
    this.recruitmentService.getInterviews({ candidateId }).subscribe({
      next: (interviews) => this.interviews.set(interviews),
      error: () => {},
    });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      applied: 'bg-blue-100 text-blue-800', screening: 'bg-yellow-100 text-yellow-800',
      shortlisted: 'bg-purple-100 text-purple-800', interview: 'bg-indigo-100 text-indigo-800',
      offered: 'bg-orange-100 text-orange-800', hired: 'bg-green-100 text-green-800',
      rejected: 'bg-red-100 text-red-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  getInterviewStatusColor(status: string): string {
    const colors: Record<string, string> = {
      scheduled: 'bg-blue-100 text-blue-800', completed: 'bg-green-100 text-green-800',
      cancelled: 'bg-red-100 text-red-800', no_show: 'bg-gray-100 text-gray-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  goBack(): void {
    this.router.navigate(['/recruitment/candidates']);
  }
}
