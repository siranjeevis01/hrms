import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const tenantInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  if (req.url.includes('/auth/')) {
    return next(req);
  }

  if (token) {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const tenantId = payload['tenant_id'];
      if (tenantId) {
        const clonedReq = req.clone({
          setHeaders: { 'X-Tenant-Id': tenantId },
        });
        return next(clonedReq);
      }
    } catch {
      // Invalid token format, proceed without tenant header
    }
  }

  return next(req);
};
