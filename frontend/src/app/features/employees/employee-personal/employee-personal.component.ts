import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  FormArray,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-personal',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDividerModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
  ],
  templateUrl: './employee-personal.component.html',
  styleUrl: './employee-personal.component.scss',
})
export class EmployeePersonalComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private employeeService = inject(EmployeeService);
  private snackBar = inject(MatSnackBar);

  form!: FormGroup;
  loading = signal(true);
  saving = signal(false);
  employeeId = signal('');

  ngOnInit(): void {
    this.form = this.fb.group({
      address: this.fb.group({
        street: [''],
        city: [''],
        state: [''],
        zipCode: [''],
        country: [''],
      }),
      emergencyContacts: this.fb.array([]),
      bankDetails: this.fb.group({
        bankName: [''],
        accountNumber: [''],
        routingNumber: [''],
        accountType: [''],
      }),
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.employeeId.set(id);
      this.loadEmployee(id);
    }
  }

  get emergencyContacts(): FormArray {
    return this.form.get('emergencyContacts') as FormArray;
  }

  loadEmployee(id: string): void {
    this.employeeService.getEmployee(id).subscribe({
      next: (emp) => {
        if (emp.address) {
          (this.form.get('address') as FormGroup).patchValue(emp.address);
        }
        if (emp.bankDetails) {
          (this.form.get('bankDetails') as FormGroup).patchValue(emp.bankDetails);
        }
        if (emp.emergencyContacts?.length) {
          emp.emergencyContacts.forEach((c) => {
            this.emergencyContacts.push(
              this.fb.group({
                name: [c.name],
                relationship: [c.relationship],
                phoneNumber: [c.phoneNumber],
                email: [c.email],
              }),
            );
          });
        }
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load employee data', 'Close', { duration: 3000 });
      },
    });
  }

  addEmergencyContact(): void {
    this.emergencyContacts.push(
      this.fb.group({
        name: ['', Validators.required],
        relationship: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        email: [''],
      }),
    );
  }

  removeEmergencyContact(index: number): void {
    this.emergencyContacts.removeAt(index);
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.saving.set(true);

    this.employeeService.updateEmployee(this.employeeId(), this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Personal information updated', 'Close', { duration: 3000 });
        this.router.navigate(['/employees', this.employeeId()]);
      },
      error: () => {
        this.saving.set(false);
        this.snackBar.open('Failed to update', 'Close', { duration: 3000 });
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/employees', this.employeeId()]);
  }
}
