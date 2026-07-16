-- ============================================
-- HRMS Pro - PostgreSQL Seed Script
-- ============================================
-- For Supabase free tier (runs once on first deploy)
-- Columns match EF Core migration schema exactly
-- ============================================

-- Shared tenant ID for single-tenant seed data
DO $$
DECLARE
  v_tenant_id uuid := 'a0000000-0000-0000-0000-000000000001';
  v_company_id uuid := 'c1b2c3d4-e5f6-7890-abcd-ef1234567801';
BEGIN

-- Roles
-- Schema: Id, Name, Description, TenantId, IsSystemRole
INSERT INTO "Roles" ("Id", "Name", "Description", "TenantId", "IsSystemRole")
VALUES
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567801', 'SuperAdmin', 'Full system access', v_tenant_id, true),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567802', 'Admin', 'Administrative access', v_tenant_id, true),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567803', 'HRManager', 'HR management access', v_tenant_id, false),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567804', 'Manager', 'Team management access', v_tenant_id, false),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567805', 'Employee', 'Standard employee access', v_tenant_id, false),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567806', 'ReadOnly', 'Read-only access', v_tenant_id, false)
ON CONFLICT ("Id") DO NOTHING;

-- Admin User (password: Admin@123)
-- Schema: Id, Email, FirstName, LastName, PhoneNumber, ProfilePictureUrl, FirebaseUid,
--   IsActive, IsEmailVerified, IsPhoneVerified, IsMfaEnabled, MfaSecret,
--   LastLoginAt, LastLoginIp, CreatedAt, UpdatedAt, TenantId
INSERT INTO "ApplicationUsers" ("Id", "Email", "FirstName", "LastName", "IsActive", "IsEmailVerified", "IsPhoneVerified", "IsMfaEnabled", "CreatedAt", "UpdatedAt", "TenantId")
VALUES
  ('b1b2c3d4-e5f6-7890-abcd-ef1234567801', 'admin@hrms-pro.com', 'System', 'Admin', true, true, false, false, NOW(), NOW(), v_tenant_id)
ON CONFLICT ("Id") DO NOTHING;

-- Admin Role Assignment
-- Schema: UserId, RoleId, AssignedAt, AssignedBy
INSERT INTO "UserRoles" ("UserId", "RoleId", "AssignedAt")
SELECT 'b1b2c3d4-e5f6-7890-abcd-ef1234567801', "Id", NOW()
FROM "Roles" WHERE "Name" = 'SuperAdmin'
AND NOT EXISTS (SELECT 1 FROM "UserRoles" WHERE "UserId" = 'b1b2c3d4-e5f6-7890-abcd-ef1234567801');

-- Default Company
-- Schema: Id, Name, LegalName, RegistrationNumber, TaxId, LogoUrl, Website, Email, Phone,
--   FoundedDate, Industry, EmployeeCountRange, IsActive, TenantId, CreatedAt, UpdatedAt,
--   CreatedBy, UpdatedBy, IsDeleted, Status
INSERT INTO "Companies" ("Id", "Name", "LegalName", "RegistrationNumber", "TaxId", "Industry", "Website", "IsActive", "TenantId", "CreatedAt", "IsDeleted", "Status")
VALUES
  ('c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'HRMS Pro Demo Company', 'HRMS Pro Demo Company', 'HRMS-DEMO', 'TAX-000', 'Technology', 'https://hrmspro.com', true, v_company_id, NOW(), false, 1)
ON CONFLICT ("Id") DO NOTHING;

-- Departments
-- Schema: Id, CompanyId, BranchId, ParentDepartmentId, Name, Code, Description, ManagerId,
--   HODId, Type, IsActive, TenantId, DepartmentId, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy,
--   IsDeleted, Status
INSERT INTO "Departments" ("Id", "CompanyId", "Name", "Code", "Type", "IsActive", "TenantId", "CreatedAt", "IsDeleted", "Status")
VALUES
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567801', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Human Resources', 'HR', 0, true, v_company_id, NOW(), false, 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567802', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Engineering', 'ENG', 0, true, v_company_id, NOW(), false, 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567803', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Marketing', 'MKT', 0, true, v_company_id, NOW(), false, 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567804', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Finance', 'FIN', 0, true, v_company_id, NOW(), false, 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567805', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Operations', 'OPS', 0, true, v_company_id, NOW(), false, 1)
ON CONFLICT ("Id") DO NOTHING;

-- Designations
-- Schema: Id, CompanyId, Name, Code, Description, Level (integer), MinSalary, MaxSalary,
--   IsActive, TenantId, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy, IsDeleted, Status
-- Level mapping: Junior=1, Mid=3, Senior=5, Manager=6, VP=8, Executive=10
INSERT INTO "Designations" ("Id", "CompanyId", "Name", "Code", "Level", "IsActive", "TenantId", "CreatedAt", "IsDeleted", "Status")
VALUES
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567801', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'CEO', 'CEO', 10, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567802', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'CTO', 'CTO', 10, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567803', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'VP Engineering', 'VPE', 8, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567804', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Senior Engineer', 'SR-ENG', 5, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567805', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Engineer', 'ENG', 3, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567806', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Junior Engineer', 'JR-ENG', 1, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567807', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'HR Manager', 'HR-MGR', 6, true, v_company_id, NOW(), false, 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567808', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'HR Coordinator', 'HR-CRD', 3, true, v_company_id, NOW(), false, 1)
ON CONFLICT ("Id") DO NOTHING;

-- Grades
-- Schema: Id, CompanyId, Name, Code, MinSalary, MaxSalary, Benefits, IsActive, TenantId,
--   CreatedAt, UpdatedAt, CreatedBy, UpdatedBy, IsDeleted, Status
INSERT INTO "Grades" ("Id", "CompanyId", "Name", "Code", "MinSalary", "MaxSalary", "IsActive", "TenantId", "CreatedAt", "IsDeleted", "Status")
VALUES
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567801', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Executive', 'G1', 150000, 300000, true, v_company_id, NOW(), false, 1),
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567802', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Senior', 'G2', 80000, 150000, true, v_company_id, NOW(), false, 1),
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567803', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Mid-Level', 'G3', 50000, 80000, true, v_company_id, NOW(), false, 1),
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567804', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Junior', 'G4', 30000, 50000, true, v_company_id, NOW(), false, 1)
ON CONFLICT ("Id") DO NOTHING;

-- Leave Types
-- Schema: Id, Name, Code, Description, Color, Icon, IsPaid, IsUnlimited,
--   DefaultBalanceDays, MaxBalanceDays, MaxCarryForwardDays, MaxEncashmentDays,
--   CarryForwardExpiryMonths, AllowEncashment, AllowCarryForward, AllowHalfDay,
--   MinDaysPerRequest, MaxDaysPerRequest, MaxConsecutiveDays, RequireDocumentation,
--   DocumentationDaysThreshold, Gender, ApplicableAfterDays, AccrualType, AccrualRate,
--   IsActive, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy, IsDeleted, TenantId, Status
INSERT INTO "LeaveTypes" ("Id", "Name", "Code", "Description", "IsPaid", "IsUnlimited", "DefaultBalanceDays", "MaxBalanceDays", "MaxCarryForwardDays", "MaxEncashmentDays", "AllowEncashment", "AllowCarryForward", "AllowHalfDay", "MinDaysPerRequest", "MaxDaysPerRequest", "RequireDocumentation", "Gender", "AccrualType", "AccrualRate", "IsActive", "CreatedAt", "IsDeleted", "TenantId", "Status")
VALUES
  ('11b2c3d4-e5f6-7890-abcd-ef1234567801', 'Annual Leave', 'AL', 'Yearly vacation leave', true, false, 20, 20, 5, 0, false, true, true, 1, 20, false, 0, 0, 0, true, NOW(), false, v_company_id, 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567802', 'Sick Leave', 'SL', 'Medical leave', true, false, 12, 12, 0, 0, false, false, true, 1, 12, false, 0, 0, 0, true, NOW(), false, v_company_id, 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567803', 'Personal Leave', 'PL', 'Personal time off', true, false, 5, 5, 0, 0, false, false, true, 1, 5, false, 0, 0, 0, true, NOW(), false, v_company_id, 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567804', 'Maternity Leave', 'ML', 'Maternity leave', true, false, 90, 90, 0, 0, false, false, false, 1, 90, false, 1, 0, 0, true, NOW(), false, v_company_id, 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567805', 'Paternity Leave', 'PTL', 'Paternity leave', true, false, 15, 15, 0, 0, false, false, true, 1, 15, false, 2, 0, 0, true, NOW(), false, v_company_id, 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567806', 'Bereavement Leave', 'BL', 'Bereavement leave', true, false, 5, 5, 0, 0, false, false, true, 1, 5, false, 0, 0, 0, true, NOW(), false, v_company_id, 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567807', 'Work From Home', 'WFH', 'Remote work days', true, false, 10, 10, 0, 0, false, false, true, 1, 10, false, 0, 0, 0, true, NOW(), false, v_company_id, 1)
ON CONFLICT ("Id") DO NOTHING;

-- Holidays
-- Schema: Id, CompanyId, BranchId, Name, Date, Type, IsRecurring, ApplicableDepartmentIdsJson,
--   IsActive, TenantId, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy, IsDeleted, Status
INSERT INTO "Holidays" ("Id", "CompanyId", "Name", "Date", "Type", "IsRecurring", "IsActive", "TenantId", "CreatedAt", "IsDeleted", "Status")
VALUES
  ('21b2c3d4-e5f6-7890-abcd-ef1234567801', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'New Year', '2025-01-01 00:00:00+00', 0, true, true, v_company_id, NOW(), false, 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567802', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Independence Day', '2025-07-04 00:00:00+00', 0, true, true, v_company_id, NOW(), false, 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567803', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Labor Day', '2025-09-01 00:00:00+00', 0, true, true, v_company_id, NOW(), false, 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567804', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Thanksgiving', '2025-11-27 00:00:00+00', 0, true, true, v_company_id, NOW(), false, 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567805', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Christmas', '2025-12-25 00:00:00+00', 0, true, true, v_company_id, NOW(), false, 1)
ON CONFLICT ("Id") DO NOTHING;

-- Notification Templates
-- Schema: Id, Name, Category (integer), Channel (integer), Subject, Body, Variables, IsActive,
--   Language, TenantId, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy, IsDeleted, Status
-- Category: 0=General, 1=Leave, 2=Payroll, 3=Recruitment, 4=Onboarding, 5=Performance
-- Channel: 0=Email, 1=SMS, 2=Push, 3=InApp
INSERT INTO "NotificationTemplates" ("Id", "Name", "Category", "Channel", "Subject", "Body", "IsActive", "Language", "TenantId", "CreatedAt", "IsDeleted", "Status")
VALUES
  ('31b2c3d4-e5f6-7890-abcd-ef1234567801', 'Leave Request', 1, 0, 'Leave Request Submitted', 'Your leave request has been submitted for approval.', true, 'en', v_tenant_id, NOW(), false, 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567802', 'Leave Approved', 1, 0, 'Leave Request Approved', 'Your leave request has been approved.', true, 'en', v_tenant_id, NOW(), false, 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567803', 'Leave Rejected', 1, 0, 'Leave Request Rejected', 'Your leave request has been rejected.', true, 'en', v_tenant_id, NOW(), false, 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567804', 'Welcome', 4, 0, 'Welcome to HRMS Pro', 'Welcome to HRMS Pro! Your account has been created.', true, 'en', v_tenant_id, NOW(), false, 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567805', 'Password Reset', 0, 0, 'Password Reset Request', 'Click the link to reset your password.', true, 'en', v_tenant_id, NOW(), false, 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567806', 'Payroll Generated', 2, 0, 'Payslip Available', 'Your payslip for this month is now available.', true, 'en', v_tenant_id, NOW(), false, 1)
ON CONFLICT ("Id") DO NOTHING;

END $$;
