import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  selector: 'app-login',
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
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private toastService = inject(ToastService);

  loginForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    rememberMe: [false],
  });

  hidePassword = true;
  loading = signal(false);
  socialLoading = signal<string | null>(null);

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    const { email, password } = this.loginForm.value;
    this.authService.login(email, password).subscribe({
      next: () => {
        this.toastService.success('Welcome back!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.loading.set(false);
        this.toastService.error(error.error?.message || 'Login failed. Please check your credentials.');
      },
    });
  }

  loginWithGoogle(): void {
    this.socialLoading.set('google');
    this.authService.loginWithGoogle().subscribe({
      next: () => {
        this.toastService.success('Welcome back!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.socialLoading.set(null);
        if (error.code !== 'auth/popup-closed-by-user') {
          this.toastService.error('Google login failed');
        }
      },
    });
  }

  loginWithMicrosoft(): void {
    this.socialLoading.set('microsoft');
    this.authService.loginWithMicrosoft().subscribe({
      next: () => {
        this.toastService.success('Welcome back!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.socialLoading.set(null);
        if (error.code !== 'auth/popup-closed-by-user') {
          this.toastService.error('Microsoft login failed');
        }
      },
    });
  }

  loginWithGitHub(): void {
    this.socialLoading.set('github');
    this.authService.loginWithGitHub().subscribe({
      next: () => {
        this.toastService.success('Welcome back!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.socialLoading.set(null);
        if (error.code !== 'auth/popup-closed-by-user') {
          this.toastService.error('GitHub login failed');
        }
      },
    });
  }

  loginWithLinkedIn(): void {
    this.socialLoading.set('linkedin');
    this.authService.loginWithLinkedIn().subscribe({
      next: () => {
        this.toastService.success('Welcome back!');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.socialLoading.set(null);
        if (error.code !== 'auth/popup-closed-by-user') {
          this.toastService.error('LinkedIn login failed');
        }
      },
    });
  }
}
