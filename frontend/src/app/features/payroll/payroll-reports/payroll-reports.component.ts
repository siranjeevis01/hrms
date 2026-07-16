import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PayrollService } from '../payroll.service';
import { PayrollReport } from '../payroll.models';

@Component({
  selector: 'app-payroll-reports',
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
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './payroll-reports.component.html',
  styleUrl: './payroll-reports.component.scss',
})
export class PayrollReportsComponent implements OnInit {
  startDate = signal<Date | null>(null);
  endDate = signal<Date | null>(null);
  department = signal('');
  reportType = signal('Monthly');
  reports = signal<PayrollReport[]>([]);
  loading = signal(false);
  hasGenerated = signal(false);

  departments = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];
  reportTypes = ['Daily', 'Weekly', 'Monthly'];
  displayedColumns = ['department', 'employeeCount', 'totalGross', 'totalDeductions', 'totalNet'];

  constructor(private payrollService: PayrollService) {}

  ngOnInit(): void {
    const today = new Date();
    this.startDate.set(new Date(today.getFullYear(), today.getMonth(), 1));
    this.endDate.set(today);
  }

  generateReport(): void {
    if (!this.startDate() || !this.endDate()) return;
    this.loading.set(true);
    this.hasGenerated.set(true);

    this.payrollService
      .getPayrollReports({
        startDate: this.startDate()!.toISOString().split('T')[0],
        endDate: this.endDate()!.toISOString().split('T')[0],
        reportType: this.reportType(),
        departmentId: this.department() || undefined,
      })
      .subscribe({
        next: (reports) => {
          this.reports.set(reports);
          this.loading.set(false);
        },
        error: () => {
          this.reports.set([]);
          this.loading.set(false);
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

  totalGross(): number {
    return this.reports().reduce((s, r) => s + r.totalGross, 0);
  }

  totalDeductions(): number {
    return this.reports().reduce((s, r) => s + r.totalDeductions, 0);
  }

  totalNet(): number {
    return this.reports().reduce((s, r) => s + r.totalNet, 0);
  }

  totalEmployees(): number {
    return this.reports().reduce((s, r) => s + r.employeeCount, 0);
  }

  exportCSV(): void {
    const headers = ['Department', 'Employees', 'Gross', 'Deductions', 'Net Pay'];
    const rows = this.reports().map((r) => [
      r.department,
      r.employeeCount.toString(),
      r.totalGross.toString(),
      r.totalDeductions.toString(),
      r.totalNet.toString(),
    ]);
    const csv = [headers, ...rows].map((r) => r.join(',')).join('\n');
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'payroll-report.csv';
    a.click();
    URL.revokeObjectURL(url);
  }
}
