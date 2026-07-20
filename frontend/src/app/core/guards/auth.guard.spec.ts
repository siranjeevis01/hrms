import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { authGuard } from './auth.guard';

describe('authGuard', () => {
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(() => {
    mockRouter = jasmine.createSpyObj('Router', ['createUrlTree']);
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        { provide: Router, useValue: mockRouter }
      ]
    });
  });

  it('should allow activation when user is authenticated', () => {
    spyOn(localStorage, 'getItem').withArgs('hrms_token').and.returnValue('token');
    const route = {} as ActivatedRouteSnapshot;
    const state = {} as RouterStateSnapshot;
    const result = TestBed.runInInjectionContext(() => authGuard(route, state));
    expect(result).toBeTrue();
  });

  it('should redirect to login when user is not authenticated', () => {
    spyOn(localStorage, 'getItem').withArgs('hrms_token').and.returnValue(null);
    const route = {} as ActivatedRouteSnapshot;
    const state = {} as RouterStateSnapshot;
    const urlTree = {} as ReturnType<Router['createUrlTree']>;
    mockRouter.createUrlTree.and.returnValue(urlTree);
    const result = TestBed.runInInjectionContext(() => authGuard(route, state));
    expect(mockRouter.createUrlTree).toHaveBeenCalledWith(['/auth/login']);
    expect(result).toBe(urlTree);
  });
});
