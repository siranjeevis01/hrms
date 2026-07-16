import { DecimalPipe, TitleCasePipe, DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { EmployeeService } from '../employee.service';
import { DocumentDto } from '../employee.models';

@Component({
  selector: 'app-employee-documents',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatInputModule,
    MatFormFieldModule,
    RouterLink,
    DecimalPipe,
    TitleCasePipe,
    DatePipe,
  ],
  templateUrl: './employee-documents.component.html',
  styleUrl: './employee-documents.component.scss',
})
export class EmployeeDocumentsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private employeeService = inject(EmployeeService);
  private snackBar = inject(MatSnackBar);

  documents = signal<DocumentDto[]>([]);
  loading = signal(true);
  uploading = signal(false);
  employeeId = signal('');
  draggedOver = signal(false);

  displayedColumns = ['name', 'type', 'uploadDate', 'fileSize', 'actions'];

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.employeeId.set(id);
      this.loadDocuments(id);
    }
  }

  loadDocuments(id: string): void {
    this.loading.set(true);
    this.employeeService.getEmployeeDocuments(id).subscribe({
      next: (data) => {
        this.documents.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Failed to load documents', 'Close', { duration: 3000 });
      },
    });
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.draggedOver.set(true);
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.draggedOver.set(false);
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.draggedOver.set(false);

    const files = event.dataTransfer?.files;
    if (files?.length) {
      this.uploadFile(files[0]);
    }
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.uploadFile(input.files[0]);
    }
  }

  uploadFile(file: File): void {
    this.uploading.set(true);
    this.employeeService.uploadDocument(this.employeeId(), file).subscribe({
      next: () => {
        this.snackBar.open('Document uploaded successfully', 'Close', { duration: 3000 });
        this.loadDocuments(this.employeeId());
        this.uploading.set(false);
      },
      error: () => {
        this.uploading.set(false);
        this.snackBar.open('Failed to upload document', 'Close', { duration: 3000 });
      },
    });
  }

  formatFileSize(bytes: number): string {
    if (bytes < 1024) return bytes + ' B';
    if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
    return (bytes / 1048576).toFixed(1) + ' MB';
  }

  goBack(): void {
    this.router.navigate(['/employees', this.employeeId()]);
  }
}
