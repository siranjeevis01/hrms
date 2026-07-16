import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, throwError, timer, Subject } from 'rxjs';
import { catchError, switchMap, finalize } from 'rxjs/operators';
import { environment } from '../../environments/environment';

const isWakingUp = { value: false };
const wakeUpComplete = new Subject<void>();
const wakeUpMessage = new Subject<string>();

export { wakeUpMessage as wakeUpMessage$ };

export const wakeUpInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: { status: number }) => {
      if (error.status === 0 && !isWakingUp.value && !environment.production) {
        return handleWakeUp(req, next);
      }
      return throwError(() => error);
    })
  );
};

function handleWakeUp(req: Parameters<HttpInterceptorFn>[0], next: Parameters<HttpInterceptorFn>[1]: Observable<unknown> {
  isWakingUp.value = true;
  wakeUpMessage.next('Server is waking up, please wait approximately 60 seconds...');

  return timer(65000).pipe(
    switchMap(() => {
      wakeUpMessage.next('Retrying request...');
      return next(req);
    }),
    catchError((error: unknown) => {
      wakeUpMessage.next('Server is still waking up. Please refresh the page in a moment.');
      return throwError(() => error);
    }),
    finalize(() => {
      isWakingUp.value = false;
      wakeUpComplete.next();
      wakeUpMessage.next('');
    })
  );
}
