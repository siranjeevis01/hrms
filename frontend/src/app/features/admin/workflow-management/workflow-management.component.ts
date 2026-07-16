import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { AdminService } from '../admin.service';
import { WorkflowTemplate } from '../admin.models';

@Component({
  selector: 'app-workflow-management',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './workflow-management.component.html',
  styleUrl: './workflow-management.component.scss',
})
export class WorkflowManagementComponent implements OnInit {
  private adminService = inject(AdminService);
  workflows = signal<WorkflowTemplate[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.adminService.getWorkflows().subscribe({
      next: (w) => { this.workflows.set(w); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
