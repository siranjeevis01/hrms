import { TitleCasePipe, DatePipe, DecimalPipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../employee.service';
import { EmployeeDto, DocumentDto, HistoryDto } from '../employee.models';

@Component({
  selector: 'app-employee-detail',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatDividerModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatSnackBarModule,
    RouterLink,
    TitleCasePipe,
    DatePipe,
    DecimalPipe,
  ],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.scss',
})
export class EmployeeDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private employeeService = inject(EmployeeService);
  private snackBar = inject(MatSnackBar);

  employee = signal<EmployeeDto | null>(null);
  documents = signal<DocumentDto[]>([]);
  history = signal<HistoryDto[]>([]);
  loading = signal(true);
  selectedTab = signal(0);

  documentColumns = ['name', 'type', 'uploadDate', 'fileSize', 'actions'];
  historyColumns = ['action', 'description', 'performedBy', 'performedOn'];

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadEmployee(id);
      this.loadDocuments(id);
      this.loadHistory(id);
    }
  }

  loadEmployee(id: string): void {
    this.loading.set(true);
    this.employeeService.getEmployee(id).subscribe({
      next: (data) => {
        this.employee.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load employee details', 'Close', { duration: 3000 });
      },
    });
  }

  loadDocuments(id: string): void {
    this.employeeService.getEmployeeDocuments(id).subscribe({
      next: (data) => this.documents.set(data),
      error: () => {},
    });
  }

  loadHistory(id: string): void {
    this.employeeService.getEmployeeHistory(id).subscribe({
      next: (data) => this.history.set(data),
      error: () => {},
    });
  }

  goBack(): void {
    this.router.navigate(['/employees']);
  }

  editEmployee(): void {
    const emp = this.employee();
    if (emp) {
      this.router.navigate(['/employees', emp.id, 'edit']);
    }
  }

  formatFileSize(bytes: number): string {
    if (bytes < 1024) return bytes + ' B';
    if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
    return (bytes / 1048576).toFixed(1) + ' MB';
  }
}
