import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { OnboardingChecklist } from '../recruitment.models';

@Component({
  selector: 'app-onboarding',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatCheckboxModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatExpansionModule,
    MatSnackBarModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './onboarding.component.html',
  styleUrl: './onboarding.component.scss',
})
export class OnboardingComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private snackBar = inject(MatSnackBar);

  checklists = signal<OnboardingChecklist[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.loadChecklists();
  }

  loadChecklists(): void {
    this.loading.set(true);
    this.recruitmentService.getOnboardingChecklists().subscribe({
      next: (checklists) => { this.checklists.set(checklists); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  toggleTask(checklistId: string, taskId: string, completed: boolean): void {
    this.recruitmentService.updateOnboardingTask(checklistId, taskId, completed).subscribe({
      next: (updated) => {
        this.checklists.update(lists => lists.map(l => l.id === updated.id ? updated : l));
        this.snackBar.open(completed ? 'Task completed' : 'Task uncompleted', 'Close', { duration: 2000 });
      },
      error: () => this.snackBar.open('Failed to update', 'Close', { duration: 2000 }),
    });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      not_started: 'bg-gray-100 text-gray-800', in_progress: 'bg-blue-100 text-blue-800',
      completed: 'bg-green-100 text-green-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  getCompletedCount(tasks: { completed: boolean }[]): number {
    return tasks.filter(t => t.completed).length;
  }
}
