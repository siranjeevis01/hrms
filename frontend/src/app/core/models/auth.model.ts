export interface User {
  id: string;
  email: string;
  displayName: string;
  photoUrl: string | null;
  phoneNumber: string | null;
  roles: string[];
  permissions: string[];
  tenantId: string;
  departmentId: string | null;
  employeeId: string | null;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
}

export interface AuthResponse {
  token: string;
  refreshToken: string;
  expiresIn: number;
  user: User;
}

export interface RefreshTokenRequest {
  refreshToken: string;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
  confirmPassword: string;
}

export interface PasswordResetRequest {
  email: string;
  token: string;
  newPassword: string;
}
