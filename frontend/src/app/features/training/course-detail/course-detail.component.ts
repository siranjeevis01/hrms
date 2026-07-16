import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TrainingService } from '../training.service';
import { Course } from '../training.models';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatExpansionModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatChipsModule, MatSnackBarModule,
  ],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.scss',
})
export class CourseDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private trainingService = inject(TrainingService);
  private snackBar = inject(MatSnackBar);

  course = signal<Course | null>(null);
  loading = signal(true);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadCourse(id);
  }

  loadCourse(id: string): void {
    this.loading.set(true);
    this.trainingService.getCourse(id).subscribe({
      next: (course) => { this.course.set(course); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  enroll(): void {
    const c = this.course();
    if (!c) return;
    this.trainingService.enrollInCourse(c.id).subscribe({
      next: () => { this.snackBar.open('Enrolled successfully', 'Close', { duration: 3000 }); },
      error: () => this.snackBar.open('Failed to enroll', 'Close', { duration: 3000 }),
    });
  }

  getRatingStars(rating: number): number[] { return Array.from({ length: 5 }, (_, i) => i < Math.round(rating) ? 1 : 0); }
  goBack(): void { this.router.navigate(['/training/courses']); }
}
