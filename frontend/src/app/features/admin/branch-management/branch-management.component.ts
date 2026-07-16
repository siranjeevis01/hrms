import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AdminService } from '../admin.service';
import { Branch } from '../admin.models';

@Component({
  selector: 'app-branch-management',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './branch-management.component.html',
  styleUrl: './branch-management.component.scss',
})
export class BranchManagementComponent implements OnInit {
  private adminService = inject(AdminService);
  branches = signal<Branch[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.adminService.getBranches().subscribe({
      next: (b) => { this.branches.set(b); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
