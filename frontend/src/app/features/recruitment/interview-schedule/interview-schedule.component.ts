import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Interview, Candidate, Job, InterviewStatus, InterviewType } from '../recruitment.models';

@Component({
  selector: 'app-interview-schedule',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatTabsModule,
    MatChipsModule, MatProgressSpinnerModule, MatSnackBarModule, MatMenuModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './interview-schedule.component.html',
  styleUrl: './interview-schedule.component.scss',
})
export class InterviewScheduleComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private snackBar = inject(MatSnackBar);

  interviews = signal<Interview[]>([]);
  candidates = signal<Candidate[]>([]);
  jobs = signal<Job[]>([]);
  loading = signal(true);
  showScheduleForm = signal(false);

  minDate = new Date();
  filterStatus = '';

  interviewTypes: { value: InterviewType; label: string }[] = [
    { value: 'phone', label: 'Phone' },
    { value: 'video', label: 'Video' },
    { value: 'in_person', label: 'In Person' },
    { value: 'technical', label: 'Technical' },
    { value: 'panel', label: 'Panel' },
  ];

  newInterview = {
    candidateId: '', jobId: '', scheduledDate: new Date(),
    duration: 60, type: 'video' as InterviewType, location: '', meetingUrl: '', interviewers: '',
  };

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.recruitmentService.getInterviews().subscribe({
      next: (interviews) => { this.interviews.set(interviews); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.recruitmentService.getCandidates().subscribe({
      next: (candidates) => this.candidates.set(candidates),
      error: () => {},
    });
    this.recruitmentService.getJobs().subscribe({
      next: (jobs) => this.jobs.set(jobs),
      error: () => {},
    });
  }

  scheduleInterview(): void {
    if (!this.newInterview.candidateId || !this.newInterview.jobId) {
      this.snackBar.open('Please select candidate and job', 'Close', { duration: 3000 });
      return;
    }
    const data = {
      ...this.newInterview,
      interviewers: this.newInterview.interviewers.split(',').map(s => s.trim()).filter(Boolean),
    };
    this.recruitmentService.scheduleInterview(data).subscribe({
      next: () => {
        this.snackBar.open('Interview scheduled', 'Close', { duration: 3000 });
        this.showScheduleForm.set(false);
        this.resetForm();
        this.loadData();
      },
      error: () => this.snackBar.open('Failed to schedule', 'Close', { duration: 3000 }),
    });
  }

  updateStatus(id: string, status: string): void {
    this.recruitmentService.updateInterviewStatus(id, status).subscribe({
      next: () => { this.snackBar.open('Status updated', 'Close', { duration: 2000 }); this.loadData(); },
      error: () => this.snackBar.open('Failed to update', 'Close', { duration: 2000 }),
    });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      scheduled: 'bg-blue-100 text-blue-800', completed: 'bg-green-100 text-green-800',
      cancelled: 'bg-red-100 text-red-800', no_show: 'bg-gray-100 text-gray-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  getTypeIcon(type: string): string {
    const icons: Record<string, string> = {
      phone: 'phone', video: 'videocam', in_person: 'person', technical: 'code', panel: 'groups',
    };
    return icons[type] || 'event';
  }

  resetForm(): void {
    this.newInterview = {
      candidateId: '', jobId: '', scheduledDate: new Date(),
      duration: 60, type: 'video', location: '', meetingUrl: '', interviewers: '',
    };
  }

  get filteredInterviews(): Interview[] {
    if (!this.filterStatus) return this.interviews();
    return this.interviews().filter(i => i.status === this.filterStatus);
  }
}
