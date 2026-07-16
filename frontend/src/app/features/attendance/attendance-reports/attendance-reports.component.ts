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
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AttendanceService } from '../attendance.service';
import { AttendanceReport } from '../attendance.models';

@Component({
  selector: 'app-attendance-reports',
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
    MatTabsModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './attendance-reports.component.html',
  styleUrl: './attendance-reports.component.scss',
})
export class AttendanceReportsComponent implements OnInit {
  startDate = signal<Date | null>(null);
  endDate = signal<Date | null>(null);
  department = signal('');
  reportType = signal<string>('Monthly');
  reports = signal<AttendanceReport[]>([]);
  loading = signal(false);
  hasGenerated = signal(false);

  departments = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];
  reportTypes = ['Daily', 'Weekly', 'Monthly'];
  displayedColumns = ['employeeName', 'department', 'present', 'absent', 'late', 'hours'];

  constructor(private attendanceService: AttendanceService) {}

  ngOnInit(): void {
    const today = new Date();
    const firstOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    this.startDate.set(firstOfMonth);
    this.endDate.set(today);
  }

  generateReport(): void {
    if (!this.startDate() || !this.endDate()) return;
    this.loading.set(true);
    this.hasGenerated.set(true);

    const filters = {
      startDate: this.startDate()!.toISOString().split('T')[0],
      endDate: this.endDate()!.toISOString().split('T')[0],
      reportType: this.reportType() as 'Daily' | 'Weekly' | 'Monthly',
      departmentId: this.department() || undefined,
    };

    this.attendanceService.getAttendanceReports(filters).subscribe({
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

  exportCSV(): void {
    const headers = ['Employee Name', 'Department', 'Present', 'Absent', 'Late', 'Hours'];
    const rows = this.reports().map((r) => [
      r.employeeName,
      r.department,
      r.present.toString(),
      r.absent.toString(),
      r.late.toString(),
      r.hours.toString(),
    ]);
    const csv = [headers, ...rows].map((row) => row.join(',')).join('\n');
    this.downloadFile(csv, 'attendance-report.csv', 'text/csv');
  }

  exportExcel(): void {
    const headers = ['Employee Name', 'Department', 'Present', 'Absent', 'Late', 'Hours'];
    const rows = this.reports().map((r) => [
      r.employeeName,
      r.department,
      r.present,
      r.absent,
      r.late,
      r.hours,
    ]);
    const content = [headers.join(','), ...rows.map((r) => r.join(','))].join('\n');
    this.downloadFile(content, 'attendance-report.xlsx', 'application/vnd.ms-excel');
  }

  exportPDF(): void {
    const content = this.reports()
      .map((r) => `${r.employeeName},${r.department},${r.present},${r.absent},${r.late},${r.hours}`)
      .join('\n');
    const headers = 'Employee Name,Department,Present,Absent,Late,Hours';
    this.downloadFile(`${headers}\n${content}`, 'attendance-report.txt', 'text/plain');
  }

  private downloadFile(content: string, filename: string, type: string): void {
    const blob = new Blob([content], { type });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    a.click();
    URL.revokeObjectURL(url);
  }

  totalPresent(): number {
    return this.reports().reduce((sum, r) => sum + r.present, 0);
  }

  totalAbsent(): number {
    return this.reports().reduce((sum, r) => sum + r.absent, 0);
  }

  totalHours(): number {
    return this.reports().reduce((sum, r) => sum + r.hours, 0);
  }
}
