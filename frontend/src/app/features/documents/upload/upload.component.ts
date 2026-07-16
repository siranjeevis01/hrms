import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { DocumentsService } from '../documents.service';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatProgressBarModule],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.scss',
})
export class UploadComponent {
  private documentsService = inject(DocumentsService);
  protected router = inject(Router);

  files = signal<{ file: File; progress: number; status: 'pending' | 'uploading' | 'done' | 'error' }[]>([]);
  folderId = signal<string>('');
  isDragOver = signal(false);
  uploading = signal(false);

  onDragOver(event: DragEvent): void { event.preventDefault(); this.isDragOver.set(true); }
  onDragLeave(): void { this.isDragOver.set(false); }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    this.isDragOver.set(false);
    if (event.dataTransfer?.files) {
      this.addFiles(event.dataTransfer.files);
    }
  }

  onFileSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) this.addFiles(input.files);
  }

  addFiles(fileList: FileList): void {
    const newFiles = Array.from(fileList).map((file) => ({ file, progress: 0, status: 'pending' as const }));
    this.files.update((f) => [...f, ...newFiles]);
  }

  removeFile(index: number): void { this.files.update((f) => f.filter((_, i) => i !== index)); }

  uploadAll(): void {
    this.uploading.set(true);
    const uploads = this.files().filter((f) => f.status === 'pending').map((f, i) => {
      f.status = 'uploading';
      return this.documentsService.uploadDocument({ file: f.file, folderId: this.folderId() || null }).toPromise()
        .then(() => { this.files.update((files) => files.map((file, idx) => idx === i ? { ...file, progress: 100, status: 'done' as const } : file)); })
        .catch(() => { this.files.update((files) => files.map((file, idx) => idx === i ? { ...file, status: 'error' as const } : file)); });
    });
    Promise.all(uploads).then(() => {
      this.uploading.set(false);
      this.router.navigate(['/documents']);
    });
  }

  formatSize(bytes: number): string {
    if (bytes === 0) return '0 B';
    const k = 1024, sizes = ['B', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
  }

  get hasPendingFiles(): boolean { return this.files().some((f) => f.status === 'pending'); }
}
