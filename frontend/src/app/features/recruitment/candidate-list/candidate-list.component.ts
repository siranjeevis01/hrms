import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMenuModule } from '@angular/material/menu';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Candidate, CandidateFilters, ApplicationStatus, CandidateSource } from '../recruitment.models';

@Component({
  selector: 'app-candidate-list',
  standalone: true,
  imports: [
    FormsModule, MatTableModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatProgressSpinnerModule, MatMenuModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './candidate-list.component.html',
  styleUrl: './candidate-list.component.scss',
})
export class CandidateListComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private router = inject(Router);

  candidates = signal<Candidate[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);

  searchQuery = '';
  selectedStatus = '';
  selectedSource = '';
  displayedColumns = ['name', 'email', 'phone', 'source', 'status', 'appliedDate', 'actions'];

  statuses: { value: ApplicationStatus; label: string }[] = [
    { value: 'applied', label: 'Applied' },
    { value: 'screening', label: 'Screening' },
    { value: 'shortlisted', label: 'Shortlisted' },
    { value: 'interview', label: 'Interview' },
    { value: 'offered', label: 'Offered' },
    { value: 'hired', label: 'Hired' },
    { value: 'rejected', label: 'Rejected' },
  ];

  sources: { value: CandidateSource; label: string }[] = [
    { value: 'job_board', label: 'Job Board' },
    { value: 'referral', label: 'Referral' },
    { value: 'company_website', label: 'Company Website' },
    { value: 'linkedin', label: 'LinkedIn' },
    { value: 'agency', label: 'Agency' },
    { value: 'campus', label: 'Campus' },
    { value: 'other', label: 'Other' },
  ];

  filteredCandidates = computed(() => {
    let result = this.candidates();
    if (this.searchQuery) {
      const q = this.searchQuery.toLowerCase();
      result = result.filter(c =>
        `${c.firstName} ${c.lastName}`.toLowerCase().includes(q) ||
        c.email.toLowerCase().includes(q) ||
        c.phone.includes(q)
      );
    }
    if (this.selectedStatus) result = result.filter(c => c.status === this.selectedStatus);
    if (this.selectedSource) result = result.filter(c => c.source === this.selectedSource);
    return result;
  });

  ngOnInit(): void {
    this.loadCandidates();
  }

  loadCandidates(): void {
    this.loading.set(true);
    this.recruitmentService.getCandidates().subscribe({
      next: (candidates) => { this.candidates.set(candidates); this.loading.set(false); },
      error: () => { this.error.set('Failed to load candidates'); this.loading.set(false); },
    });
  }

  viewCandidate(id: string): void {
    this.router.navigate(['/recruitment/candidates', id]);
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

  getSourceLabel(source: string): string {
    return this.sources.find(s => s.value === source)?.label || source;
  }

  clearFilters(): void {
    this.searchQuery = '';
    this.selectedStatus = '';
    this.selectedSource = '';
  }
}
