import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { authInterceptor } from './auth.interceptor';

describe('authInterceptor', () => {
  let httpClient: HttpClient;
  let httpTesting: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(withInterceptors([authInterceptor])),
        provideHttpClientTesting()
      ]
    });
    httpClient = TestBed.inject(HttpClient);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should add Authorization header when token exists', () => {
    spyOn(localStorage, 'getItem').withArgs('hrms_token').and.returnValue('Bearer test-token');

    httpClient.get('/api/data').subscribe();

    const req = httpTesting.expectOne('/api/data');
    expect(req.request.headers.has('Authorization')).toBeTrue();
    req.flush({});
  });

  it('should not add Authorization for auth endpoints', () => {
    spyOn(localStorage, 'getItem').withArgs('hrms_token').and.returnValue('Bearer test-token');

    httpClient.get('/auth/login').subscribe();

    const req = httpTesting.expectOne('/auth/login');
    expect(req.request.headers.has('Authorization')).toBeFalse();
    req.flush({});
  });

  it('should not add Authorization when no token', () => {
    spyOn(localStorage, 'getItem').withArgs('hrms_token').and.returnValue(null);

    httpClient.get('/api/data').subscribe();

    const req = httpTesting.expectOne('/api/data');
    expect(req.request.headers.has('Authorization')).toBeFalse();
    req.flush({});
  });
});
