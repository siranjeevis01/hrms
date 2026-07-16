import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { PayrollService } from '../payroll.service';
import { PayrollRunResult } from '../payroll.models';

@Component({
  selector: 'app-payroll-run',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
  ],
  templateUrl: './payroll-run.component.html',
  styleUrl: './payroll-run.component.scss',
})
export class PayrollRunComponent implements OnInit {
  selectedMonth = signal(new Date().getMonth() + 1);
  selectedYear = signal(new Date().getFullYear());
  isRunning = signal(false);
  result = signal<PayrollRunResult | null>(null);
  progress = signal(0);
  showConfirmDialog = signal(false);

  months = [
    { value: 1, label: 'January' },
    { value: 2, label: 'February' },
    { value: 3, label: 'March' },
    { value: 4, label: 'April' },
    { value: 5, label: 'May' },
    { value: 6, label: 'June' },
    { value: 7, label: 'July' },
    { value: 8, label: 'August' },
    { value: 9, label: 'September' },
    { value: 10, label: 'October' },
    { value: 11, label: 'November' },
    { value: 12, label: 'December' },
  ];

  years = [2024, 2025, 2026, 2027];

  constructor(
    private payrollService: PayrollService,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {}

  openConfirmDialog(): void {
    this.showConfirmDialog.set(true);
  }

  closeConfirmDialog(): void {
    this.showConfirmDialog.set(false);
  }

  executePayroll(): void {
    this.showConfirmDialog.set(false);
    this.isRunning.set(true);
    this.progress.set(0);
    this.result.set(null);

    const progressInterval = setInterval(() => {
      this.progress.update((p) => Math.min(p + 5, 90));
    }, 300);

    this.payrollService.runPayroll(this.selectedMonth(), this.selectedYear()).subscribe({
      next: (result) => {
        clearInterval(progressInterval);
        this.progress.set(100);
        setTimeout(() => {
          this.isRunning.set(false);
          this.result.set(result);
          if (result.errors.length === 0) {
            this.snackBar.open(
              `Payroll processed for ${result.processedCount} employees`,
              'Close',
              { duration: 5000 },
            );
          } else {
            this.snackBar.open(
              `Processed with ${result.errors.length} errors`,
              'Close',
              { duration: 5000 },
            );
          }
        }, 500);
      },
      error: () => {
        clearInterval(progressInterval);
        this.isRunning.set(false);
        this.progress.set(0);
        this.snackBar.open('Payroll processing failed', 'Close', { duration: 3000 });
      },
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-IN', {
      style: 'currency',
      currency: 'INR',
      maximumFractionDigits: 0,
    }).format(value);
  }
}
