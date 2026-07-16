import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const tenantInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const user = authService.getCurrentUser();

  if (user?.tenantId) {
    const clonedReq = req.clone({
      setHeaders: { 'X-Tenant-Id': user.tenantId },
    });
    return next(clonedReq);
  }

  return next(req);
};
