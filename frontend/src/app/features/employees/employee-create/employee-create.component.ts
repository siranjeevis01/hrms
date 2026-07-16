import { TitleCasePipe, DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatStepperModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    RouterLink,
    TitleCasePipe,
    DatePipe,
  ],
  templateUrl: './employee-create.component.html',
  styleUrl: './employee-create.component.scss',
})
export class EmployeeCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private employeeService = inject(EmployeeService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  personalForm!: FormGroup;
  employmentForm!: FormGroup;
  documentsForm!: FormGroup;
  loading = signal(false);

  departments = ['Engineering', 'HR', 'Finance', 'Marketing', 'Sales', 'Operations'];
  designations = ['Manager', 'Senior Developer', 'Developer', 'Junior Developer', 'Lead', 'Director', 'VP'];
  branches = ['New York', 'London', 'Singapore', 'Bangalore', 'Remote'];
  employmentTypes = ['Full-Time', 'Part-Time', 'Contract', 'Intern'];
  genders = ['Male', 'Female', 'Other'];
  maritalStatuses = ['Single', 'Married', 'Divorced', 'Widowed'];
  bloodGroups = ['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'];

  ngOnInit(): void {
    this.personalForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s-]{10,}$/)]],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      maritalStatus: [''],
      bloodGroup: [''],
      nationality: [''],
    });

    this.employmentForm = this.fb.group({
      department: ['', Validators.required],
      designation: ['', Validators.required],
      branch: ['', Validators.required],
      employmentType: ['', Validators.required],
      joinDate: ['', Validators.required],
      reportingManager: [''],
    });

    this.documentsForm = this.fb.group({
      photo: [''],
      idProof: [''],
      resume: [''],
    });
  }

  get personalControls() {
    return this.personalForm.controls;
  }

  get employmentControls() {
    return this.employmentForm.controls;
  }

  onSubmit(): void {
    if (this.personalForm.invalid || this.employmentForm.invalid) {
      this.snackBar.open('Please fill all required fields', 'Close', { duration: 3000 });
      return;
    }

    this.loading.set(true);

    const command = {
      ...this.personalForm.value,
      ...this.employmentForm.value,
    };

    this.employeeService.createEmployee(command).subscribe({
      next: (id) => {
        this.snackBar.open('Employee created successfully', 'Close', { duration: 3000 });
        this.router.navigate(['/employees', id]);
      },
      error: (err) => {
        this.loading.set(false);
        this.snackBar.open(
          err.error?.message || 'Failed to create employee',
          'Close',
          { duration: 3000 },
        );
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/employees']);
  }
}
