import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  User,
  LoginRequest,
  RegisterRequest,
  AuthResponse,
  ChangePasswordRequest,
  PasswordResetRequest,
} from '../models/auth.model';
import {
  getAuth,
  signInWithPopup,
  GoogleAuthProvider,
  OAuthProvider,
  GithubAuthProvider,
  signOut,
  type UserCredential,
} from 'firebase/auth';
import { getFirebaseAuth } from '../config/firebase.config';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  private readonly TOKEN_KEY = 'hrms_token';
  private readonly REFRESH_TOKEN_KEY = 'hrms_refresh_token';
  private readonly USER_KEY = 'hrms_user';

  login(email: string, password: string): Observable<AuthResponse> {
    const request: LoginRequest = { email, password };
    return this.http
      .post<AuthResponse>(`${environment.apiUrl}/api/identity/auth/login`, request)
      .pipe(tap((response) => this.storeAuthData(response)));
  }

  register(request: RegisterRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${environment.apiUrl}/api/identity/auth/register`, request)
      .pipe(tap((response) => this.storeAuthData(response)));
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
    const auth = getFirebaseAuth();
    signOut(auth).catch(() => {});
    this.router.navigate(['/auth/login']);
  }

  refreshToken(): Observable<AuthResponse> {
    const refreshToken = this.getRefreshToken();
    return this.http
      .post<AuthResponse>(`${environment.apiUrl}/api/identity/auth/refresh`, { refreshToken })
      .pipe(tap((response) => this.storeAuthData(response)));
  }

  getCurrentUser(): User | null {
    const userJson = localStorage.getItem(this.USER_KEY);
    if (!userJson) return null;
    try {
      return JSON.parse(userJson) as User;
    } catch {
      return null;
    }
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  hasRole(role: string): boolean {
    const user = this.getCurrentUser();
    return user?.roles?.includes(role) ?? false;
  }

  hasPermission(permission: string): boolean {
    const user = this.getCurrentUser();
    return user?.permissions?.includes(permission) ?? false;
  }

  updateStoredUser(user: User): void {
    localStorage.setItem(this.USER_KEY, JSON.stringify(user));
  }

  loginWithGoogle(): Observable<AuthResponse> {
    const auth = getFirebaseAuth();
    const provider = new GoogleAuthProvider();
    return new Observable<AuthResponse>((subscriber) => {
      signInWithPopup(auth, provider)
        .then((result: UserCredential) => this.handleFirebaseResult(result))
        .then((response) => {
          subscriber.next(response);
          subscriber.complete();
        })
        .catch((error) => subscriber.error(error));
    });
  }

  loginWithMicrosoft(): Observable<AuthResponse> {
    const auth = getFirebaseAuth();
    const provider = new OAuthProvider('microsoft.com');
    return new Observable<AuthResponse>((subscriber) => {
      signInWithPopup(auth, provider)
        .then((result: UserCredential) => this.handleFirebaseResult(result))
        .then((response) => {
          subscriber.next(response);
          subscriber.complete();
        })
        .catch((error) => subscriber.error(error));
    });
  }

  loginWithGitHub(): Observable<AuthResponse> {
    const auth = getFirebaseAuth();
    const provider = new GithubAuthProvider();
    return new Observable<AuthResponse>((subscriber) => {
      signInWithPopup(auth, provider)
        .then((result: UserCredential) => this.handleFirebaseResult(result))
        .then((response) => {
          subscriber.next(response);
          subscriber.complete();
        })
        .catch((error) => subscriber.error(error));
    });
  }

  loginWithLinkedIn(): Observable<AuthResponse> {
    const auth = getFirebaseAuth();
    const provider = new OAuthProvider('linkedin.com');
    provider.addScope('r_liteprofile');
    provider.addScope('r_emailaddress');
    return new Observable<AuthResponse>((subscriber) => {
      signInWithPopup(auth, provider)
        .then((result: UserCredential) => this.handleFirebaseResult(result))
        .then((response) => {
          subscriber.next(response);
          subscriber.complete();
        })
        .catch((error) => subscriber.error(error));
    });
  }

  changePassword(request: ChangePasswordRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/api/identity/auth/change-password`, request);
  }

  requestPasswordReset(email: string): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/api/identity/auth/forgot-password`, { email });
  }

  resetPassword(request: PasswordResetRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/api/identity/auth/reset-password`, request);
  }

  private handleFirebaseResult(result: UserCredential): Promise<AuthResponse> {
    return new Promise((resolve, reject) => {
      result.user.getIdToken().then((idToken) => {
        this.http
          .post<AuthResponse>(`${environment.apiUrl}/api/identity/auth/firebase`, { idToken })
          .subscribe({
            next: (response) => {
              this.storeAuthData(response);
              resolve(response);
            },
            error: (error) => reject(error),
          });
      });
    });
  }

  private storeAuthData(response: AuthResponse): void {
    localStorage.setItem(this.TOKEN_KEY, response.token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, response.refreshToken);
    localStorage.setItem(this.USER_KEY, JSON.stringify(response.user));
  }

  private getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }
}
