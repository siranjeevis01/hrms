import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TrainingService } from '../training.service';
import { Assessment } from '../training.models';

@Component({
  selector: 'app-assessment-list',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatTabsModule, MatProgressSpinnerModule, MatSnackBarModule],
  templateUrl: './assessment-list.component.html',
  styleUrl: './assessment-list.component.scss',
})
export class AssessmentListComponent implements OnInit {
  private trainingService = inject(TrainingService);
  private snackBar = inject(MatSnackBar);

  available = signal<Assessment[]>([]);
  completed = signal<Assessment[]>([]);
  loading = signal(true);

  ngOnInit(): void { this.loadData(); }

  loadData(): void {
    this.loading.set(true);
    this.trainingService.getAssessments({ status: 'available' }).subscribe({
      next: (a) => { this.available.set(a); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.trainingService.getAssessments({ status: 'completed' }).subscribe({
      next: (a) => this.completed.set(a), error: () => {},
    });
  }

  startAssessment(id: string): void {
    this.trainingService.startAssessment(id).subscribe({
      next: () => this.snackBar.open('Assessment started', 'Close', { duration: 3000 }),
      error: () => this.snackBar.open('Failed to start', 'Close', { duration: 3000 }),
    });
  }
}
