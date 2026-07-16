import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DocumentsService } from '../documents.service';
import { DocumentItem, DocumentFolder } from '../documents.models';

@Component({
  selector: 'app-folder',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './folder.component.html',
  styleUrl: './folder.component.scss',
})
export class FolderComponent implements OnInit {
  private route = inject(ActivatedRoute);
  protected router = inject(Router);
  private documentsService = inject(DocumentsService);

  folder = signal<DocumentFolder | null>(null);
  documents = signal<DocumentItem[]>([]);
  subfolders = signal<DocumentFolder[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id') || '';
    this.loadFolder(id);
  }

  loadFolder(id: string): void {
    this.loading.set(true);
    Promise.all([
      this.documentsService.getFolder(id).toPromise(),
      this.documentsService.getDocuments(id).toPromise(),
      this.documentsService.getFolders(id).toPromise(),
    ]).then(([folder, docs, folders]) => {
      this.folder.set(folder || null);
      this.documents.set(docs || []);
      this.subfolders.set(folders || []);
      this.loading.set(false);
    }).catch(() => this.loading.set(false));
  }

  goBack(): void { this.router.navigate(['/documents']); }
  formatSize(bytes: number): string {
    if (bytes === 0) return '0 B';
    const k = 1024, sizes = ['B', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
  }
}
