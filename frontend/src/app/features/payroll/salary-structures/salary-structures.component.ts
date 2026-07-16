import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PayrollService } from '../payroll.service';
import { SalaryStructure, SalaryComponent } from '../payroll.models';

@Component({
  selector: 'app-salary-structures',
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
    MatSnackBarModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './salary-structures.component.html',
  styleUrl: './salary-structures.component.scss',
})
export class SalaryStructuresComponent implements OnInit {
  structures = signal<SalaryStructure[]>([]);
  loading = signal(true);
  showForm = signal(false);
  submitting = signal(false);

  structureName = signal('');
  components = signal<{ name: string; type: string; calculationType: string; value: number }[]>([]);

  componentTypes = ['Earning', 'Deduction'];
  calculationTypes = ['Fixed', 'Percentage'];

  constructor(
    private payrollService: PayrollService,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    this.loadStructures();
  }

  loadStructures(): void {
    this.payrollService.getSalaryStructures().subscribe({
      next: (structures) => {
        this.structures.set(structures);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  openForm(): void {
    this.structureName.set('');
    this.components.set([]);
    this.addComponent();
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  addComponent(): void {
    this.components.update((c) => [
      ...c,
      { name: '', type: 'Earning', calculationType: 'Fixed', value: 0 },
    ]);
  }

  removeComponent(index: number): void {
    this.components.update((c) => c.filter((_, i) => i !== index));
  }

  updateComponent(index: number, field: string, value: any): void {
    this.components.update((comps) => {
      const updated = [...comps];
      (updated[index] as any)[field] = value;
      return updated;
    });
  }

  saveStructure(): void {
    if (!this.structureName()) {
      this.snackBar.open('Please enter a structure name', 'Close', { duration: 3000 });
      return;
    }
    if (this.components().length === 0) {
      this.snackBar.open('Add at least one component', 'Close', { duration: 3000 });
      return;
    }

    this.submitting.set(true);
    this.payrollService
      .createSalaryStructure({
        name: this.structureName(),
        components: this.components().map((c) => ({
          id: '',
          name: c.name,
          type: c.type as any,
          calculationType: c.calculationType as any,
          value: c.value,
        })),
      })
      .subscribe({
        next: () => {
          this.snackBar.open('Salary structure created', 'Close', { duration: 3000 });
          this.showForm.set(false);
          this.submitting.set(false);
          this.loadStructures();
        },
        error: () => {
          this.snackBar.open('Failed to create structure', 'Close', { duration: 3000 });
          this.submitting.set(false);
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
