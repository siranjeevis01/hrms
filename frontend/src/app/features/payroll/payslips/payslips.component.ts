import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PayrollService } from '../payroll.service';
import { Payslip, PayslipDetail } from '../payroll.models';

@Component({
  selector: 'app-payslips',
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
    MatTableModule,
    MatDialogModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './payslips.component.html',
  styleUrl: './payslips.component.scss',
})
export class PayslipsComponent implements OnInit {
  payslips = signal<Payslip[]>([]);
  loading = signal(true);
  selectedMonth = signal(new Date().getMonth() + 1);
  selectedYear = signal(new Date().getFullYear());
  searchQuery = signal('');
  departmentFilter = signal('');
  selectedPayslip = signal<PayslipDetail | null>(null);
  loadingDetail = signal(false);

  displayedColumns = [
    'employeeCode',
    'employeeName',
    'department',
    'grossSalary',
    'totalDeductions',
    'netPay',
    'status',
    'actions',
  ];

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

  years = [2024, 2025, 2026];
  departments = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];

  constructor(
    private payrollService: PayrollService,
    private dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.loadPayslips();
  }

  loadPayslips(): void {
    this.loading.set(true);
    this.payrollService
      .getPayslips({
        page: 1,
        pageSize: 100,
        month: this.selectedMonth(),
        year: this.selectedYear(),
        departmentId: this.departmentFilter() || undefined,
        employeeSearch: this.searchQuery() || undefined,
      })
      .subscribe({
        next: (result) => {
          this.payslips.set(result.items);
          this.loading.set(false);
        },
        error: () => this.loading.set(false),
      });
  }

  viewPayslip(row: Payslip): void {
    this.loadingDetail.set(true);
    this.payrollService.getPayslip(row.employeeId, row.month, row.year).subscribe({
      next: (detail) => {
        this.selectedPayslip.set(detail);
        this.loadingDetail.set(false);
      },
      error: () => this.loadingDetail.set(false),
    });
  }

  closePayslip(): void {
    this.selectedPayslip.set(null);
  }

  downloadPayslip(row: Payslip, name: string): void {
    this.payrollService.downloadPayslip(row.employeeId, row.month, row.year).subscribe({
      next: (blob) => {
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `payslip-${name}.pdf`;
        a.click();
        URL.revokeObjectURL(url);
      },
      error: () => {},
    });
  }

  bulkDownload(): void {
    this.payslips().forEach((p) => {
      this.downloadPayslip(p, p.employeeName.replace(/\s+/g, '-'));
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-IN', {
      style: 'currency',
      currency: 'INR',
      maximumFractionDigits: 0,
    }).format(value);
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Paid': return 'bg-emerald-100 text-emerald-700';
      case 'Generated': return 'bg-blue-100 text-blue-700';
      case 'Pending': return 'bg-amber-100 text-amber-700';
      case 'OnHold': return 'bg-red-100 text-red-700';
      default: return 'bg-gray-100 text-gray-600';
    }
  }
}
