import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TrainingService } from '../training.service';
import { LearningPath } from '../training.models';

@Component({
  selector: 'app-learning-paths',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatProgressBarModule, MatProgressSpinnerModule, MatSnackBarModule],
  templateUrl: './learning-paths.component.html',
  styleUrl: './learning-paths.component.scss',
})
export class LearningPathsComponent implements OnInit {
  private trainingService = inject(TrainingService);
  private snackBar = inject(MatSnackBar);

  paths = signal<LearningPath[]>([]);
  loading = signal(true);
  selectedPath = signal<LearningPath | null>(null);

  ngOnInit(): void { this.loadPaths(); }

  loadPaths(): void {
    this.loading.set(true);
    this.trainingService.getLearningPaths().subscribe({
      next: (paths) => { this.paths.set(paths); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  selectPath(path: LearningPath): void {
    this.selectedPath.set(path);
  }

  enroll(pathId: string): void {
    this.trainingService.enrollInPath(pathId).subscribe({
      next: () => { this.snackBar.open('Enrolled', 'Close', { duration: 3000 }); this.loadPaths(); },
      error: () => this.snackBar.open('Failed', 'Close', { duration: 3000 }),
    });
  }
}
