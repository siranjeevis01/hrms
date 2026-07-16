import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProjectsService } from '../projects.service';

@Component({
  selector: 'app-project-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatStepperModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './project-create.component.html',
  styleUrl: './project-create.component.scss',
})
export class ProjectCreateComponent {
  private fb = inject(FormBuilder);
  private projectsService = inject(ProjectsService);
  private router = inject(Router);

  saving = signal(false);
  teamMembers = signal<{ id: string; name: string }[]>([]);

  basicInfoForm: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    code: ['', [Validators.required, Validators.pattern(/^[A-Z0-9-]+$/)]],
    description: ['', Validators.required],
    client: [''],
    departmentId: ['', Validators.required],
    managerId: ['', Validators.required],
  });

  datesForm: FormGroup = this.fb.group({
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
    budget: [0, [Validators.required, Validators.min(0)]],
  });

  teamForm: FormGroup = this.fb.group({
    memberSearch: [''],
  });

  departments = signal<{ id: string; name: string }[]>([
    { id: '1', name: 'Engineering' },
    { id: '2', name: 'Marketing' },
    { id: '3', name: 'Sales' },
    { id: '4', name: 'Finance' },
    { id: '5', name: 'HR' },
  ]);

  managers = signal<{ id: string; name: string }[]>([
    { id: 'm1', name: 'John Smith' },
    { id: 'm2', name: 'Sarah Johnson' },
    { id: 'm3', name: 'Mike Wilson' },
  ]);

  suggestedMembers = signal<{ id: string; name: string }[]>([
    { id: 'u1', name: 'Alice Brown' },
    { id: 'u2', name: 'Bob Davis' },
    { id: 'u3', name: 'Carol White' },
    { id: 'u4', name: 'David Lee' },
    { id: 'u5', name: 'Eva Martinez' },
  ]);

  addTeamMember(member: { id: string; name: string }): void {
    const current = this.teamMembers();
    if (!current.find((m) => m.id === member.id)) {
      this.teamMembers.set([...current, member]);
    }
  }

  removeTeamMember(memberId: string): void {
    this.teamMembers.set(this.teamMembers().filter((m) => m.id !== memberId));
  }

  onSubmit(): void {
    if (this.basicInfoForm.invalid || this.datesForm.invalid) {
      this.basicInfoForm.markAllAsTouched();
      this.datesForm.markAllAsTouched();
      return;
    }

    this.saving.set(true);
    const formValue = { ...this.basicInfoForm.value, ...this.datesForm.value };

    this.projectsService
      .createProject({
        name: formValue.name,
        code: formValue.code,
        description: formValue.description,
        client: formValue.client,
        startDate: formValue.startDate,
        endDate: formValue.endDate,
        budget: formValue.budget,
        managerId: formValue.managerId,
        departmentId: formValue.departmentId,
        teamMemberIds: this.teamMembers().map((m) => m.id),
      })
      .subscribe({
        next: (project) => {
          this.saving.set(false);
          this.router.navigate(['/projects', project.id]);
        },
        error: () => {
          this.saving.set(false);
        },
      });
  }

  cancel(): void {
    this.router.navigate(['/projects']);
  }
}
