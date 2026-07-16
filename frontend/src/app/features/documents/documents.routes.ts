import { Routes } from '@angular/router';

export const DOCUMENTS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./document-list/document-list.component').then((m) => m.DocumentListComponent),
  },
  {
    path: 'folders/:id',
    loadComponent: () =>
      import('./folder/folder.component').then((m) => m.FolderComponent),
  },
  {
    path: 'upload',
    loadComponent: () =>
      import('./upload/upload.component').then((m) => m.UploadComponent),
  },
];
