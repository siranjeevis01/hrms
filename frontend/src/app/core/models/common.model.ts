export interface DropdownOption {
  value: string | number;
  label: string;
}

export interface SelectItem {
  id: string | number;
  name: string;
}

export interface DateRange {
  startDate: Date;
  endDate: Date;
}

export interface PaginationParams {
  pageNumber: number;
  pageSize: number;
  search?: string;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
}

export interface AppNotification {
  id: string;
  title: string;
  message: string;
  type: 'info' | 'success' | 'warning' | 'error';
  isRead: boolean;
  createdAt: Date;
  link?: string;
}
