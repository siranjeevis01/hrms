import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRippleModule } from '@angular/material/core';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Job, JobFilters, JobStatus, EmploymentType } from '../recruitment.models';

@Component({
  selector: 'app-job-listings',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatChipsModule, MatProgressSpinnerModule,
    MatButtonToggleModule, MatRippleModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './job-listings.component.html',
  styleUrl: './job-listings.component.scss',
})
export class JobListingsComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private router = inject(Router);

  jobs = signal<Job[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);
  viewMode = signal<'grid' | 'list'>('grid');

  filters = signal<JobFilters>({});
  searchQuery = '';
  selectedDepartment = '';
  selectedStatus = '';
  selectedEmploymentType = '';

  departments = signal<string[]>([]);
  statuses: { value: JobStatus; label: string }[] = [
    { value: 'draft', label: 'Draft' },
    { value: 'published', label: 'Published' },
    { value: 'on_hold', label: 'On Hold' },
    { value: 'closed', label: 'Closed' },
  ];
  employmentTypes: { value: EmploymentType; label: string }[] = [
    { value: 'full_time', label: 'Full Time' },
    { value: 'part_time', label: 'Part Time' },
    { value: 'contract', label: 'Contract' },
    { value: 'internship', label: 'Internship' },
    { value: 'freelance', label: 'Freelance' },
  ];

  filteredJobs = computed(() => {
    let result = this.jobs();
    if (this.searchQuery) {
      const q = this.searchQuery.toLowerCase();
      result = result.filter(j => j.title.toLowerCase().includes(q) || j.department.toLowerCase().includes(q));
    }
    if (this.selectedDepartment) result = result.filter(j => j.department === this.selectedDepartment);
    if (this.selectedStatus) result = result.filter(j => j.status === this.selectedStatus);
    if (this.selectedEmploymentType) result = result.filter(j => j.employmentType === this.selectedEmploymentType);
    return result;
  });

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.loading.set(true);
    this.recruitmentService.getJobs().subscribe({
      next: (jobs) => {
        this.jobs.set(jobs);
        const depts = [...new Set(jobs.map(j => j.department))];
        this.departments.set(depts);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load jobs');
        this.loading.set(false);
      },
    });
  }

  navigateTo(path: string): void {
    this.router.navigate(['/recruitment', path]);
  }

  getStatusColor(status: JobStatus): string {
    const colors: Record<string, string> = {
      draft: 'bg-gray-100 text-gray-800', published: 'bg-green-100 text-green-800',
      on_hold: 'bg-yellow-100 text-yellow-800', closed: 'bg-red-100 text-red-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  getEmploymentTypeLabel(type: EmploymentType): string {
    return this.employmentTypes.find(t => t.value === type)?.label || type;
  }

  clearFilters(): void {
    this.searchQuery = '';
    this.selectedDepartment = '';
    this.selectedStatus = '';
    this.selectedEmploymentType = '';
  }
}
