import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe } from '@angular/common';
import { PerformanceService } from '../performance.service';
import { FeedbackRequest } from '../performance.models';

@Component({
  selector: 'app-feedback360',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatTabsModule, MatProgressSpinnerModule, MatSnackBarModule, DatePipe,
  ],
  templateUrl: './feedback360.component.html',
  styleUrl: './feedback360.component.scss',
})
export class Feedback360Component implements OnInit {
  private performanceService = inject(PerformanceService);
  private snackBar = inject(MatSnackBar);

  pendingRequests = signal<FeedbackRequest[]>([]);
  givenFeedback = signal<FeedbackRequest[]>([]);
  receivedFeedback = signal<FeedbackRequest[]>([]);
  loading = signal(true);
  showRequestForm = signal(false);

  newRequest = { targetId: '', questions: [
    { question: 'What are this person\'s strengths?', answer: '' },
    { question: 'What areas can they improve?', answer: '' },
    { question: 'How effective is their communication?', answer: '' },
  ] };
  answers: { question: string; answer: string }[] = [];

  ngOnInit(): void { this.loadData(); }

  loadData(): void {
    this.loading.set(true);
    this.performanceService.getFeedbackRequests({ status: 'pending' }).subscribe({
      next: (r) => { this.pendingRequests.set(r); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.performanceService.getFeedbackRequests({ status: 'submitted' }).subscribe({
      next: (r) => this.givenFeedback.set(r), error: () => {},
    });
  }

  requestFeedback(): void {
    this.performanceService.requestFeedback(this.newRequest).subscribe({
      next: () => { this.snackBar.open('Feedback requested', 'Close', { duration: 3000 }); this.showRequestForm.set(false); this.loadData(); },
      error: () => this.snackBar.open('Failed', 'Close', { duration: 3000 }),
    });
  }

  startFeedback(request: FeedbackRequest): void {
    this.answers = request.questions.map(q => ({ question: q.question, answer: '' }));
  }

  submitFeedback(id: string): void {
    this.performanceService.submitFeedback(id, this.answers).subscribe({
      next: () => { this.snackBar.open('Feedback submitted', 'Close', { duration: 3000 }); this.loadData(); this.answers = []; },
      error: () => this.snackBar.open('Failed', 'Close', { duration: 3000 }),
    });
  }

  getStatusColor(status: string): string {
    return status === 'pending' ? 'bg-amber-100 text-amber-800' : 'bg-green-100 text-green-800';
  }
}
