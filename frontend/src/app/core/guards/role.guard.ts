import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const toastService = inject(ToastService);

  const expectedRoles = route.data['roles'] as string[] | undefined;
  if (!expectedRoles || expectedRoles.length === 0) {
    return true;
  }

  const hasRole = expectedRoles.some((role) => authService.hasRole(role));
  if (hasRole) {
    return true;
  }

  toastService.error('You do not have the required role to access this page');
  return router.createUrlTree(['/dashboard']);
};
