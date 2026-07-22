import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { NgClass, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { ProjectsService } from '../projects.service';
import { ProjectTask, BoardColumn, TaskStatus, TaskPriority } from '../projects.models';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    DragDropModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatMenuModule,
    MatTooltipModule,
    MatDialogModule,
    MatSelectModule,
    NgClass,
    TitleCasePipe,
  ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss',
})
export class BoardComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private projectsService = inject(ProjectsService);

  projectId = '';
  columns = signal<BoardColumn[]>([]);
  loading = signal(true);
  assigneeFilter = signal<string>('');
  priorityFilter = signal<TaskPriority | ''>('');
  selectedTask = signal<ProjectTask | null>(null);
  showTaskDetail = signal(false);

  statuses: TaskStatus[] = ['todo', 'in_progress', 'in_review', 'done'];

  priorityColors: Record<TaskPriority, string> = {
    low: 'bg-gray-100 text-gray-700',
    medium: 'bg-blue-100 text-blue-700',
    high: 'bg-orange-100 text-orange-700',
    critical: 'bg-red-100 text-red-700',
  };

  columnColors: Record<string, string> = {
    todo: 'border-t-gray-400',
    in_progress: 'border-t-blue-500',
    in_review: 'border-t-yellow-500',
    done: 'border-t-green-500',
  };

  columnIcons: Record<string, string> = {
    todo: 'radio_button_unchecked',
    in_progress: 'timelapse',
    in_review: 'rate_review',
    done: 'check_circle',
  };

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.loadBoard();
  }

  loadBoard(): void {
    this.loading.set(true);
    this.projectsService.getBoard(this.projectId).subscribe({
      next: (columns) => {
        this.columns.set(columns);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  onDrop(event: CdkDragDrop<ProjectTask[]>): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
      const task = event.container.data[event.currentIndex];
      const newStatus = event.container.id as TaskStatus;
      this.projectsService
        .updateTaskStatus(this.projectId, { taskId: task.id, status: newStatus, position: event.currentIndex })
        .subscribe();
    }
  }

  openTaskDetail(task: ProjectTask): void {
    this.selectedTask.set(task);
    this.showTaskDetail.set(true);
  }

  closeTaskDetail(): void {
    this.showTaskDetail.set(false);
    this.selectedTask.set(null);
  }

  getPriorityColor(priority: TaskPriority): string {
    return this.priorityColors[priority] || 'bg-gray-100 text-gray-700';
  }

  getConnectedLists(): string[] {
    return this.statuses;
  }

  goBack(): void {
    this.router.navigate(['/projects', this.projectId]);
  }

  getStatusLabel(status: TaskStatus): string {
    const labels: Record<TaskStatus, string> = {
      todo: 'To Do',
      in_progress: 'In Progress',
      in_review: 'In Review',
      done: 'Done',
    };
    return labels[status];
  }

  getTaskTypeIcon(type: string): string {
    const icons: Record<string, string> = {
      story: 'auto_stories',
      task: 'check_circle',
      bug: 'bug_report',
      epic: 'star',
    };
    return icons[type] || 'article';
  }
}
