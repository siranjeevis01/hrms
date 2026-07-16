import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PerformanceService } from '../performance.service';

@Component({
  selector: 'app-goal-create',
  standalone: true,
  imports: [
    ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule,
    MatSnackBarModule,
  ],
  templateUrl: './goal-create.component.html',
  styleUrl: './goal-create.component.scss',
})
export class GoalCreateComponent {
  private fb = inject(FormBuilder);
  private performanceService = inject(PerformanceService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  saving = signal(false);
  minDate = new Date();

  goalForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', Validators.required],
    category: ['individual', Validators.required],
    startDate: [new Date(), Validators.required],
    endDate: [new Date(), Validators.required],
    priority: ['medium', Validators.required],
    weight: [50, [Validators.required, Validators.min(1), Validators.max(100)]],
    assignedTo: [[] as string[]],
    keyResults: this.fb.array([]),
  });

  get keyResults(): FormArray { return this.goalForm.get('keyResults') as FormArray; }

  addKeyResult(): void {
    this.keyResults.push(this.fb.group({
      title: ['', Validators.required],
      target: [100, [Validators.required, Validators.min(1)]],
      current: [0],
      unit: [''],
    }));
  }

  removeKeyResult(index: number): void {
    this.keyResults.removeAt(index);
  }

  save(): void {
    if (this.goalForm.invalid) { this.goalForm.markAllAsTouched(); return; }
    this.saving.set(true);
    this.performanceService.createGoal(this.goalForm.value).subscribe({
      next: () => { this.snackBar.open('Goal created', 'Close', { duration: 3000 }); this.router.navigate(['/performance/goals']); },
      error: () => { this.saving.set(false); this.snackBar.open('Failed to create goal', 'Close', { duration: 3000 }); },
    });
  }

  cancel(): void { this.router.navigate(['/performance/goals']); }
}
