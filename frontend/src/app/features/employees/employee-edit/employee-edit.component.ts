import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
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
  ],
  templateUrl: './employee-edit.component.html',
  styleUrl: './employee-edit.component.scss',
})
export class EmployeeEditComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private employeeService = inject(EmployeeService);
  private snackBar = inject(MatSnackBar);

  form!: FormGroup;
  loading = signal(true);
  saving = signal(false);
  employeeId = signal('');

  departments = ['Engineering', 'HR', 'Finance', 'Marketing', 'Sales', 'Operations'];
  designations = ['Manager', 'Senior Developer', 'Developer', 'Junior Developer', 'Lead', 'Director', 'VP'];
  branches = ['New York', 'London', 'Singapore', 'Bangalore', 'Remote'];
  employmentTypes = ['Full-Time', 'Part-Time', 'Contract', 'Intern'];
  genders = ['Male', 'Female', 'Other'];
  maritalStatuses = ['Single', 'Married', 'Divorced', 'Widowed'];
  bloodGroups = ['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'];

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s-]{10,}$/)]],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      maritalStatus: [''],
      bloodGroup: [''],
      nationality: [''],
      department: ['', Validators.required],
      designation: ['', Validators.required],
      branch: ['', Validators.required],
      employmentType: ['', Validators.required],
      joinDate: ['', Validators.required],
      reportingManager: [''],
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.employeeId.set(id);
      this.loadEmployee(id);
    }
  }

  get controls() {
    return this.form.controls;
  }

  loadEmployee(id: string): void {
    this.employeeService.getEmployee(id).subscribe({
      next: (emp) => {
        this.form.patchValue({
          firstName: emp.firstName,
          lastName: emp.lastName,
          email: emp.email,
          phoneNumber: emp.phoneNumber,
          dateOfBirth: emp.dateOfBirth,
          gender: emp.gender,
          maritalStatus: emp.maritalStatus,
          bloodGroup: emp.bloodGroup,
          nationality: emp.nationality,
          department: emp.department,
          designation: emp.designation,
          branch: emp.branch,
          employmentType: '',
          joinDate: emp.joinDate,
          reportingManager: '',
        });
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load employee', 'Close', { duration: 3000 });
      },
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.employeeService.updateEmployee(this.employeeId(), this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Employee updated successfully', 'Close', { duration: 3000 });
        this.router.navigate(['/employees', this.employeeId()]);
      },
      error: (err) => {
        this.saving.set(false);
        this.snackBar.open(err.error?.message || 'Failed to update employee', 'Close', { duration: 3000 });
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/employees', this.employeeId()]);
  }
}
