import { NgClass, DecimalPipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../employee.service';
import { SalaryStructure } from '../employee.models';

@Component({
  selector: 'app-employee-salary',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    RouterLink,
    NgClass,
    DecimalPipe,
  ],
  templateUrl: './employee-salary.component.html',
  styleUrl: './employee-salary.component.scss',
})
export class EmployeeSalaryComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private employeeService = inject(EmployeeService);
  private snackBar = inject(MatSnackBar);

  salary = signal<SalaryStructure | null>(null);
  loading = signal(true);
  editing = signal(false);
  saving = signal(false);
  employeeId = signal('');

  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.fb.group({
      basicSalary: [0, [Validators.required, Validators.min(0)]],
      hra: [0, [Validators.min(0)]],
      transportAllowance: [0, [Validators.min(0)]],
      medicalAllowance: [0, [Validators.min(0)]],
      specialAllowance: [0, [Validators.min(0)]],
      pf: [0, [Validators.min(0)]],
      esi: [0, [Validators.min(0)]],
      professionalTax: [0, [Validators.min(0)]],
      incomeTax: [0, [Validators.min(0)]],
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.employeeId.set(id);
      this.loadSalary(id);
    }
  }

  get controls() {
    return this.form.controls;
  }

  loadSalary(id: string): void {
    this.loading.set(true);
    this.employeeService.getEmployeeSalary(id).subscribe({
      next: (data) => {
        this.salary.set(data);
        this.form.patchValue(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load salary data', 'Close', { duration: 3000 });
      },
    });
  }

  startEdit(): void {
    this.editing.set(true);
  }

  cancelEdit(): void {
    this.editing.set(false);
    if (this.salary()) {
      this.form.patchValue(this.salary()!);
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.saving.set(true);

    this.employeeService.updateEmployee(this.employeeId(), this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Salary updated successfully', 'Close', { duration: 3000 });
        this.editing.set(false);
        this.saving.set(false);
        this.loadSalary(this.employeeId());
      },
      error: () => {
        this.saving.set(false);
        this.snackBar.open('Failed to update salary', 'Close', { duration: 3000 });
      },
    });
  }

  goBack(): void {
    this.router.navigate(['/employees', this.employeeId()]);
  }
}
