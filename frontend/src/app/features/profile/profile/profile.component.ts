import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ProfileService } from '../profile.service';
import { UserProfile, EmergencyContact, EmployeeSkill, LeaveSummary, AttendanceSummary } from '../profile.models';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatTabsModule, MatChipsModule, MatProgressSpinnerModule, MatProgressBarModule, NgClass, DatePipe, TitleCasePipe],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  private profileService = inject(ProfileService);
  protected router = inject(Router);

  profile = signal<UserProfile | null>(null);
  emergencyContacts = signal<EmergencyContact[]>([]);
  skills = signal<EmployeeSkill[]>([]);
  leaveSummary = signal<LeaveSummary[]>([]);
  attendanceSummary = signal<AttendanceSummary | null>(null);
  loading = signal(true);
  editing = signal(false);

  editedProfile: Partial<UserProfile> = {};

  ngOnInit(): void { this.loadProfile(); }

  loadProfile(): void {
    this.loading.set(true);
    this.profileService.getProfile().subscribe({
      next: (p) => { this.profile.set(p); this.editedProfile = { ...p }; this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.profileService.getEmergencyContacts().subscribe({
      next: (c) => this.emergencyContacts.set(Array.isArray(c) ? c : []),
      error: () => this.emergencyContacts.set([]),
    });
    this.profileService.getSkills().subscribe({
      next: (s) => this.skills.set(Array.isArray(s) ? s : []),
      error: () => this.skills.set([]),
    });
    this.profileService.getLeaveSummary().subscribe({
      next: (l) => this.leaveSummary.set(Array.isArray(l) ? l : []),
      error: () => this.leaveSummary.set([]),
    });
    this.profileService.getAttendanceSummary().subscribe({
      next: (a) => this.attendanceSummary.set(a),
      error: () => this.attendanceSummary.set(null),
    });
  }

  startEditing(): void { this.editedProfile = { ...this.profile()! }; this.editing.set(true); }
  cancelEditing(): void { this.editing.set(false); }

  saveProfile(): void {
    this.profileService.updateProfile(this.editedProfile as any).subscribe({
      next: (p) => { this.profile.set(p); this.editing.set(false); },
    });
  }

  get skillLevelColor(): Record<string, string> {
    return { beginner: 'bg-gray-100 text-gray-700', intermediate: 'bg-blue-100 text-blue-700', advanced: 'bg-green-100 text-green-700', expert: 'bg-purple-100 text-purple-700' };
  }

  getSkillLevelPercent(level: string): number {
    const m: Record<string, number> = { beginner: 25, intermediate: 50, advanced: 75, expert: 100 };
    return m[level] || 0;
  }
}
