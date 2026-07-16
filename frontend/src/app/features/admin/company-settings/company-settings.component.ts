import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AdminService } from '../admin.service';
import { CompanySettings } from '../admin.models';

@Component({
  selector: 'app-company-settings',
  standalone: true,
  imports: [ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatProgressSpinnerModule],
  templateUrl: './company-settings.component.html',
  styleUrl: './company-settings.component.scss',
})
export class CompanySettingsComponent implements OnInit {
  private fb = inject(FormBuilder);
  private adminService = inject(AdminService);
  saving = signal(false);
  loading = signal(true);

  settingsForm: FormGroup = this.fb.group({
    name: ['', Validators.required], logo: [''], address: [''], city: [''], state: [''],
    country: [''], postalCode: [''], phone: [''], email: ['', Validators.email],
    website: [''], currency: ['USD'], timezone: ['UTC'], dateFormat: ['MM/dd/yyyy'],
    language: ['en'], taxId: [''], registrationNumber: [''],
  });

  ngOnInit(): void {
    this.adminService.getCompanySettings().subscribe({
      next: (s) => { this.settingsForm.patchValue(s); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  save(): void {
    if (this.settingsForm.invalid) return;
    this.saving.set(true);
    this.adminService.updateCompanySettings(this.settingsForm.value).subscribe({
      next: () => this.saving.set(false),
      error: () => this.saving.set(false),
    });
  }
}
