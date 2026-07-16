import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NgClass, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { ProjectsService } from '../projects.service';
import { Project, ProjectStats } from '../projects.models';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    RouterOutlet,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatProgressBarModule,
    MatChipsModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatDialogModule,
    NgClass,
    TitleCasePipe,
  ],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.scss',
})
export class ProjectDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  protected router = inject(Router);
  private projectsService = inject(ProjectsService);

  project = signal<Project | null>(null);
  stats = signal<ProjectStats | null>(null);
  loading = signal(true);
  projectId = '';

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.loadProject();
  }

  loadProject(): void {
    this.loading.set(true);
    this.projectsService.getProject(this.projectId).subscribe({
      next: (project) => {
        this.project.set(project);
        this.loading.set(false);
        this.loadStats();
      },
      error: () => this.loading.set(false),
    });
  }

  loadStats(): void {
    this.projectsService.getProjectStats(this.projectId).subscribe({
      next: (stats) => this.stats.set(stats),
      error: () => {},
    });
  }

  navigateTo(path: string): void {
    this.router.navigate([path], { relativeTo: this.route });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      planning: 'bg-blue-100 text-blue-800',
      active: 'bg-green-100 text-green-800',
      on_hold: 'bg-yellow-100 text-yellow-800',
      completed: 'bg-purple-100 text-purple-800',
      cancelled: 'bg-red-100 text-red-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  get progressColor(): string {
    const progress = this.project()?.progress || 0;
    if (progress >= 75) return 'accent';
    if (progress >= 40) return 'primary';
    return 'warn';
  }

  openAddTaskDialog(): void {
    // Placeholder for dialog
  }

  openLogTimeDialog(): void {
    // Placeholder for dialog
  }
}
