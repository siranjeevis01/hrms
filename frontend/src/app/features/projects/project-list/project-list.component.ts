import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgClass, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ProjectsService } from '../projects.service';
import { Project, ProjectStatus } from '../projects.models';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatProgressBarModule,
    MatChipsModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    NgClass,
    TitleCasePipe,
  ],
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.scss',
})
export class ProjectListComponent implements OnInit {
  private projectsService = inject(ProjectsService);
  private router = inject(Router);

  projects = signal<Project[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);
  searchQuery = signal('');
  statusFilter = signal<ProjectStatus | ''>('');
  filteredProjects = signal<Project[]>([]);

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.loading.set(true);
    this.error.set(null);
    this.projectsService
      .getProjects(this.searchQuery(), this.statusFilter() || undefined)
      .subscribe({
        next: (projects) => {
          this.projects.set(projects);
          this.filteredProjects.set(projects);
          this.loading.set(false);
        },
        error: () => {
          this.error.set('Failed to load projects');
          this.loading.set(false);
        },
      });
  }

  onSearch(): void {
    this.loadProjects();
  }

  onStatusFilter(): void {
    this.loadProjects();
  }

  navigateToCreate(): void {
    this.router.navigate(['/projects/create']);
  }

  navigateToProject(id: string): void {
    this.router.navigate(['/projects', id]);
  }

  getStatusColor(status: ProjectStatus): string {
    const colors: Record<ProjectStatus, string> = {
      planning: 'bg-blue-100 text-blue-800',
      active: 'bg-green-100 text-green-800',
      on_hold: 'bg-yellow-100 text-yellow-800',
      completed: 'bg-purple-100 text-purple-800',
      cancelled: 'bg-red-100 text-red-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  getProgressColor(progress: number): string {
    if (progress >= 75) return 'accent';
    if (progress >= 40) return 'primary';
    return 'warn';
  }

  formatBudget(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      maximumFractionDigits: 0,
    }).format(amount);
  }
}
