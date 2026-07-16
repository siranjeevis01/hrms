import { NgClass, DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { EmployeeService } from '../employee.service';
import { EmployeeListDto, PagedResult } from '../employee.models';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatChipsModule,
    RouterLink,
    NgClass,
    DatePipe,
  ],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss',
})
export class EmployeeListComponent implements OnInit {
  private employeeService = inject(EmployeeService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  displayedColumns = ['photo', 'employeeCode', 'name', 'department', 'designation', 'joinDate', 'status', 'actions'];

  employees = signal<EmployeeListDto[]>([]);
  loading = signal(true);
  totalItems = signal(0);
  currentPage = signal(0);
  pageSize = signal(10);

  search = signal('');
  selectedDepartment = signal('');
  selectedStatus = signal('');

  departments = ['Engineering', 'HR', 'Finance', 'Marketing', 'Sales', 'Operations'];
  statuses = ['Active', 'Inactive', 'OnNotice', 'Terminated'];

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading.set(true);
    this.employeeService.getEmployees({
      page: this.currentPage() + 1,
      pageSize: this.pageSize(),
      search: this.search(),
      department: this.selectedDepartment(),
      status: this.selectedStatus(),
    }).subscribe({
      next: (result) => {
        this.employees.set(result.items);
        this.totalItems.set(result.totalCount);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load employees', 'Close', { duration: 3000 });
      },
    });
  }

  onSearch(): void {
    this.currentPage.set(0);
    this.loadEmployees();
  }

  onFilterChange(): void {
    this.currentPage.set(0);
    this.loadEmployees();
  }

  onPageChange(event: PageEvent): void {
    this.currentPage.set(event.pageIndex);
    this.pageSize.set(event.pageSize);
    this.loadEmployees();
  }

  viewEmployee(id: string): void {
    this.router.navigate(['/employees', id]);
  }

  editEmployee(id: string): void {
    this.router.navigate(['/employees', id, 'edit']);
  }

  deleteEmployee(employee: EmployeeListDto): void {
    const confirmed = confirm(`Are you sure you want to delete ${employee.firstName} ${employee.lastName}?`);
    if (confirmed) {
      this.employeeService.deleteEmployee(employee.id).subscribe({
        next: () => {
          this.snackBar.open('Employee deleted successfully', 'Close', { duration: 3000 });
          this.loadEmployees();
        },
        error: () => {
          this.snackBar.open('Failed to delete employee', 'Close', { duration: 3000 });
        },
      });
    }
  }

  navigateToCreate(): void {
    this.router.navigate(['/employees/create']);
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      Active: 'bg-green-100 text-green-800',
      Inactive: 'bg-gray-100 text-gray-800',
      OnNotice: 'bg-yellow-100 text-yellow-800',
      Terminated: 'bg-red-100 text-red-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }
}
