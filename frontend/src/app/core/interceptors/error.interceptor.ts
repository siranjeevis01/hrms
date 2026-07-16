import { HttpRequest, HttpHandlerFn, HttpInterceptorFn, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';

let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const errorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const toastService = inject(ToastService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (
        error.status === 401 &&
        !req.url.includes('/auth/login') &&
        !req.url.includes('/auth/register') &&
        !req.url.includes('/auth/refresh')
      ) {
        return handle401Error(req, next, authService, router);
      }

      if (error.status === 403) {
        toastService.error('You do not have permission to perform this action');
      } else if (error.status === 404) {
        toastService.error('The requested resource was not found');
      } else if (error.status >= 500) {
        toastService.error('An unexpected error occurred. Please try again later.');
      } else if (error.error?.message) {
        toastService.error(error.error.message);
      }

      return throwError(() => error);
    })
  );
};

function handle401Error(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
  authService: AuthService,
  router: Router
): Observable<HttpEvent<unknown>> {
  if (!isRefreshing) {
    isRefreshing = true;
    refreshTokenSubject.next(null);

    return authService.refreshToken().pipe(
      switchMap((response) => {
        isRefreshing = false;
        refreshTokenSubject.next(response.token);
        const clonedReq = req.clone({
          setHeaders: { Authorization: `Bearer ${response.token}` },
        });
        return next(clonedReq);
      }),
      catchError((refreshError) => {
        isRefreshing = false;
        refreshTokenSubject.next(null);
        authService.logout();
        router.navigate(['/auth/login']);
        return throwError(() => refreshError);
      })
    );
  }

  return refreshTokenSubject.pipe(
    filter((token) => token !== null),
    take(1),
    switchMap((token) => {
      const clonedReq = req.clone({
        setHeaders: { Authorization: `Bearer ${token}` },
      });
      return next(clonedReq);
    })
  );
}
