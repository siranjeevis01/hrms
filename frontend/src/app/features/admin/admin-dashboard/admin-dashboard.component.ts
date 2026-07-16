import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgClass, DatePipe } from '@angular/common';
import { AdminService } from '../admin.service';
import { AdminDashboardStats } from '../admin.models';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatListModule, MatProgressSpinnerModule, NgClass, DatePipe],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent implements OnInit {
  private adminService = inject(AdminService);
  private router = inject(Router);
  stats = signal<AdminDashboardStats | null>(null);
  loading = signal(true);

  quickLinks = [
    { icon: 'business', label: 'Company Settings', route: 'company', color: 'bg-blue-50 text-blue-600' },
    { icon: 'apartment', label: 'Departments', route: 'departments', color: 'bg-green-50 text-green-600' },
    { icon: 'badge', label: 'Designations', route: 'designations', color: 'bg-yellow-50 text-yellow-600' },
    { icon: 'location_city', label: 'Branches', route: 'branches', color: 'bg-purple-50 text-purple-600' },
    { icon: 'admin_panel_settings', label: 'Roles', route: 'roles', color: 'bg-red-50 text-red-600' },
    { icon: 'lock', label: 'Permissions', route: 'permissions', color: 'bg-orange-50 text-orange-600' },
    { icon: 'history', label: 'Audit Logs', route: 'audit-logs', color: 'bg-gray-50 text-gray-600' },
    { icon: 'flag', label: 'Feature Flags', route: 'feature-flags', color: 'bg-cyan-50 text-cyan-600' },
    { icon: 'account_tree', label: 'Workflows', route: 'workflow', color: 'bg-pink-50 text-pink-600' },
  ];

  ngOnInit(): void {
    this.adminService.getDashboardStats().subscribe({
      next: (stats) => { this.stats.set(stats); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  navigateTo(route: string): void { this.router.navigate(['/admin', route]); }

  getHealthColor(status: string): string {
    const c: Record<string, string> = { healthy: 'text-green-600 bg-green-50', degraded: 'text-yellow-600 bg-yellow-50', down: 'text-red-600 bg-red-50' };
    return c[status] || 'text-gray-600 bg-gray-50';
  }
}
