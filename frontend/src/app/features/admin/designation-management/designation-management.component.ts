import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { DecimalPipe } from '@angular/common';
import { AdminService } from '../admin.service';
import { Designation } from '../admin.models';

@Component({
  selector: 'app-designation-management',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatTableModule, DecimalPipe],
  templateUrl: './designation-management.component.html',
  styleUrl: './designation-management.component.scss',
})
export class DesignationManagementComponent implements OnInit {
  private adminService = inject(AdminService);
  designations = signal<Designation[]>([]);
  loading = signal(true);
  displayedColumns = ['name', 'level', 'department', 'employeeCount', 'salary', 'actions'];

  ngOnInit(): void {
    this.adminService.getDesignations().subscribe({
      next: (d) => { this.designations.set(d); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
