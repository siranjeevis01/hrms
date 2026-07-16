import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { RecruitmentService } from '../recruitment.service';
import { EmploymentType } from '../recruitment.models';

@Component({
  selector: 'app-job-create',
  standalone: true,
  imports: [
    ReactiveFormsModule, FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatChipsModule, MatFormFieldModule, MatProgressSpinnerModule,
    MatSnackBarModule,
  ],
  templateUrl: './job-create.component.html',
  styleUrl: './job-create.component.scss',
})
export class JobCreateComponent {
  private fb = inject(FormBuilder);
  private recruitmentService = inject(RecruitmentService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  saving = signal(false);
  newSkill = '';
  newRequirement = '';
  newResponsibility = '';
  newBenefit = '';

  employmentTypes: { value: EmploymentType; label: string }[] = [
    { value: 'full_time', label: 'Full Time' },
    { value: 'part_time', label: 'Part Time' },
    { value: 'contract', label: 'Contract' },
    { value: 'internship', label: 'Internship' },
    { value: 'freelance', label: 'Freelance' },
  ];

  jobForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', [Validators.required, Validators.minLength(10)]],
    department: ['', Validators.required],
    designation: ['', Validators.required],
    employmentType: ['full_time', Validators.required],
    minExperience: [0, [Validators.required, Validators.min(0)]],
    maxExperience: [10, [Validators.required, Validators.min(0)]],
    minSalary: [0, [Validators.required, Validators.min(0)]],
    maxSalary: [0, [Validators.required, Validators.min(0)]],
    skills: [[] as string[]],
    requirements: [[] as string[]],
    responsibilities: [[] as string[]],
    benefits: [[] as string[]],
  });

  get skills(): string[] { return this.jobForm.get('skills')?.value || []; }
  get requirements(): string[] { return this.jobForm.get('requirements')?.value || []; }
  get responsibilities(): string[] { return this.jobForm.get('responsibilities')?.value || []; }
  get benefits(): string[] { return this.jobForm.get('benefits')?.value || []; }

  addSkill(): void {
    if (this.newSkill.trim()) {
      const current = this.skills;
      this.jobForm.patchValue({ skills: [...current, this.newSkill.trim()] });
      this.newSkill = '';
    }
  }

  removeSkill(skill: string): void {
    this.jobForm.patchValue({ skills: this.skills.filter(s => s !== skill) });
  }

  addRequirement(): void {
    if (this.newRequirement.trim()) {
      this.jobForm.patchValue({ requirements: [...this.requirements, this.newRequirement.trim()] });
      this.newRequirement = '';
    }
  }

  removeRequirement(req: string): void {
    this.jobForm.patchValue({ requirements: this.requirements.filter(r => r !== req) });
  }

  addResponsibility(): void {
    if (this.newResponsibility.trim()) {
      this.jobForm.patchValue({ responsibilities: [...this.responsibilities, this.newResponsibility.trim()] });
      this.newResponsibility = '';
    }
  }

  removeResponsibility(resp: string): void {
    this.jobForm.patchValue({ responsibilities: this.responsibilities.filter(r => r !== resp) });
  }

  addBenefit(): void {
    if (this.newBenefit.trim()) {
      this.jobForm.patchValue({ benefits: [...this.benefits, this.newBenefit.trim()] });
      this.newBenefit = '';
    }
  }

  removeBenefit(benefit: string): void {
    this.jobForm.patchValue({ benefits: this.benefits.filter(b => b !== benefit) });
  }

  saveDraft(): void {
    this.saveJob('draft');
  }

  publish(): void {
    this.saveJob('published');
  }

  private saveJob(status: string): void {
    if (this.jobForm.invalid) {
      this.jobForm.markAllAsTouched();
      return;
    }
    this.saving.set(true);
    const jobData = { ...this.jobForm.value, status };
    this.recruitmentService.createJob(jobData).subscribe({
      next: () => {
        this.snackBar.open('Job created successfully', 'Close', { duration: 3000 });
        this.router.navigate(['/recruitment/jobs']);
      },
      error: () => {
        this.saving.set(false);
        this.snackBar.open('Failed to create job', 'Close', { duration: 3000 });
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/recruitment/jobs']);
  }
}
