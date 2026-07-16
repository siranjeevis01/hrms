import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PayrollService } from '../payroll.service';
import { SalaryComponent } from '../payroll.models';

@Component({
  selector: 'app-salary-components',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './salary-components.component.html',
  styleUrl: './salary-components.component.scss',
})
export class SalaryComponentsComponent implements OnInit {
  components = signal<SalaryComponent[]>([]);
  loading = signal(true);

  constructor(private payrollService: PayrollService) {}

  ngOnInit(): void {
    this.loadComponents();
  }

  loadComponents(): void {
    this.payrollService.getSalaryComponents().subscribe({
      next: (components) => {
        this.components.set(components);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  getEarningComponents(): SalaryComponent[] {
    return this.components().filter((c) => c.type === 'Earning');
  }

  getDeductionComponents(): SalaryComponent[] {
    return this.components().filter((c) => c.type === 'Deduction');
  }

  formatValue(comp: SalaryComponent): string {
    return comp.calculationType === 'Fixed'
      ? new Intl.NumberFormat('en-IN', { style: 'currency', currency: 'INR', maximumFractionDigits: 0 }).format(comp.value)
      : `${comp.value}%`;
  }
}
