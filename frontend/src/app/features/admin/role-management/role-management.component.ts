import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { AdminService } from '../admin.service';
import { Role } from '../admin.models';

@Component({
  selector: 'app-role-management',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatMenuModule],
  templateUrl: './role-management.component.html',
  styleUrl: './role-management.component.scss',
})
export class RoleManagementComponent implements OnInit {
  private adminService = inject(AdminService);
  roles = signal<Role[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.adminService.getRoles().subscribe({
      next: (r) => { this.roles.set(r); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
