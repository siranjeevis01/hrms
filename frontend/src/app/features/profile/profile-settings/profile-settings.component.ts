import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { TitleCasePipe, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { ProfileService } from '../profile.service';
import { ProfileSettings, SessionInfo } from '../profile.models';

@Component({
  selector: 'app-profile-settings',
  standalone: true,
  imports: [ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatSlideToggleModule, MatDividerModule, MatProgressSpinnerModule, MatListModule, TitleCasePipe, DatePipe],
  templateUrl: './profile-settings.component.html',
  styleUrl: './profile-settings.component.scss',
})
export class ProfileSettingsComponent implements OnInit {
  private fb = inject(FormBuilder);
  private profileService = inject(ProfileService);
  settings = signal<ProfileSettings | null>(null);
  sessions = signal<SessionInfo[]>([]);
  loading = signal(true);
  saving = signal(false);

  passwordForm: FormGroup = this.fb.group({
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, Validators.minLength(8)]],
  });

  ngOnInit(): void {
    this.profileService.getSettings().subscribe({
      next: (s) => { this.settings.set(s); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.profileService.getSessions().subscribe({ next: (s) => this.sessions.set(s) });
  }

  changePassword(): void {
    if (this.passwordForm.invalid) return;
    this.saving.set(true);
    this.profileService.changePassword(this.passwordForm.value).subscribe({
      next: () => { this.saving.set(false); this.passwordForm.reset(); },
      error: () => this.saving.set(false),
    });
  }

  toggleTheme(theme: 'light' | 'dark' | 'system'): void {
    this.profileService.updateSettings({ theme }).subscribe({
      next: (s) => this.settings.set(s),
    });
  }

  toggleNotification(type: string, field: 'email' | 'push' | 'sms'): void {
    const current = this.settings();
    if (!current) return;
    const notifications = current.notifications.map((n) => n.type === type ? { ...n, [field]: !n[field] } : n);
    this.profileService.updateSettings({ notifications }).subscribe({ next: (s) => this.settings.set(s) });
  }

  revokeSession(id: string): void {
    this.profileService.revokeSession(id).subscribe({ next: () => this.sessions.update((s) => s.filter((si) => si.id !== id)) });
  }
}
