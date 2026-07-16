import { Component, Input, Output, EventEmitter, ViewChild, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, type MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, type PageEvent } from '@angular/material/paginator';
import { MatSortModule, type Sort } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EmptyStateComponent } from '../empty-state/empty-state';

export interface ColumnDef {
  key: string;
  header: string;
  sortable?: boolean;
  type?: 'text' | 'date' | 'currency' | 'status' | 'image';
  class?: string;
}

export interface SortEvent {
  active: string;
  direction: 'asc' | 'desc' | '';
}

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    EmptyStateComponent,
  ],
  templateUrl: './data-table.html',
  styleUrl: './data-table.scss',
})
export class DataTableComponent implements OnChanges {
  @Input() columns: ColumnDef[] = [];
  @Input() dataSource: unknown[] = [];
  @Input() totalCount: number = 0;
  @Input() pageSize: number = 10;
  @Input() loading: boolean = false;
  @Input() emptyMessage: string = 'No data available';
  @Input() emptyIcon: string = 'table_chart';
  @Input() pageSizeOptions: number[] = [5, 10, 25, 50];
  @Input() showPaginator: boolean = true;
  @Input() rowClass: string = '';

  row: any;

  @Output() sortChange = new EventEmitter<SortEvent>();
  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() rowClick = new EventEmitter<unknown>();

  displayedColumns: string[] = [];

  ngOnChanges(): void {
    this.displayedColumns = this.columns.map((c) => c.key);
  }

  onSortChange(sort: Sort): void {
    this.sortChange.emit({ active: sort.active, direction: sort.direction });
  }

  onPageChange(event: PageEvent): void {
    this.pageChange.emit(event);
  }

  onRowClick(row: unknown): void {
    this.rowClick.emit(row);
  }

  getCellValue(row: unknown, columnKey: string): unknown {
    const record = row as Record<string, unknown>;
    return record[columnKey];
  }

  formatDate(value: unknown): string {
    if (!value) return '';
    return new Date(value as string).toLocaleDateString();
  }

  formatCurrency(value: unknown): string {
    if (value == null) return '$0.00';
    return `$${(value as number).toLocaleString('en-US', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    })}`;
  }

  getStatusClass(value: unknown): string {
    const str = String(value).toLowerCase();
    const statusMap: Record<string, string> = {
      active: 'status-active',
      inactive: 'status-inactive',
      pending: 'status-pending',
      approved: 'status-approved',
      rejected: 'status-rejected',
      completed: 'status-completed',
      'in progress': 'status-in-progress',
      draft: 'status-draft',
    };
    return statusMap[str] || 'status-default';
  }
}
