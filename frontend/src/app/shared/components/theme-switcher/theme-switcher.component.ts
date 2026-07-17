import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeService } from '../../../core/services/theme.service';

@Component({
  selector: 'app-theme-switcher',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="theme-switcher">
      <button class="theme-switcher__trigger" (click)="toggleDropdown()" title="Change theme">
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <circle cx="12" cy="12" r="4"/>
          <path d="M12 2v2"/>
          <path d="M12 20v2"/>
          <path d="m4.93 4.93 1.41 1.41"/>
          <path d="m17.66 17.66 1.41 1.41"/>
          <path d="M2 12h2"/>
          <path d="M20 12h2"/>
          <path d="m6.34 17.66-1.41 1.41"/>
          <path d="m19.07 4.93-1.41 1.41"/>
        </svg>
        <span class="theme-switcher__label">Theme</span>
      </button>

      @if (isOpen) {
        <div class="theme-switcher__dropdown">
          <div class="theme-switcher__dropdown-header">Select Theme</div>
          @for (theme of themeService.themes(); track theme.name) {
            <button
              class="theme-switcher__option"
              [class.theme-switcher__option--active]="theme.name === themeService.currentThemeName()"
              (click)="selectTheme(theme.name)">
              <span class="theme-switcher__color-swatch" [style.background]="theme.primary"></span>
              <span class="theme-switcher__option-label">{{ theme.displayName }}</span>
              @if (theme.name === themeService.currentThemeName()) {
                <svg class="theme-switcher__check" xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round">
                  <polyline points="20 6 9 17 4 12"/>
                </svg>
              }
            </button>
          }
        </div>
      }
    </div>
  `,
  styles: [`
    .theme-switcher { position: relative; }
    .theme-switcher__trigger {
      display: flex; align-items: center; gap: 6px;
      padding: 6px 12px; border-radius: 8px;
      border: 1px solid var(--hrms-border, #E5E7EB);
      background: var(--hrms-surface, #FFFFFF);
      color: var(--hrms-text, #1F2937);
      cursor: pointer; font-size: 13px; font-weight: 500;
      transition: all 0.2s;
    }
    .theme-switcher__trigger:hover { border-color: var(--hrms-primary, #4F46E5); background: var(--hrms-surface-hover, #F3F4F6); }
    .theme-switcher__dropdown {
      position: absolute; top: calc(100% + 8px); right: 0; z-index: 1000;
      min-width: 220px; border-radius: 12px; padding: 8px;
      background: var(--hrms-surface, #FFFFFF);
      border: 1px solid var(--hrms-border, #E5E7EB);
      box-shadow: 0 10px 25px rgba(0,0,0,0.12);
    }
    .theme-switcher__dropdown-header {
      padding: 8px 12px; font-size: 11px; font-weight: 600;
      text-transform: uppercase; letter-spacing: 0.05em;
      color: var(--hrms-text-secondary, #6B7280);
    }
    .theme-switcher__option {
      display: flex; align-items: center; gap: 10px;
      width: 100%; padding: 8px 12px; border: none; border-radius: 8px;
      background: transparent; cursor: pointer; font-size: 13px;
      color: var(--hrms-text, #1F2937); transition: background 0.15s;
    }
    .theme-switcher__option:hover { background: var(--hrms-surface-hover, #F3F4F6); }
    .theme-switcher__option--active { background: var(--hrms-surface-hover, #F3F4F6); font-weight: 600; }
    .theme-switcher__color-swatch { width: 16px; height: 16px; border-radius: 50%; flex-shrink: 0; border: 2px solid rgba(0,0,0,0.1); }
    .theme-switcher__option-label { flex: 1; text-align: left; }
    .theme-switcher__check { color: var(--hrms-primary, #4F46E5); flex-shrink: 0; }
  `],
})
export class ThemeSwitcherComponent {
  themeService = inject(ThemeService);
  isOpen = false;

  toggleDropdown(): void { this.isOpen = !this.isOpen; }

  selectTheme(name: string): void {
    this.themeService.setTheme(name);
    this.isOpen = false;
  }
}
