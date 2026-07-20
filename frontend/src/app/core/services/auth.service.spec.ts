import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthService, provideHttpClient()]
    });
    service = TestBed.inject(AuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return null for getCurrentUser when not logged in', () => {
    expect(service.getCurrentUser()).toBeNull();
  });

  it('should return false for isAuthenticated when no token', () => {
    expect(service.isAuthenticated()).toBeFalse();
  });

  it('should return null for getToken when no token', () => {
    expect(service.getToken()).toBeNull();
  });
});
