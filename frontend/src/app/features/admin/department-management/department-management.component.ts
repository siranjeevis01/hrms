import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTreeModule, MatTreeNestedDataSource } from '@angular/material/tree';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatDialogModule } from '@angular/material/dialog';
import { AdminService } from '../admin.service';
import { Department } from '../admin.models';

@Component({
  selector: 'app-department-management',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatTreeModule, MatDialogModule],
  templateUrl: './department-management.component.html',
  styleUrl: './department-management.component.scss',
})
export class DepartmentManagementComponent implements OnInit {
  private adminService = inject(AdminService);
  departments = signal<Department[]>([]);
  loading = signal(true);
  treeControl = new NestedTreeControl<Department>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<Department>();
  hasChild = (_: number, node: Department) => !!node.children && node.children.length > 0;

  ngOnInit(): void {
    this.adminService.getDepartments().subscribe({
      next: (depts) => { this.departments.set(depts); this.dataSource.data = depts; this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
