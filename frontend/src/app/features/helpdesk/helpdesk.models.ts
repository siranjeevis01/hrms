export type TicketStatus = 'open' | 'in_progress' | 'resolved' | 'closed';
export type TicketPriority = 'low' | 'medium' | 'high' | 'urgent';

export interface Ticket {
  id: string;
  subject: string;
  description: string;
  status: TicketStatus;
  priority: TicketPriority;
  category: TicketCategory;
  employeeId: string;
  employeeName: string;
  assignedTo: string | null;
  assignedToName: string | null;
  attachments: TicketAttachment[];
  comments: TicketComment[];
  createdAt: string;
  updatedAt: string;
  resolvedAt: string | null;
}

export interface TicketCategory {
  id: string;
  name: string;
  icon: string;
  color: string;
}

export interface TicketComment {
  id: string;
  author: string;
  authorName: string;
  content: string;
  createdAt: string;
  isInternal: boolean;
}

export interface TicketAttachment {
  id: string;
  name: string;
  url: string;
  size: number;
  uploadedAt: string;
}

export interface KnowledgeArticle {
  id: string;
  title: string;
  content: string;
  category: string;
  author: string;
  authorName: string;
  views: number;
  helpful: number;
  tags: string[];
  publishedAt: string;
  updatedAt: string;
}

export interface Faq {
  id: string;
  question: string;
  answer: string;
  category: string;
  helpful: number;
  views: number;
  createdAt: string;
}

export interface HelpdeskStats {
  open: number;
  inProgress: number;
  resolved: number;
  closed: number;
  total: number;
  averageResolutionTime: number;
  satisfactionRate: number;
}

export interface CreateTicketRequest {
  subject: string;
  description: string;
  categoryId: string;
  priority: TicketPriority;
  attachments: File[];
}

export interface TicketFilters {
  status?: TicketStatus;
  priority?: TicketPriority;
  categoryId?: string;
  search?: string;
  dateFrom?: string;
  dateTo?: string;
}
