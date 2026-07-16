export interface UserProfile {
  id: string;
  employeeId: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  avatar: string;
  designation: string;
  department: string;
  branch: string;
  dateOfJoining: string;
  dateOfBirth: string;
  gender: string;
  bloodGroup: string;
  address: string;
  city: string;
  state: string;
  country: string;
  postalCode: string;
  reportingManager: string;
  employmentType: string;
  status: string;
}

export interface EmergencyContact {
  id: string;
  name: string;
  relationship: string;
  phone: string;
  email: string;
  address: string;
}

export interface EmployeeSkill {
  id: string;
  name: string;
  level: 'beginner' | 'intermediate' | 'advanced' | 'expert';
  yearsOfExperience: number;
  endorsed: boolean;
  endorsements: number;
}

export interface EmployeeDocument {
  id: string;
  name: string;
  type: string;
  url: string;
  uploadedAt: string;
}

export interface LeaveSummary {
  totalLeaves: number;
  usedLeaves: number;
  pendingLeaves: number;
  balance: number;
  leaveType: string;
}

export interface AttendanceSummary {
  present: number;
  absent: number;
  late: number;
  wfh: number;
  totalWorkingDays: number;
  attendancePercentage: number;
}

export interface NotificationPreference {
  email: boolean;
  push: boolean;
  sms: boolean;
  type: string;
}

export interface SessionInfo {
  id: string;
  device: string;
  browser: string;
  ipAddress: string;
  lastActive: string;
  isCurrent: boolean;
}

export interface ProfileSettings {
  notifications: NotificationPreference[];
  theme: 'light' | 'dark' | 'system';
  language: string;
  timezone: string;
  dateFormat: string;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
  confirmPassword: string;
}

export interface UpdateProfileRequest {
  firstName: string;
  lastName: string;
  phone: string;
  address: string;
  city: string;
  state: string;
  country: string;
  postalCode: string;
  dateOfBirth: string;
  gender: string;
  bloodGroup: string;
}
