export type DocumentType = 'file' | 'folder';

export interface DocumentItem {
  id: string;
  name: string;
  type: DocumentType;
  mimeType: string;
  size: number;
  folderId: string | null;
  url: string;
  uploadedBy: string;
  uploadedByName: string;
  createdAt: string;
  updatedAt: string;
  isShared: boolean;
  sharedWith: DocumentShare[];
}

export interface DocumentFolder {
  id: string;
  name: string;
  parentId: string | null;
  parentName: string | null;
  itemCount: number;
  totalSize: number;
  createdAt: string;
  updatedAt: string;
  color: string;
  icon: string;
}

export interface DocumentShare {
  id: string;
  userId: string;
  userName: string;
  permission: 'view' | 'edit' | 'admin';
  sharedAt: string;
}

export interface UploadDocumentRequest {
  file: File;
  folderId: string | null;
}

export interface ShareDocumentRequest {
  documentId: string;
  userId: string;
  permission: 'view' | 'edit' | 'admin';
}
