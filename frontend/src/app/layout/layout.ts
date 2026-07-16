import { Component, OnInit, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { SidebarComponent } from './sidebar/sidebar';
import { HeaderComponent } from './header/header';
import { LoadingService } from '../core/services/loading.service';
import { NotificationService } from '../core/services/notification.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatSidenavModule, SidebarComponent, HeaderComponent],
  templateUrl: './layout.html',
  styleUrl: './layout.scss',
})
export class LayoutComponent implements OnInit {
  private breakpointObserver = inject(BreakpointObserver);
  loadingService = inject(LoadingService);
  private notificationService = inject(NotificationService);

  sidebarOpened = signal(true);
  isMobile = signal(false);

  ngOnInit(): void {
    this.breakpointObserver.observe([Breakpoints.Handset, '(max-width: 768px)']).subscribe((result) => {
      this.isMobile.set(result.matches);
      if (result.matches) {
        this.sidebarOpened.set(false);
      } else {
        this.sidebarOpened.set(true);
      }
    });

    this.notificationService.startConnection();
  }

  toggleSidebar(): void {
    this.sidebarOpened.update((v) => !v);
  }

  closeSidebar(): void {
    if (this.isMobile()) {
      this.sidebarOpened.set(false);
    }
  }
}
