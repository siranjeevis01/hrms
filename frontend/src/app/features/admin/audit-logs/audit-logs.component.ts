import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatExpansionModule } from '@angular/material/expansion';
import { NgClass, DatePipe } from '@angular/common';
import { AdminService } from '../admin.service';
import { AuditLog } from '../admin.models';

@Component({
  selector: 'app-audit-logs',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatTableModule, MatExpansionModule, DatePipe, NgClass],
  templateUrl: './audit-logs.component.html',
  styleUrl: './audit-logs.component.scss',
})
export class AuditLogsComponent implements OnInit {
  private adminService = inject(AdminService);
  logs = signal<AuditLog[]>([]);
  loading = signal(true);
  searchQuery = signal('');
  actionFilter = signal('');
  entityTypeFilter = signal('');
  dateFrom = signal('');
  dateTo = signal('');
  displayedColumns = ['timestamp', 'userName', 'action', 'entityType', 'entityName', 'ipAddress'];

  ngOnInit(): void { this.loadLogs(); }

  loadLogs(): void {
    this.loading.set(true);
    this.adminService.getAuditLogs({
      search: this.searchQuery() || undefined,
      action: this.actionFilter() || undefined,
      entityType: this.entityTypeFilter() || undefined,
      dateFrom: this.dateFrom() || undefined,
      dateTo: this.dateTo() || undefined,
    }).subscribe({
      next: (logs) => { this.logs.set(logs); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  getActionColor(action: string): string {
    if (action.includes('Create')) return 'bg-green-100 text-green-700';
    if (action.includes('Update')) return 'bg-blue-100 text-blue-700';
    if (action.includes('Delete')) return 'bg-red-100 text-red-700';
    return 'bg-gray-100 text-gray-700';
  }
}
