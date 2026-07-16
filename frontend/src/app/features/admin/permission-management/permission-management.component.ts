import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTreeModule, MatTreeNestedDataSource } from '@angular/material/tree';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AdminService } from '../admin.service';
import { Permission } from '../admin.models';

@Component({
  selector: 'app-permission-management',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatCheckboxModule, MatTreeModule],
  templateUrl: './permission-management.component.html',
  styleUrl: './permission-management.component.scss',
})
export class PermissionManagementComponent implements OnInit {
  private adminService = inject(AdminService);
  permissions = signal<Permission[]>([]);
  loading = signal(true);
  treeControl = new NestedTreeControl<Permission>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<Permission>();
  hasChild = (_: number, node: Permission) => !!node.children && node.children.length > 0;

  ngOnInit(): void {
    this.adminService.getPermissions().subscribe({
      next: (p) => { this.permissions.set(p); this.dataSource.data = p; this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
