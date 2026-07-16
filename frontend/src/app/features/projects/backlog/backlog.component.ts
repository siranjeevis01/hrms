import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CdkDragDrop, DragDropModule, transferArrayItem } from '@angular/cdk/drag-drop';
import { NgClass, TitleCasePipe, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { ProjectsService } from '../projects.service';
import { ProjectTask, TaskPriority, Sprint } from '../projects.models';

@Component({
  selector: 'app-backlog',
  standalone: true,
  imports: [
    DragDropModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatMenuModule,
    MatSelectModule,
    MatFormFieldModule,
    NgClass,
    TitleCasePipe,
    DatePipe,
  ],
  templateUrl: './backlog.component.html',
  styleUrl: './backlog.component.scss',
})
export class BacklogComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private projectsService = inject(ProjectsService);

  projectId = '';
  backlogItems = signal<ProjectTask[]>([]);
  sprints = signal<Sprint[]>([]);
  loading = signal(true);
  selectedSprintFilter = signal<string>('');

  priorityColors: Record<TaskPriority, string> = {
    low: 'bg-gray-100 text-gray-700',
    medium: 'bg-blue-100 text-blue-700',
    high: 'bg-orange-100 text-orange-700',
    critical: 'bg-red-100 text-red-700',
  };

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    Promise.all([
      this.projectsService.getBacklog(this.projectId).toPromise(),
      this.projectsService.getSprints(this.projectId).toPromise(),
    ]).then(([backlog, sprints]) => {
      this.backlogItems.set(backlog || []);
      this.sprints.set(sprints || []);
      this.loading.set(false);
    }).catch(() => this.loading.set(false));
  }

  onDrop(event: CdkDragDrop<ProjectTask[]>): void {
    if (event.previousContainer !== event.container) {
      transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
      const task = event.container.data[event.currentIndex];
      const sprintId = event.container.id;
      this.projectsService
        .updateTask(task.id, { sprintId })
        .subscribe();
    }
  }

  getPriorityColor(priority: TaskPriority): string {
    return this.priorityColors[priority] || 'bg-gray-100 text-gray-700';
  }

  getTaskTypeLabel(type: string): string {
    const labels: Record<string, string> = { story: 'US', task: 'TSK', bug: 'BUG', epic: 'EPIC' };
    return labels[type] || 'TSK';
  }

  goBack(): void {
    this.router.navigate(['/projects', this.projectId]);
  }
}
