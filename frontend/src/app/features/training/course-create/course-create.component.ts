import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TrainingService } from '../training.service';

@Component({
  selector: 'app-course-create',
  standalone: true,
  imports: [
    ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatStepperModule, MatProgressSpinnerModule, MatSnackBarModule, TitleCasePipe,
  ],
  templateUrl: './course-create.component.html',
  styleUrl: './course-create.component.scss',
})
export class CourseCreateComponent {
  private fb = inject(FormBuilder);
  private trainingService = inject(TrainingService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  saving = signal(false);
  currentStep = 0;

  basicForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', Validators.required],
    category: ['', Validators.required],
    difficulty: ['beginner', Validators.required],
    duration: [60, [Validators.required, Validators.min(1)]],
    instructor: ['', Validators.required],
  });

  modulesForm = this.fb.group({
    modules: this.fb.array([]),
  });

  assessmentForm = this.fb.group({
    assessmentTitle: [''],
    questions: this.fb.array([]),
  });

  get modules(): FormArray { return this.modulesForm.get('modules') as FormArray; }
  get assessmentQuestions(): FormArray { return this.assessmentForm.get('questions') as FormArray; }

  addModule(): void {
    this.modules.push(this.fb.group({
      title: ['', Validators.required],
      lessons: this.fb.array([]),
    }));
  }

  removeModule(index: number): void { this.modules.removeAt(index); }

  addLesson(moduleIndex: number): void {
    const lessons = this.modules.at(moduleIndex).get('lessons') as FormArray;
    lessons.push(this.fb.group({
      title: ['', Validators.required],
      type: ['video', Validators.required],
      content: [''],
      duration: [10],
    }));
  }

  removeLesson(moduleIndex: number, lessonIndex: number): void {
    const lessons = this.modules.at(moduleIndex).get('lessons') as FormArray;
    lessons.removeAt(lessonIndex);
  }

  addAssessmentQuestion(): void {
    this.assessmentQuestions.push(this.fb.group({
      question: ['', Validators.required],
      options: this.fb.array(['', '', '', '']),
      correctAnswer: [0],
    }));
  }

  removeAssessmentQuestion(index: number): void { this.assessmentQuestions.removeAt(index); }

  nextStep(): void { if (this.currentStep < 3) this.currentStep++; }
  prevStep(): void { if (this.currentStep > 0) this.currentStep--; }

  publish(): void {
    this.saving.set(true);
    const courseData = { ...this.basicForm.value, modules: this.modules.value, status: 'published' };
    this.trainingService.createCourse(courseData).subscribe({
      next: () => { this.snackBar.open('Course created', 'Close', { duration: 3000 }); this.router.navigate(['/training/courses']); },
      error: () => { this.saving.set(false); this.snackBar.open('Failed', 'Close', { duration: 3000 }); },
    });
  }

  cancel(): void { this.router.navigate(['/training/courses']); }
}
