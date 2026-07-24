import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpEvent } from '@angular/common/http';
import { Observable, throwError, timer, Subject } from 'rxjs';
import { catchError, switchMap, finalize, retryWhen, take } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

const isWakingUp = { value: false };
const wakeUpComplete = new Subject<void>();
const wakeUpMessage = new Subject<string>();

export { wakeUpMessage as wakeUpMessage$ };

export const wakeUpInterceptor: HttpInterceptorFn = (req, next) => {
  // Skip wake-up for auth endpoints and the health check itself
  if (req.url.includes('/auth/login') || req.url.includes('/auth/register') ||
      req.url.includes('/auth/refresh') || req.url.includes('/health/live')) {
    return next(req);
  }

  return next(req).pipe(
    catchError((error: { status: number }) => {
      if (error.status === 0 && !isWakingUp.value) {
        return handleWakeUp(req, next);
      }
      return throwError(() => error);
    })
  );
};

function handleWakeUp(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {
  isWakingUp.value = true;
  wakeUpMessage.next('Server is waking up (free tier cold start), please wait...');

  // Render free tier takes 30-60s to wake up. Poll with retries.
  let retryCount = 0;
  const maxRetries = 6;
  const retryDelay = 10000; // 10 seconds between retries

  return timer(5000).pipe(
    switchMap(() => {
      wakeUpMessage.next(`Server is waking up... attempt ${retryCount + 1}/${maxRetries}`);
      return next(req).pipe(
        retryWhen(errors => {
          return errors.pipe(
            switchMap((err) => {
              retryCount++;
              if (retryCount >= maxRetries) {
                wakeUpMessage.next('Server is still waking up. Please refresh the page in a moment.');
                return throwError(() => err);
              }
              wakeUpMessage.next(`Server is still starting... retry ${retryCount}/${maxRetries}`);
              return timer(retryDelay);
            })
          );
        })
      );
    }),
    catchError((error: unknown) => {
      wakeUpMessage.next('Server may still be waking up. Try refreshing in 30-60 seconds.');
      return throwError(() => error);
    }),
    finalize(() => {
      isWakingUp.value = false;
      wakeUpComplete.next();
      wakeUpMessage.next('');
    })
  );
}
