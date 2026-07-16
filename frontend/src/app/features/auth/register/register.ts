import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthService } from '../../../core/services/auth.service';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    MatDividerModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private toastService = inject(ToastService);

  registerForm: FormGroup = this.fb.group(
    {
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[\d\s\-()]{7,15}$/)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]],
      acceptTerms: [false, [Validators.requiredTrue]],
    },
    { validators: this.passwordMatchValidator }
  );

  hidePassword = true;
  hideConfirmPassword = true;
  loading = signal(false);
  socialLoading = signal<string | null>(null);

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }
    return null;
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    const { firstName, lastName, email, phoneNumber, password } = this.registerForm.value;
    this.authService
      .register({ firstName, lastName, email, phoneNumber, password })
      .subscribe({
        next: () => {
          this.toastService.success('Account created successfully! Please sign in.');
          this.router.navigate(['/auth/login']);
        },
        error: (error) => {
          this.loading.set(false);
          this.toastService.error(error.error?.message || 'Registration failed. Please try again.');
        },
      });
  }

  registerWithGoogle(): void {
    this.socialLoading.set('google');
    this.authService.loginWithGoogle().subscribe({
      next: () => {
        this.toastService.success('Account created successfully!');
        this.router.navigate(['/dashboard']);
      },
      error: () => this.socialLoading.set(null),
    });
  }

  registerWithMicrosoft(): void {
    this.socialLoading.set('microsoft');
    this.authService.loginWithMicrosoft().subscribe({
      next: () => {
        this.toastService.success('Account created successfully!');
        this.router.navigate(['/dashboard']);
      },
      error: () => this.socialLoading.set(null),
    });
  }
}
