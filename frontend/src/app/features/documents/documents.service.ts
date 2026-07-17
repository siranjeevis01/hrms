import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  DocumentItem,
  DocumentFolder,
  UploadDocumentRequest,
  ShareDocumentRequest,
} from './documents.models';

@Injectable({ providedIn: 'root' })
export class DocumentsService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/documents/Documents`;

  getDocuments(folderId?: string, search?: string, sort?: string): Observable<DocumentItem[]> {
    let params = new HttpParams();
    if (folderId) params = params.set('folderId', folderId);
    if (search) params = params.set('search', search);
    if (sort) params = params.set('sort', sort);
    return this.http.get<DocumentItem[]>(this.apiUrl, { params });
  }

  getDocument(id: string): Observable<DocumentItem> {
    return this.http.get<DocumentItem>(`${this.apiUrl}/${id}`);
  }

  getFolders(parentId?: string): Observable<DocumentFolder[]> {
    let params = new HttpParams();
    if (parentId) params = params.set('parentId', parentId);
    return this.http.get<DocumentFolder[]>(`${this.apiUrl}/folders`, { params });
  }

  getFolder(id: string): Observable<DocumentFolder> {
    return this.http.get<DocumentFolder>(`${this.apiUrl}/folders/${id}`);
  }

  createFolder(name: string, parentId: string | null): Observable<DocumentFolder> {
    return this.http.post<DocumentFolder>(`${this.apiUrl}/folders`, { name, parentId });
  }

  deleteFolder(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/folders/${id}`);
  }

  uploadDocument(request: UploadDocumentRequest): Observable<DocumentItem> {
    const formData = new FormData();
    formData.append('file', request.file);
    if (request.folderId) formData.append('folderId', request.folderId);
    return this.http.post<DocumentItem>(`${this.apiUrl}/upload`, formData);
  }

  downloadDocument(id: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/${id}/download`, { responseType: 'blob' });
  }

  deleteDocument(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  shareDocument(request: ShareDocumentRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${request.documentId}/share`, {
      userId: request.userId,
      permission: request.permission,
    });
  }

  removeShare(documentId: string, userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${documentId}/share/${userId}`);
  }
}
