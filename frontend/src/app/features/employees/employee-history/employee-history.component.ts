import { NgClass, DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../employee.service';
import { HistoryDto } from '../employee.models';

@Component({
  selector: 'app-employee-history',
  standalone: true,
  imports: [
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    RouterLink,
    NgClass,
    DatePipe,
  ],
  templateUrl: './employee-history.component.html',
  styleUrl: './employee-history.component.scss',
})
export class EmployeeHistoryComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private employeeService = inject(EmployeeService);
  private snackBar = inject(MatSnackBar);

  history = signal<HistoryDto[]>([]);
  filteredHistory = signal<HistoryDto[]>([]);
  loading = signal(true);
  employeeId = signal('');
  selectedAction = signal('');

  actionTypes = ['Promotion', 'Transfer', 'Status Change', 'Salary Change', 'Personal Update'];

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.employeeId.set(id);
      this.loadHistory(id);
    }
  }

  loadHistory(id: string): void {
    this.loading.set(true);
    this.employeeService.getEmployeeHistory(id).subscribe({
      next: (data) => {
        this.history.set(data);
        this.filteredHistory.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load history', 'Close', { duration: 3000 });
      },
    });
  }

  filterByAction(): void {
    const action = this.selectedAction();
    if (!action) {
      this.filteredHistory.set(this.history());
    } else {
      this.filteredHistory.set(
        this.history().filter((h) => h.action === action),
      );
    }
  }

  getActionIcon(action: string): string {
    const icons: Record<string, string> = {
      Promotion: 'trending_up',
      Transfer: 'swap_horiz',
      'Status Change': 'autorenew',
      'Salary Change': 'payments',
      'Personal Update': 'person',
    };
    return icons[action] || 'info';
  }

  getActionColor(action: string): string {
    const colors: Record<string, string> = {
      Promotion: 'bg-green-500',
      Transfer: 'bg-blue-500',
      'Status Change': 'bg-yellow-500',
      'Salary Change': 'bg-purple-500',
      'Personal Update': 'bg-gray-500',
    };
    return colors[action] || 'bg-gray-400';
  }

  goBack(): void {
    this.router.navigate(['/employees', this.employeeId()]);
  }
}
