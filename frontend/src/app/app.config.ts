import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { routes } from './app.routes';
import {
  authInterceptor,
  errorInterceptor,
  loadingInterceptor,
  tenantInterceptor,
  wakeUpInterceptor,
} from './core/interceptors';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withComponentInputBinding()),
    provideHttpClient(
      withInterceptors([authInterceptor, wakeUpInterceptor, errorInterceptor, loadingInterceptor, tenantInterceptor])
    ),
    provideAnimationsAsync(),
    provideCharts(withDefaultRegisterables()),
  ],
};
