import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { TitleCasePipe } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TrainingService } from '../training.service';
import { Course, CourseDifficulty } from '../training.models';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatProgressSpinnerModule, MatSnackBarModule, TitleCasePipe,
  ],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.scss',
})
export class CourseListComponent implements OnInit {
  private trainingService = inject(TrainingService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  courses = signal<Course[]>([]);
  loading = signal(true);
  searchQuery = '';
  filterCategory = '';
  filterDifficulty = '';

  categories = signal<string[]>([]);

  filteredCourses = computed(() => {
    let result = this.courses();
    if (this.searchQuery) {
      const q = this.searchQuery.toLowerCase();
      result = result.filter(c => c.title.toLowerCase().includes(q) || c.instructor.toLowerCase().includes(q));
    }
    if (this.filterCategory) result = result.filter(c => c.category === this.filterCategory);
    if (this.filterDifficulty) result = result.filter(c => c.difficulty === this.filterDifficulty);
    return result;
  });

  ngOnInit(): void { this.loadCourses(); }

  loadCourses(): void {
    this.loading.set(true);
    this.trainingService.getCourses().subscribe({
      next: (courses) => {
        this.courses.set(courses);
        this.categories.set([...new Set(courses.map(c => c.category))]);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  navigateTo(path: string): void { this.router.navigate(['/training', path]); }

  enroll(courseId: string, event: Event): void {
    event.stopPropagation();
    this.trainingService.enrollInCourse(courseId).subscribe({
      next: () => this.snackBar.open('Enrolled successfully', 'Close', { duration: 3000 }),
      error: () => this.snackBar.open('Failed to enroll', 'Close', { duration: 3000 }),
    });
  }

  getRatingStars(rating: number): number[] { return Array.from({ length: 5 }, (_, i) => i < Math.round(rating) ? 1 : 0); }

  getDifficultyColor(diff: string): string {
    const c: Record<string, string> = { beginner: 'bg-green-100 text-green-800', intermediate: 'bg-amber-100 text-amber-800', advanced: 'bg-red-100 text-red-800' };
    return c[diff] || 'bg-gray-100 text-gray-800';
  }
}
