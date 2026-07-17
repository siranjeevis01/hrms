import { Component, Output, EventEmitter, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';

interface NavItem {
  icon: string;
  label: string;
  route: string;
}

interface NavCategory {
  label: string;
  icon: string;
  items: NavItem[];
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatExpansionModule,
    MatTooltipModule,
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class SidebarComponent {
  @Output() closeSidebar = new EventEmitter<void>();

  expandedCategories = signal<Record<string, boolean>>({
    Main: true,
    People: false,
    Workforce: false,
    Finance: false,
    Talent: false,
    Projects: false,
    Collaboration: false,
    Analytics: false,
    Admin: false,
  });

  categories: NavCategory[] = [
    {
      label: 'Main',
      icon: 'home',
      items: [
        { icon: 'dashboard', label: 'Dashboard', route: '/dashboard' },
        { icon: 'person', label: 'My Profile', route: '/profile' },
      ],
    },
    {
      label: 'People',
      icon: 'people',
      items: [
        { icon: 'badge', label: 'Employees', route: '/employees' },
        { icon: 'account_tree', label: 'Organization', route: '/admin' },
      ],
    },
    {
      label: 'Workforce',
      icon: 'schedule',
      items: [
        { icon: 'event_available', label: 'Attendance', route: '/attendance' },
        { icon: 'beach_access', label: 'Leave', route: '/leave' },
      ],
    },
    {
      label: 'Finance',
      icon: 'payments',
      items: [
        { icon: 'account_balance', label: 'Payroll', route: '/payroll' },
        { icon: 'receipt_long', label: 'Expenses', route: '/expenses' },
        { icon: 'flight', label: 'Travel', route: '/travel' },
      ],
    },
    {
      label: 'Talent',
      icon: 'star',
      items: [
        { icon: 'work', label: 'Recruitment', route: '/recruitment' },
        { icon: 'school', label: 'Training', route: '/training' },
        { icon: 'trending_up', label: 'Performance', route: '/performance' },
      ],
    },
    {
      label: 'Projects',
      icon: 'folder',
      items: [
        { icon: 'cases', label: 'Projects', route: '/projects' },
        { icon: 'task_alt', label: 'Tasks', route: '/projects' },
        { icon: 'timer', label: 'Timesheet', route: '/projects' },
      ],
    },
    {
      label: 'Collaboration',
      icon: 'forum',
      items: [
        { icon: 'chat', label: 'Chat', route: '/chat' },
        { icon: 'folder_open', label: 'Documents', route: '/documents' },
        { icon: 'support_agent', label: 'Helpdesk', route: '/helpdesk' },
      ],
    },
    {
      label: 'Analytics',
      icon: 'analytics',
      items: [
        { icon: 'assessment', label: 'Reports', route: '/reports' },
        { icon: 'insert_chart', label: 'Dashboards', route: '/reports' },
      ],
    },
    {
      label: 'Admin',
      icon: 'admin_panel_settings',
      items: [
        { icon: 'settings', label: 'Settings', route: '/admin/settings' },
        { icon: 'history', label: 'Audit Logs', route: '/admin/audit-logs' },
        { icon: 'account_tree', label: 'Workflow', route: '/admin/workflow' },
      ],
    },
  ];

  toggleCategory(label: string): void {
    this.expandedCategories.update((current) => ({
      ...current,
      [label]: !current[label],
    }));
  }

  isCategoryExpanded(label: string): boolean {
    return this.expandedCategories()[label] ?? false;
  }
}
