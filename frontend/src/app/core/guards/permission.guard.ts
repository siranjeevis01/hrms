import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';

export const permissionGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const toastService = inject(ToastService);

  const requiredPermissions = route.data['permissions'] as string[] | undefined;
  if (!requiredPermissions || requiredPermissions.length === 0) {
    return true;
  }

  const hasPermission = requiredPermissions.some((perm) => authService.hasPermission(perm));
  if (hasPermission) {
    return true;
  }

  toastService.error('You do not have the required permissions to access this page');
  return router.createUrlTree(['/dashboard']);
};
