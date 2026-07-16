export interface CompanySettings {
  id: string;
  name: string;
  logo: string;
  address: string;
  city: string;
  state: string;
  country: string;
  postalCode: string;
  phone: string;
  email: string;
  website: string;
  currency: string;
  timezone: string;
  dateFormat: string;
  language: string;
  taxId: string;
  registrationNumber: string;
}

export interface Department {
  id: string;
  name: string;
  code: string;
  description: string;
  headId: string | null;
  headName: string | null;
  parentId: string | null;
  parentName: string | null;
  employeeCount: number;
  budget: number;
  isActive: boolean;
  children: Department[];
  createdAt: string;
  updatedAt: string;
}

export interface Designation {
  id: string;
  name: string;
  level: number;
  departmentId: string;
  departmentName: string;
  minSalary: number;
  maxSalary: number;
  employeeCount: number;
  isActive: boolean;
  createdAt: string;
}

export interface Branch {
  id: string;
  name: string;
  code: string;
  address: string;
  city: string;
  state: string;
  country: string;
  phone: string;
  email: string;
  isHeadquarters: boolean;
  employeeCount: number;
  latitude: number;
  longitude: number;
  isActive: boolean;
  createdAt: string;
}

export interface Role {
  id: string;
  name: string;
  description: string;
  color: string;
  permissionCount: number;
  userCount: number;
  isSystem: boolean;
  isActive: boolean;
  permissions: string[];
  createdAt: string;
  updatedAt: string;
}

export interface Permission {
  id: string;
  name: string;
  code: string;
  module: string;
  feature: string;
  action: string;
  description: string;
  children: Permission[];
}

export interface AuditLog {
  id: string;
  userId: string;
  userName: string;
  action: string;
  entityType: string;
  entityId: string;
  entityName: string;
  oldValues: Record<string, any> | null;
  newValues: Record<string, any> | null;
  ipAddress: string;
  userAgent: string;
  timestamp: string;
}

export interface FeatureFlag {
  id: string;
  name: string;
  key: string;
  description: string;
  isEnabled: boolean;
  environment: string;
  percentage: number;
  allowedRoles: string[];
  createdAt: string;
  updatedAt: string;
}

export interface WorkflowTemplate {
  id: string;
  name: string;
  description: string;
  entityType: string;
  steps: WorkflowStep[];
  isActive: boolean;
  createdAt: string;
}

export interface WorkflowStep {
  id: string;
  name: string;
  order: number;
  approverRole: string;
  action: string;
  isRequired: boolean;
}

export interface SystemHealth {
  status: 'healthy' | 'degraded' | 'down';
  uptime: number;
  cpuUsage: number;
  memoryUsage: number;
  diskUsage: number;
  activeUsers: number;
  lastBackup: string;
  version: string;
}

export interface AdminDashboardStats {
  totalUsers: number;
  activeUsers: number;
  totalDepartments: number;
  totalRoles: number;
  recentAuditLogs: AuditLog[];
  systemHealth: SystemHealth;
}
