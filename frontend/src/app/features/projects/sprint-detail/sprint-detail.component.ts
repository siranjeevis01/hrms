import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgClass, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProjectsService } from '../projects.service';
import { Sprint, ProjectTask, TaskPriority } from '../projects.models';

@Component({
  selector: 'app-sprint-detail',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    NgClass,
    TitleCasePipe,
  ],
  templateUrl: './sprint-detail.component.html',
  styleUrl: './sprint-detail.component.scss',
})
export class SprintDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private projectsService = inject(ProjectsService);

  projectId = '';
  sprintId = '';
  sprint = signal<Sprint | null>(null);
  loading = signal(true);

  priorityColors: Record<TaskPriority, string> = {
    low: 'bg-gray-100 text-gray-700',
    medium: 'bg-blue-100 text-blue-700',
    high: 'bg-orange-100 text-orange-700',
    critical: 'bg-red-100 text-red-700',
  };

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.sprintId = this.route.snapshot.paramMap.get('sprintId') || '';
    this.loadSprint();
  }

  loadSprint(): void {
    this.loading.set(true);
    this.projectsService.getSprint(this.projectId, this.sprintId).subscribe({
      next: (sprint) => {
        this.sprint.set(sprint);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  getPriorityColor(priority: TaskPriority): string {
    return this.priorityColors[priority] || 'bg-gray-100 text-gray-700';
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      todo: 'bg-gray-100 text-gray-700',
      in_progress: 'bg-blue-100 text-blue-700',
      in_review: 'bg-yellow-100 text-yellow-700',
      done: 'bg-green-100 text-green-700',
    };
    return colors[status] || 'bg-gray-100 text-gray-700';
  }

  goBack(): void {
    this.router.navigate(['/projects', this.projectId, 'sprints']);
  }
}
