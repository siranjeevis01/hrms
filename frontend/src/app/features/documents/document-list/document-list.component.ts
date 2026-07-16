import { TitleCasePipe, DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DocumentsService } from '../documents.service';
import { DocumentItem, DocumentFolder } from '../documents.models';

@Component({
  selector: 'app-document-list',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatMenuModule, MatProgressSpinnerModule, MatTooltipModule, TitleCasePipe, DatePipe],
  templateUrl: './document-list.component.html',
  styleUrl: './document-list.component.scss',
})
export class DocumentListComponent implements OnInit {
  private documentsService = inject(DocumentsService);
  protected router = inject(Router);

  documents = signal<DocumentItem[]>([]);
  folders = signal<DocumentFolder[]>([]);
  loading = signal(true);
  searchQuery = signal('');
  sortBy = signal('name');
  viewMode = signal<'grid' | 'list'>('grid');
  currentFolderId = signal<string | null>(null);
  breadcrumbs = signal<{ id: string | null; name: string }[]>([{ id: null, name: 'Root' }]);

  ngOnInit(): void { this.loadData(); }

  loadData(): void {
    this.loading.set(true);
    Promise.all([
      this.documentsService.getDocuments(this.currentFolderId() || undefined, this.searchQuery() || undefined, this.sortBy()).toPromise(),
      this.documentsService.getFolders(this.currentFolderId() || undefined).toPromise(),
    ]).then(([docs, folders]) => {
      this.documents.set(docs || []);
      this.folders.set(folders || []);
      this.loading.set(false);
    }).catch(() => this.loading.set(false));
  }

  navigateToFolder(folder: DocumentFolder): void {
    this.currentFolderId.set(folder.id);
    this.breadcrumbs.update((bc) => [...bc, { id: folder.id, name: folder.name }]);
    this.loadData();
  }

  navigateToBreadcrumb(index: number): void {
    const bc = this.breadcrumbs();
    this.currentFolderId.set(bc[index].id);
    this.breadcrumbs.set(bc.slice(0, index + 1));
    this.loadData();
  }

  formatSize(bytes: number): string {
    if (bytes === 0) return '0 B';
    const k = 1024;
    const sizes = ['B', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
  }

  getFileIcon(item: DocumentItem): string {
    if (item.type === 'folder') return 'folder';
    if (item.mimeType.startsWith('image/')) return 'image';
    if (item.mimeType.includes('pdf')) return 'picture_as_pdf';
    if (item.mimeType.includes('spreadsheet') || item.mimeType.includes('excel')) return 'table_chart';
    if (item.mimeType.includes('document') || item.mimeType.includes('word')) return 'description';
    if (item.mimeType.includes('presentation') || item.mimeType.includes('powerpoint')) return 'slideshow';
    return 'insert_drive_file';
  }

  downloadDocument(id: string): void {
    this.documentsService.downloadDocument(id).subscribe({
      next: (blob) => {
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = '';
        a.click();
        URL.revokeObjectURL(url);
      },
    });
  }

  deleteDocument(id: string): void {
    this.documentsService.deleteDocument(id).subscribe({ next: () => this.loadData() });
  }
}
