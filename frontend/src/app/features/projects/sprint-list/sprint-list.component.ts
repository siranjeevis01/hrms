import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgClass, TitleCasePipe, DatePipe, DecimalPipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { ProjectsService } from '../projects.service';
import { Sprint } from '../projects.models';

@Component({
  selector: 'app-sprint-list',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatProgressBarModule,
    MatMenuModule,
    MatDialogModule,
    NgClass,
    TitleCasePipe,
    DatePipe,
    DecimalPipe,
  ],
  templateUrl: './sprint-list.component.html',
  styleUrl: './sprint-list.component.scss',
})
export class SprintListComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private projectsService = inject(ProjectsService);

  projectId = '';
  sprints = signal<Sprint[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.loadSprints();
  }

  loadSprints(): void {
    this.loading.set(true);
    this.projectsService.getSprints(this.projectId).subscribe({
      next: (sprints) => {
        this.sprints.set(sprints);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  startSprint(sprintId: string): void {
    this.projectsService.startSprint(this.projectId, sprintId).subscribe({
      next: () => this.loadSprints(),
    });
  }

  completeSprint(sprintId: string): void {
    this.projectsService.completeSprint(this.projectId, sprintId).subscribe({
      next: () => this.loadSprints(),
    });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      planned: 'bg-blue-100 text-blue-800',
      active: 'bg-green-100 text-green-800',
      completed: 'bg-gray-100 text-gray-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  getProgressColor(progress: number): string {
    if (progress >= 75) return 'accent';
    if (progress >= 40) return 'primary';
    return 'warn';
  }

  navigateToSprint(sprintId: string): void {
    this.router.navigate(['/projects', this.projectId, 'sprints', sprintId]);
  }

  goBack(): void {
    this.router.navigate(['/projects', this.projectId]);
  }
}
