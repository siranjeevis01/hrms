import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PayrollService } from '../payroll.service';
import { TaxCalculation } from '../payroll.models';

@Component({
  selector: 'app-tax-calculator',
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
    MatProgressSpinnerModule,
  ],
  templateUrl: './tax-calculator.component.html',
  styleUrl: './tax-calculator.component.scss',
})
export class TaxCalculatorComponent {
  annualSalary = signal<number>(0);
  result = signal<TaxCalculation | null>(null);
  loading = signal(false);

  constructor(private payrollService: PayrollService) {}

  calculate(): void {
    if (!this.annualSalary() || this.annualSalary() <= 0) return;
    this.loading.set(true);
    this.payrollService.calculateTax(this.annualSalary()).subscribe({
      next: (calc) => {
        this.result.set(calc);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
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
