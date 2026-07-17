import { Injectable, signal, computed, PLATFORM_ID, inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Theme, THEMES } from '../models/theme.model';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private platformId = inject(PLATFORM_ID);

  private readonly STORAGE_KEY = 'hrms_theme';

  themes = signal<Theme[]>(THEMES);
  currentTheme = signal<Theme>(THEMES[0]);
  currentThemeName = computed(() => this.currentTheme().name);
  isDarkMode = computed(() => this.currentTheme().isDark);

  constructor() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadSavedTheme();
    }
  }

  setTheme(themeName: string): void {
    const theme = THEMES.find(t => t.name === themeName);
    if (!theme) return;

    this.currentTheme.set(theme);
    this.applyTheme(theme);

    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem(this.STORAGE_KEY, themeName);
    }
  }

  toggleDarkMode(): void {
    const current = this.currentTheme();
    if (current.isDark) {
      this.setTheme('default');
    } else {
      this.setTheme('dark');
    }
  }

  private loadSavedTheme(): void {
    const saved = localStorage.getItem(this.STORAGE_KEY);
    if (saved) {
      this.setTheme(saved);
    } else {
      const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
      if (prefersDark) {
        this.setTheme('dark');
      } else {
        this.applyTheme(THEMES[0]);
      }
    }
  }

  private applyTheme(theme: Theme): void {
    if (!isPlatformBrowser(this.platformId)) return;

    const root = document.documentElement;

    root.style.setProperty('--hrms-primary', theme.primary);
    root.style.setProperty('--hrms-primary-light', theme.primaryLight);
    root.style.setProperty('--hrms-primary-dark', theme.primaryDark);
    root.style.setProperty('--hrms-secondary', theme.secondary);
    root.style.setProperty('--hrms-accent', theme.accent);
    root.style.setProperty('--hrms-success', theme.success);
    root.style.setProperty('--hrms-warning', theme.warning);
    root.style.setProperty('--hrms-danger', theme.danger);
    root.style.setProperty('--hrms-info', theme.info);
    root.style.setProperty('--hrms-bg', theme.bg);
    root.style.setProperty('--hrms-surface', theme.surface);
    root.style.setProperty('--hrms-surface-hover', theme.surfaceHover);
    root.style.setProperty('--hrms-text', theme.text);
    root.style.setProperty('--hrms-text-secondary', theme.textSecondary);
    root.style.setProperty('--hrms-border', theme.border);
    root.style.setProperty('--hrms-border-light', theme.borderLight);
    root.style.setProperty('--hrms-sidebar-bg', theme.sidebarBg);
    root.style.setProperty('--hrms-sidebar-text', theme.sidebarText);
    root.style.setProperty('--hrms-sidebar-hover', theme.sidebarHover);
    root.style.setProperty('--hrms-sidebar-active', theme.sidebarActive);
    root.style.setProperty('--hrms-header-bg', theme.headerBg);
    root.style.setProperty('--hrms-header-border', theme.headerBorder);
    root.style.setProperty('--hrms-card-bg', theme.cardBg);
    root.style.setProperty('--hrms-input-bg', theme.inputBg);
    root.style.setProperty('--hrms-input-border', theme.inputBorder);

    document.body.classList.remove('theme-dark', 'theme-light');
    document.body.classList.add(theme.isDark ? 'theme-dark' : 'theme-light');
  }
}
