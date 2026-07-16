CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AnalyticsEvents" (
        "Id" uuid NOT NULL,
        "EventType" integer NOT NULL,
        "EntityId" text,
        "EntityType" text,
        "EmployeeId" uuid,
        "Data" text,
        "Timestamp" timestamp with time zone NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_AnalyticsEvents" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ApplicationUsers" (
        "Id" uuid NOT NULL,
        "Email" text NOT NULL,
        "FirstName" text NOT NULL,
        "LastName" text NOT NULL,
        "PhoneNumber" text,
        "ProfilePictureUrl" text,
        "FirebaseUid" text,
        "IsActive" boolean NOT NULL,
        "IsEmailVerified" boolean NOT NULL,
        "IsPhoneVerified" boolean NOT NULL,
        "IsMfaEnabled" boolean NOT NULL,
        "MfaSecret" text,
        "LastLoginAt" timestamp with time zone,
        "LastLoginIp" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone NOT NULL,
        "TenantId" uuid,
        CONSTRAINT "PK_ApplicationUsers" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Appraisals" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ManagerId" uuid,
        "Period" text NOT NULL,
        "Type" integer NOT NULL,
        "Status" integer NOT NULL,
        "FinalRating" numeric,
        "HikePercentage" numeric,
        "PromotionRecommended" boolean NOT NULL,
        "Bonus" numeric,
        "Comments" text,
        "ApprovedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Appraisals" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ApprovalMatrices" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "EntityType" integer NOT NULL,
        "Conditions" text,
        "Approvers" text,
        "IsActive" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ApprovalMatrices" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AssessmentAttempts" (
        "Id" uuid NOT NULL,
        "AssessmentId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Answers" text,
        "Score" integer NOT NULL,
        "TotalPoints" integer NOT NULL,
        "Passed" boolean NOT NULL,
        "StartedAt" timestamp with time zone NOT NULL,
        "CompletedAt" timestamp with time zone,
        "AttemptNumber" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_AssessmentAttempts" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Assessments" (
        "Id" uuid NOT NULL,
        "CourseId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "PassingScore" integer NOT NULL,
        "TotalPoints" integer NOT NULL,
        "TimeLimitMinutes" integer,
        "MaxAttempts" integer NOT NULL,
        "Status" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Assessments" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AttendancePolicies" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "GracePeriodMinutes" integer NOT NULL,
        "MaxLateAllowed" integer NOT NULL,
        "LateDeductionMinutes" integer NOT NULL,
        "AutoCheckoutTime" interval NOT NULL,
        "HalfDayMinimumHours" numeric NOT NULL,
        "FullDayMinimumHours" numeric NOT NULL,
        "OvertimeEnabled" boolean NOT NULL,
        "OvertimeThresholdMinutes" integer NOT NULL,
        "MaxOvertimeMinutes" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_AttendancePolicies" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AttendanceRecords" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "CheckInTime" timestamp with time zone,
        "CheckOutTime" timestamp with time zone,
        "ShiftId" uuid,
        "Status" integer NOT NULL,
        "CheckInMethod" integer,
        "CheckOutMethod" integer,
        "CheckInLatitude" double precision,
        "CheckInLongitude" double precision,
        "CheckOutLatitude" double precision,
        "CheckOutLongitude" double precision,
        "WifiSSID" text,
        "WifiBSSID" text,
        "QrCodeId" text,
        "TotalHours" numeric,
        "OvertimeHours" numeric,
        "BreakMinutes" integer NOT NULL,
        "IsLate" boolean NOT NULL,
        "LateMinutes" integer NOT NULL,
        "IsEarlyExit" boolean NOT NULL,
        "EarlyExitMinutes" integer NOT NULL,
        "Notes" text,
        "ApprovedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "IsApproved" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_AttendanceRecords" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AttendanceRegularizations" (
        "Id" uuid NOT NULL,
        "AttendanceRecordId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Reason" text NOT NULL,
        "RequestedDate" timestamp with time zone NOT NULL,
        "OriginalCheckIn" timestamp with time zone,
        "OriginalCheckOut" timestamp with time zone,
        "RequestedCheckIn" timestamp with time zone,
        "RequestedCheckOut" timestamp with time zone,
        "Status" integer NOT NULL,
        "ApprovedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "RejectionReason" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_AttendanceRegularizations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AttendanceSummaries" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Year" integer NOT NULL,
        "Month" integer NOT NULL,
        "TotalWorkingDays" integer NOT NULL,
        "TotalPresent" integer NOT NULL,
        "TotalAbsent" integer NOT NULL,
        "TotalLate" integer NOT NULL,
        "TotalHalfDays" integer NOT NULL,
        "TotalHolidays" integer NOT NULL,
        "TotalWeekOffs" integer NOT NULL,
        "TotalWFH" integer NOT NULL,
        "TotalLeaveDays" integer NOT NULL,
        "TotalOvertimeMinutes" integer NOT NULL,
        "TotalWorkedMinutes" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_AttendanceSummaries" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AuthAuditLogs" (
        "Id" uuid NOT NULL,
        "UserId" uuid,
        "Action" text NOT NULL,
        "IpAddress" text,
        "UserAgent" text,
        "Details" text,
        "Timestamp" timestamp with time zone NOT NULL,
        "IsSuccess" boolean NOT NULL,
        "FailureReason" text,
        CONSTRAINT "PK_AuthAuditLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Bonuses" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "BonusType" integer NOT NULL,
        "Amount" numeric NOT NULL,
        "Month" integer NOT NULL,
        "Year" integer NOT NULL,
        "Status" integer NOT NULL,
        "ApprovedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Bonuses" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Branches" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Phone" text,
        "Email" text,
        "ManagerId" uuid,
        "IsHeadquarters" boolean NOT NULL,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Branches" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Bugs" (
        "Id" uuid NOT NULL,
        "StoryId" uuid,
        "ProjectId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "StepsToReproduce" text,
        "ExpectedBehavior" text,
        "ActualBehavior" text,
        "Severity" integer NOT NULL,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "AssignedTo" uuid,
        "FoundBy" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Bugs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "CalibrationSessions" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "ReviewPeriod" text NOT NULL,
        "ConductedBy" uuid NOT NULL,
        "Status" integer NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_CalibrationSessions" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Candidates" (
        "Id" uuid NOT NULL,
        "FirstName" text NOT NULL,
        "LastName" text NOT NULL,
        "Email" text NOT NULL,
        "PhoneNumber" text NOT NULL,
        "CurrentCompany" text,
        "CurrentDesignation" text,
        "TotalExperience" numeric,
        "ExpectedSalary" numeric,
        "Currency" text NOT NULL,
        "ResumeUrl" text,
        "CoverLetter" text,
        "Source" integer NOT NULL,
        "ReferralEmployeeId" uuid,
        "Skills" text NOT NULL,
        "Education" text NOT NULL,
        "Status" integer NOT NULL,
        "RejectionReason" text,
        "Notes" text,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Candidates" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Certificates" (
        "Id" uuid NOT NULL,
        "CourseId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "CertificateNumber" text NOT NULL,
        "IssuedAt" timestamp with time zone NOT NULL,
        "ExpiryDate" timestamp with time zone,
        "PdfUrl" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Certificates" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ChatChannels" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Type" integer NOT NULL,
        "CreatorId" uuid NOT NULL,
        "IsArchived" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ChatChannels" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ChatNotifications" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ConversationId" uuid NOT NULL,
        "MessageId" uuid NOT NULL,
        "Type" integer NOT NULL,
        "IsRead" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ChatNotifications" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Comments" (
        "Id" uuid NOT NULL,
        "TaskItemId" uuid,
        "StoryId" uuid,
        "BugId" uuid,
        "EmployeeId" uuid NOT NULL,
        "Content" text NOT NULL,
        "ParentCommentId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Comments" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Companies" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "LegalName" text NOT NULL,
        "RegistrationNumber" text NOT NULL,
        "TaxId" text NOT NULL,
        "LogoUrl" text,
        "Website" text,
        "Email" text,
        "Phone" text,
        "FoundedDate" timestamp with time zone,
        "Industry" text,
        "EmployeeCountRange" text,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Companies" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "CompanyPolicies" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Category" integer NOT NULL,
        "Content" text NOT NULL,
        "EffectiveDate" timestamp with time zone NOT NULL,
        "ExpiryDate" timestamp with time zone,
        "IsActive" boolean NOT NULL,
        "Version" integer NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_CompanyPolicies" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "CompOffs" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "LeaveApplicationId" uuid,
        "EarnedDate" timestamp with time zone NOT NULL,
        "ExpiryDate" timestamp with time zone NOT NULL,
        "Days" numeric NOT NULL,
        "Reason" text,
        "Status" integer NOT NULL,
        "UsedDate" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_CompOffs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Conversations" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Type" integer NOT NULL,
        "CreatorId" uuid NOT NULL,
        "Description" text,
        "IsPrivate" boolean NOT NULL,
        "LastMessageAt" timestamp with time zone,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Conversations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Courses" (
        "Id" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "Code" text NOT NULL,
        "Category" text,
        "DifficultyLevel" integer NOT NULL,
        "Duration" integer NOT NULL,
        "MaxEnrollments" integer NOT NULL,
        "ThumbnailUrl" text,
        "InstructorId" uuid,
        "DepartmentId" uuid,
        "Status" integer NOT NULL,
        "IsPublished" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Courses" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Dashboards" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "UserId" uuid NOT NULL,
        "IsDefault" boolean NOT NULL,
        "IsPublic" boolean NOT NULL,
        "Layout" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Dashboards" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DataChangeLogs" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "EntityType" integer NOT NULL,
        "EntityId" text NOT NULL,
        "ChangeType" text NOT NULL,
        "SerializedData" text,
        "Timestamp" timestamp with time zone NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DataChangeLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Delegations" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "DelegateToUserId" uuid NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "IsActive" boolean NOT NULL,
        "EntityType" integer,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Delegations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Departments" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "BranchId" uuid,
        "ParentDepartmentId" uuid,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Description" text,
        "ManagerId" uuid,
        "HODId" uuid,
        "Type" integer NOT NULL,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "DepartmentId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Departments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Departments_Departments_DepartmentId" FOREIGN KEY ("DepartmentId") REFERENCES "Departments" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Designations" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Description" text,
        "Level" integer NOT NULL,
        "MinSalary" numeric,
        "MaxSalary" numeric,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Designations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DocumentFolders" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "ParentFolderId" uuid,
        "Path" text NOT NULL,
        "Description" text,
        "CreatedBy" uuid NOT NULL,
        "IsSystem" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "DocumentFolderId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DocumentFolders" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DocumentFolders_DocumentFolders_DocumentFolderId" FOREIGN KEY ("DocumentFolderId") REFERENCES "DocumentFolders" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DocumentTemplates" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Category" integer NOT NULL,
        "FileUrl" text NOT NULL,
        "Placeholders" text,
        "CreatedBy" uuid NOT NULL,
        "IsPublic" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DocumentTemplates" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmailQueues" (
        "Id" uuid NOT NULL,
        "To" text NOT NULL,
        "Cc" text,
        "Bcc" text,
        "Subject" text NOT NULL,
        "Body" text NOT NULL,
        "IsHtml" boolean NOT NULL,
        "Attachments" text,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "ScheduledAt" timestamp with time zone,
        "SentAt" timestamp with time zone,
        "RetryCount" integer NOT NULL,
        "MaxRetries" integer NOT NULL,
        "LastError" text,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_EmailQueues" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Employees" (
        "Id" uuid NOT NULL,
        "EmployeeCode" text NOT NULL,
        "UserId" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "BranchId" uuid,
        "DepartmentId" uuid NOT NULL,
        "DesignationId" uuid NOT NULL,
        "GradeId" uuid,
        "ReportsToId" uuid,
        "FirstName" text NOT NULL,
        "LastName" text NOT NULL,
        "MiddleName" text,
        "PreferredName" text,
        "Email" text NOT NULL,
        "PersonalEmail" text,
        "PhoneNumber" text,
        "DateOfBirth" timestamp with time zone,
        "Gender" integer,
        "MaritalStatus" integer,
        "Nationality" text,
        "BloodGroup" text,
        "ProfilePictureUrl" text,
        "JoiningDate" timestamp with time zone NOT NULL,
        "ConfirmationDate" timestamp with time zone,
        "LastWorkingDate" timestamp with time zone,
        "EmploymentType" integer NOT NULL,
        "EmploymentStatus" integer NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Employees" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmployeeTaxDeclarations" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "FinancialYear" text NOT NULL,
        "DeclaredAmount" numeric NOT NULL,
        "InvestedAmount" numeric NOT NULL,
        "ProofSubmitted" boolean NOT NULL,
        "VerifiedBy" uuid,
        "VerifiedAt" timestamp with time zone,
        "Status" integer NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_EmployeeTaxDeclarations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ExpenseCategories" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Description" text,
        "DefaultPolicyId" uuid,
        "IsActive" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ExpenseCategories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ExpenseClaims" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "TotalAmount" numeric NOT NULL,
        "Currency" text NOT NULL,
        "Status" integer NOT NULL,
        "SubmittedAt" timestamp with time zone NOT NULL,
        "ReviewedBy" uuid,
        "ReviewedAt" timestamp with time zone,
        "RejectionReason" text,
        "PolicyViolationNotes" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_ExpenseClaims" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ExpensePolicies" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Category" integer NOT NULL,
        "MaxAmount" numeric NOT NULL,
        "Currency" text NOT NULL,
        "RequiresReceipt" boolean NOT NULL,
        "ApprovalRequired" boolean NOT NULL,
        "IsActive" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ExpensePolicies" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Faqs" (
        "Id" uuid NOT NULL,
        "Question" text NOT NULL,
        "Answer" text NOT NULL,
        "CategoryId" uuid,
        "Order" integer NOT NULL,
        "IsActive" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Faqs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Feedback360s" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ReviewerId" uuid NOT NULL,
        "ReviewPeriod" text NOT NULL,
        "Relationship" integer NOT NULL,
        "Status" integer NOT NULL,
        "SubmittedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Feedback360s" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "GeoFences" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "BranchId" uuid,
        "Name" text NOT NULL,
        "Latitude" double precision NOT NULL,
        "Longitude" double precision NOT NULL,
        "RadiusMeters" double precision NOT NULL,
        "IsActive" boolean NOT NULL,
        "WorkingDays" text,
        "StartTime" interval NOT NULL,
        "EndTime" interval NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_GeoFences" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Goals" (
        "Id" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "Category" integer NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ManagerId" uuid,
        "DepartmentId" uuid,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "Weight" numeric NOT NULL,
        "TargetValue" numeric,
        "CurrentValue" numeric,
        "Unit" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Goals" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Grades" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "MinSalary" numeric NOT NULL,
        "MaxSalary" numeric NOT NULL,
        "Benefits" text,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Grades" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "HelpdeskTickets" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Subject" text NOT NULL,
        "Description" text NOT NULL,
        "Category" integer NOT NULL,
        "Priority" integer NOT NULL,
        "Status" integer NOT NULL,
        "AssignedTo" uuid,
        "DepartmentId" uuid,
        "ResolutionNotes" text,
        "ResolvedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_HelpdeskTickets" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "HolidayCalendarEntries" (
        "Id" uuid NOT NULL,
        "HolidayId" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "BranchId" uuid,
        "Date" timestamp with time zone NOT NULL,
        "Name" text NOT NULL,
        "IsOptional" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_HolidayCalendarEntries" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Holidays" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "BranchId" uuid,
        "Name" text NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "Type" integer NOT NULL,
        "IsRecurring" boolean NOT NULL,
        "ApplicableDepartmentIdsJson" text,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Holidays" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "JobPostings" (
        "Id" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text NOT NULL,
        "DepartmentId" uuid NOT NULL,
        "DesignationId" uuid NOT NULL,
        "BranchId" uuid NOT NULL,
        "EmploymentType" integer NOT NULL,
        "MinExperience" integer NOT NULL,
        "MaxExperience" integer NOT NULL,
        "MinSalary" numeric NOT NULL,
        "MaxSalary" numeric NOT NULL,
        "Currency" text NOT NULL,
        "Skills" text NOT NULL,
        "Requirements" text NOT NULL,
        "Responsibilities" text NOT NULL,
        "Benefits" text NOT NULL,
        "Status" integer NOT NULL,
        "PublishedAt" timestamp with time zone,
        "ClosedAt" timestamp with time zone,
        "HeadCount" integer NOT NULL,
        "FilledCount" integer NOT NULL,
        "HiringManagerId" uuid NOT NULL,
        "RecruiterId" uuid NOT NULL,
        "IsUrgent" boolean NOT NULL,
        "ApplicationDeadline" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_JobPostings" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "KnowledgeArticles" (
        "Id" uuid NOT NULL,
        "Title" text NOT NULL,
        "Content" text NOT NULL,
        "CategoryId" uuid NOT NULL,
        "AuthorId" uuid NOT NULL,
        "Tags" text,
        "ViewCount" integer NOT NULL,
        "IsPublished" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_KnowledgeArticles" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "KPIs" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Category" integer NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "DepartmentId" uuid,
        "MetricType" integer NOT NULL,
        "TargetValue" numeric NOT NULL,
        "CurrentValue" numeric NOT NULL,
        "Unit" text,
        "Weight" numeric NOT NULL,
        "ScoringMethod" integer NOT NULL,
        "Period" text NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_KPIs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LearningPaths" (
        "Id" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "DepartmentId" uuid,
        "Status" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_LearningPaths" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeaveAccrualPolicies" (
        "Id" uuid NOT NULL,
        "LeaveTypeId" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "AccrualFrequency" integer NOT NULL,
        "AccrualDay" integer NOT NULL,
        "CustomAccrualDay" integer,
        "MaxAccrualPerYear" numeric NOT NULL,
        "ResetType" integer NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LeaveAccrualPolicies" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeaveApplications" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "LeaveTypeId" uuid NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "TotalDays" numeric NOT NULL,
        "IsHalfDay" boolean NOT NULL,
        "HalfDayType" integer,
        "Reason" text NOT NULL,
        "Status" integer NOT NULL,
        "AppliedAt" timestamp with time zone NOT NULL,
        "ApprovedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "RejectedBy" uuid,
        "RejectedAt" timestamp with time zone,
        "RejectionReason" text,
        "CurrentApprovalLevel" integer,
        "IsSandwichApplied" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_LeaveApplications" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeaveApprovalMatrices" (
        "Id" uuid NOT NULL,
        "LeaveTypeId" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Level" integer NOT NULL,
        "ApproverType" integer NOT NULL,
        "ApproverUserId" uuid,
        "IsRequired" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LeaveApprovalMatrices" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeaveBalances" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "LeaveTypeId" uuid NOT NULL,
        "Year" integer NOT NULL,
        "TotalDays" numeric NOT NULL,
        "UsedDays" numeric NOT NULL,
        "PendingDays" numeric NOT NULL,
        "CarryForwardDays" numeric NOT NULL,
        "EncashedDays" numeric NOT NULL,
        "AdjustedDays" numeric NOT NULL,
        "LastAccrualDate" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LeaveBalances" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeavePolicies" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "SandwichPolicyEnabled" boolean NOT NULL,
        "SandwichPolicyDays" integer,
        "MinNoticeDays" integer NOT NULL,
        "MaxPendingApplications" integer NOT NULL,
        "AllowBackDatedLeave" boolean NOT NULL,
        "BackDatedLimitDays" integer,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LeavePolicies" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeaveTypes" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Description" text,
        "Color" text,
        "Icon" text,
        "IsPaid" boolean NOT NULL,
        "IsUnlimited" boolean NOT NULL,
        "DefaultBalanceDays" integer NOT NULL,
        "MaxBalanceDays" integer NOT NULL,
        "MaxCarryForwardDays" integer NOT NULL,
        "MaxEncashmentDays" integer NOT NULL,
        "CarryForwardExpiryMonths" integer,
        "AllowEncashment" boolean NOT NULL,
        "AllowCarryForward" boolean NOT NULL,
        "AllowHalfDay" boolean NOT NULL,
        "MinDaysPerRequest" integer NOT NULL,
        "MaxDaysPerRequest" integer NOT NULL,
        "MaxConsecutiveDays" integer,
        "RequireDocumentation" boolean NOT NULL,
        "DocumentationDaysThreshold" integer,
        "Gender" integer NOT NULL,
        "ApplicableAfterDays" integer,
        "AccrualType" integer NOT NULL,
        "AccrualRate" numeric NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LeaveTypes" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Loans" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "LoanType" integer NOT NULL,
        "Amount" numeric NOT NULL,
        "OutstandingAmount" numeric NOT NULL,
        "MonthlyDeduction" numeric NOT NULL,
        "StartDate" date NOT NULL,
        "EndDate" date,
        "Status" integer NOT NULL,
        "ApprovedBy" uuid,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Loans" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LoginHistories" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "LoginAt" timestamp with time zone NOT NULL,
        "LogoutAt" timestamp with time zone,
        "IpAddress" text,
        "UserAgent" text,
        "Device" text,
        "Browser" text,
        "IsSuccessful" boolean NOT NULL,
        "FailureReason" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LoginHistories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "NotificationDeliveryLogs" (
        "Id" uuid NOT NULL,
        "NotificationId" uuid NOT NULL,
        "Channel" integer NOT NULL,
        "Provider" text NOT NULL,
        "ProviderMessageId" text,
        "Status" integer NOT NULL,
        "Response" text,
        "AttemptCount" integer NOT NULL,
        "LastAttemptAt" timestamp with time zone,
        "NextRetryAt" timestamp with time zone,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_NotificationDeliveryLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "NotificationGroups" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Members" text,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_NotificationGroups" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "NotificationPreferences" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Category" integer NOT NULL,
        "Channel" integer NOT NULL,
        "IsEnabled" boolean NOT NULL,
        "Frequency" integer NOT NULL,
        "QuietHoursStart" time without time zone,
        "QuietHoursEnd" time without time zone,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_NotificationPreferences" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Notifications" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Message" text NOT NULL,
        "Type" integer NOT NULL,
        "Category" integer NOT NULL,
        "Priority" integer NOT NULL,
        "Status" integer NOT NULL,
        "Channel" integer NOT NULL,
        "SentAt" timestamp with time zone,
        "DeliveredAt" timestamp with time zone,
        "ReadAt" timestamp with time zone,
        "FailedAt" timestamp with time zone,
        "FailureReason" text,
        "Data" text,
        "ActionUrl" text,
        "IsRead" boolean NOT NULL,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Notifications" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "NotificationTemplates" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Category" integer NOT NULL,
        "Channel" integer NOT NULL,
        "Subject" text,
        "Body" text NOT NULL,
        "Variables" text,
        "IsActive" boolean NOT NULL,
        "Language" text NOT NULL,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_NotificationTemplates" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "OKRs" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ManagerId" uuid,
        "Period" text NOT NULL,
        "Status" integer NOT NULL,
        "OverallScore" numeric,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_OKRs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "OnboardingChecklists" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "CandidateId" uuid NOT NULL,
        "JoiningDate" timestamp with time zone NOT NULL,
        "Items" text NOT NULL,
        "Status" integer NOT NULL,
        "CompletedAt" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_OnboardingChecklists" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "PayrollRuns" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Month" integer NOT NULL,
        "Year" integer NOT NULL,
        "Status" integer NOT NULL,
        "ProcessedAt" timestamp with time zone,
        "ProcessedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "ApprovedBy" uuid,
        "TotalEmployees" integer NOT NULL,
        "TotalGrossAmount" numeric NOT NULL,
        "TotalDeductions" numeric NOT NULL,
        "TotalNetAmount" numeric NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_PayrollRuns" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "PerformanceReviews" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ReviewerId" uuid NOT NULL,
        "ReviewPeriod" text NOT NULL,
        "ReviewType" integer NOT NULL,
        "Status" integer NOT NULL,
        "OverallRating" numeric,
        "OverallScore" numeric,
        "Strengths" text,
        "AreasForImprovement" text,
        "Comments" text,
        "SubmittedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_PerformanceReviews" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Projects" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Code" text NOT NULL,
        "DepartmentId" uuid NOT NULL,
        "ClientName" text,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "Budget" numeric NOT NULL,
        "ActualCost" numeric NOT NULL,
        "Currency" text,
        "ProjectManagerId" uuid,
        "OwnerId" uuid,
        "ProgressPercentage" numeric NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Projects" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "PushNotifications" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Body" text NOT NULL,
        "Data" text,
        "DeviceTokens" text,
        "Status" integer NOT NULL,
        "SentAt" timestamp with time zone,
        "Response" text,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_PushNotifications" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "RefreshTokens" (
        "Id" uuid NOT NULL,
        "Token" text NOT NULL,
        "UserId" uuid NOT NULL,
        "Expires" timestamp with time zone NOT NULL,
        "Created" timestamp with time zone NOT NULL,
        "CreatedByIp" text NOT NULL,
        "Revoked" timestamp with time zone,
        "RevokedByIp" text,
        "ReplacedByToken" text,
        CONSTRAINT "PK_RefreshTokens" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ReportTemplates" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Category" integer NOT NULL,
        "ReportType" integer NOT NULL,
        "DataSource" text NOT NULL,
        "QueryDefinition" text,
        "Parameters" text,
        "Format" integer NOT NULL,
        "AccessLevel" integer NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ReportTemplates" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Roles" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "TenantId" uuid,
        "IsSystemRole" boolean NOT NULL,
        CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "SalaryComponentDefs" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Type" integer NOT NULL,
        "CalculationType" integer NOT NULL,
        "DefaultValue" numeric NOT NULL,
        "Formula" text,
        "IsTaxable" boolean NOT NULL,
        "IsPensionable" boolean NOT NULL,
        "IsPFApplicable" boolean NOT NULL,
        "IsESIApplicable" boolean NOT NULL,
        "IsActive" boolean NOT NULL,
        "SortOrder" integer NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_SalaryComponentDefs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "SalaryComponents" (
        "Name" text NOT NULL,
        "Type" text NOT NULL,
        "Amount" numeric NOT NULL,
        "Percentage" numeric,
        "IsEarning" boolean NOT NULL,
        "IsActive" boolean NOT NULL
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ScheduledReports" (
        "Id" uuid NOT NULL,
        "TemplateId" uuid NOT NULL,
        "Name" text NOT NULL,
        "CronExpression" text NOT NULL,
        "Recipients" text,
        "Parameters" text,
        "Format" integer NOT NULL,
        "IsActive" boolean NOT NULL,
        "LastRunAt" timestamp with time zone,
        "NextRunAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ScheduledReports" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ShiftAssignments" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ShiftId" uuid NOT NULL,
        "EffectiveFrom" timestamp with time zone NOT NULL,
        "EffectiveTo" timestamp with time zone,
        "IsCurrent" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ShiftAssignments" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Shifts" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "StartTime" time without time zone NOT NULL,
        "EndTime" time without time zone NOT NULL,
        "BreakDuration" interval NOT NULL,
        "GraceMinutes" integer NOT NULL,
        "IsFlexible" boolean NOT NULL,
        "MaxShifts" integer NOT NULL,
        "IsActive" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Shifts" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "SmsQueues" (
        "Id" uuid NOT NULL,
        "PhoneNumber" text NOT NULL,
        "Message" text NOT NULL,
        "Status" integer NOT NULL,
        "Provider" text NOT NULL,
        "ProviderMessageId" text,
        "RetryCount" integer NOT NULL,
        "MaxRetries" integer NOT NULL,
        "SentAt" timestamp with time zone,
        "LastError" text,
        "TenantId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_SmsQueues" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "SystemAuditLogs" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "UserName" text NOT NULL,
        "ActionType" integer NOT NULL,
        "EntityType" integer NOT NULL,
        "EntityId" text NOT NULL,
        "OldValues" text,
        "NewValues" text,
        "IpAddress" text,
        "UserAgent" text,
        "TenantId" text NOT NULL,
        "Timestamp" timestamp with time zone NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_SystemAuditLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TaskItems" (
        "Id" uuid NOT NULL,
        "StoryId" uuid,
        "ProjectId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "AssignedTo" uuid,
        "EstimatedHours" numeric NOT NULL,
        "LoggedHours" numeric NOT NULL,
        "DueDate" timestamp with time zone,
        "ParentTaskId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_TaskItems" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TaxConfigurations" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "FinancialYear" text NOT NULL,
        "Country" text NOT NULL,
        "TaxSlabConfig" text NOT NULL,
        "PFContributionRate" numeric NOT NULL,
        "ESIContributionRate" numeric NOT NULL,
        "ProfessionalTaxConfig" text NOT NULL,
        "StandardDeduction" numeric NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TaxConfigurations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TaxSlabs" (
        "MinIncome" numeric NOT NULL,
        "MaxIncome" numeric NOT NULL,
        "TaxRate" numeric NOT NULL,
        "Surcharge" numeric NOT NULL
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TicketCategories" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Code" text NOT NULL,
        "Description" text,
        "DefaultAssigneeId" uuid,
        "SLAHours" integer NOT NULL,
        "IsActive" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TicketCategories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TimeLogs" (
        "Id" uuid NOT NULL,
        "TaskItemId" uuid,
        "StoryId" uuid,
        "EmployeeId" uuid NOT NULL,
        "Hours" numeric NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "Description" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TimeLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TrainingSchedules" (
        "Id" uuid NOT NULL,
        "CourseId" uuid NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "Location" text,
        "MaxParticipants" integer NOT NULL,
        "InstructorName" text,
        "MeetingUrl" text,
        "Status" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_TrainingSchedules" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TravelRequests" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Purpose" text NOT NULL,
        "Destination" text NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "EstimatedCost" numeric NOT NULL,
        "ActualCost" numeric,
        "Currency" text NOT NULL,
        "TransportMode" integer NOT NULL,
        "AccommodationType" integer NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_TravelRequests" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "UserPresences" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "PresenceStatus" integer NOT NULL,
        "LastSeenAt" timestamp with time zone,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_UserPresences" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "UserSessions" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "DeviceInfo" text NOT NULL,
        "IpAddress" text NOT NULL,
        "LastActiveAt" timestamp with time zone NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "ExpiresAt" timestamp with time zone NOT NULL,
        "IsActive" boolean NOT NULL,
        "RefreshTokenId" uuid,
        CONSTRAINT "PK_UserSessions" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "VisaRequests" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Country" text NOT NULL,
        "VisaType" text NOT NULL,
        "Purpose" text NOT NULL,
        "TravelRequestId" uuid,
        "Status" integer NOT NULL,
        "SubmissionDate" timestamp with time zone NOT NULL,
        "ExpiryDate" timestamp with time zone,
        "DocumentUrl" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_VisaRequests" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WidgetPresets" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "WidgetType" integer NOT NULL,
        "DefaultConfiguration" text,
        "Description" text,
        "Category" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_WidgetPresets" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WifiNetworks" (
        "Id" uuid NOT NULL,
        "CompanyId" uuid NOT NULL,
        "BranchId" uuid,
        "Name" text NOT NULL,
        "Ssid" text NOT NULL,
        "Bssid" text NOT NULL,
        "IsPrimary" boolean NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_WifiNetworks" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WorkflowDefinitions" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "EntityType" integer NOT NULL,
        "Steps" text,
        "IsActive" boolean NOT NULL,
        "Version" integer NOT NULL,
        "CreatedBy" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_WorkflowDefinitions" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WorkflowInstances" (
        "Id" uuid NOT NULL,
        "WorkflowDefinitionId" uuid NOT NULL,
        "EntityType" integer NOT NULL,
        "EntityId" uuid NOT NULL,
        "RequestedById" uuid NOT NULL,
        "CurrentStepNumber" integer NOT NULL,
        "Status" integer NOT NULL,
        "StartedAt" timestamp with time zone NOT NULL,
        "CompletedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_WorkflowInstances" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WorkFromHomes" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "Reason" text NOT NULL,
        "Status" integer NOT NULL,
        "ApprovedBy" uuid,
        "ApprovedAt" timestamp with time zone,
        "DayWiseStatus" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_WorkFromHomes" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "UserPermissions" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Permission" text NOT NULL,
        "IsGranted" boolean NOT NULL,
        "GrantedAt" timestamp with time zone NOT NULL,
        "GrantedBy" uuid,
        CONSTRAINT "PK_UserPermissions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserPermissions_ApplicationUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "ApplicationUsers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AssessmentQuestions" (
        "Id" uuid NOT NULL,
        "AssessmentId" uuid NOT NULL,
        "QuestionText" text NOT NULL,
        "QuestionType" integer NOT NULL,
        "Options" text,
        "CorrectAnswer" text,
        "Points" integer NOT NULL,
        "Order" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_AssessmentQuestions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_AssessmentQuestions_Assessments_AssessmentId" FOREIGN KEY ("AssessmentId") REFERENCES "Assessments" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "CalibrationEntries" (
        "Id" uuid NOT NULL,
        "CalibrationSessionId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "OriginalRating" numeric NOT NULL,
        "CalibratedRating" numeric NOT NULL,
        "Justification" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_CalibrationEntries" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_CalibrationEntries_CalibrationSessions_CalibrationSessionId" FOREIGN KEY ("CalibrationSessionId") REFERENCES "CalibrationSessions" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ConversationParticipants" (
        "Id" uuid NOT NULL,
        "ConversationId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Role" integer NOT NULL,
        "JoinedAt" timestamp with time zone NOT NULL,
        "LeftAt" timestamp with time zone,
        "LastReadMessageId" uuid,
        "IsMuted" boolean NOT NULL,
        "ChatChannelId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ConversationParticipants" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ConversationParticipants_ChatChannels_ChatChannelId" FOREIGN KEY ("ChatChannelId") REFERENCES "ChatChannels" ("Id"),
        CONSTRAINT "FK_ConversationParticipants_Conversations_ConversationId" FOREIGN KEY ("ConversationId") REFERENCES "Conversations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Messages" (
        "Id" uuid NOT NULL,
        "ConversationId" uuid NOT NULL,
        "SenderId" uuid NOT NULL,
        "Content" text NOT NULL,
        "Type" integer NOT NULL,
        "ParentMessageId" uuid,
        "IsEdited" boolean NOT NULL,
        "EditedAt" timestamp with time zone,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Messages" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Messages_Conversations_ConversationId" FOREIGN KEY ("ConversationId") REFERENCES "Conversations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "CourseModules" (
        "Id" uuid NOT NULL,
        "CourseId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "Order" integer NOT NULL,
        "Duration" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_CourseModules" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_CourseModules_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Enrollments" (
        "Id" uuid NOT NULL,
        "CourseId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "EnrolledAt" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "ProgressPercentage" double precision NOT NULL,
        "CompletedAt" timestamp with time zone,
        "CertificateId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Enrollments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Enrollments_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DashboardShares" (
        "Id" uuid NOT NULL,
        "DashboardId" uuid NOT NULL,
        "SharedWithUserId" uuid NOT NULL,
        "Permission" text NOT NULL,
        "SharedAt" timestamp with time zone NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DashboardShares" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DashboardShares_Dashboards_DashboardId" FOREIGN KEY ("DashboardId") REFERENCES "Dashboards" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DashboardWidgets" (
        "Id" uuid NOT NULL,
        "DashboardId" uuid NOT NULL,
        "WidgetType" integer NOT NULL,
        "Title" text NOT NULL,
        "DataSource" text,
        "Configuration" text,
        "Position" text,
        "Size" text,
        "RefreshIntervalSeconds" integer NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DashboardWidgets" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DashboardWidgets_Dashboards_DashboardId" FOREIGN KEY ("DashboardId") REFERENCES "Dashboards" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Documents" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "FileName" text NOT NULL,
        "ContentType" text NOT NULL,
        "FileSize" bigint NOT NULL,
        "FileUrl" text NOT NULL,
        "ThumbnailUrl" text,
        "FolderId" uuid,
        "UploadedBy" uuid NOT NULL,
        "Description" text,
        "Tags" text,
        "IsPublic" boolean NOT NULL,
        "Status" integer NOT NULL,
        "Category" integer NOT NULL,
        "TenantId" text NOT NULL,
        "DocumentFolderId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Documents" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Documents_DocumentFolders_DocumentFolderId" FOREIGN KEY ("DocumentFolderId") REFERENCES "DocumentFolders" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "BankDetails" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "BankName" text NOT NULL,
        "BankCode" text NOT NULL,
        "AccountNumber" text NOT NULL,
        "AccountHolderName" text NOT NULL,
        "IsPrimary" boolean NOT NULL,
        "TaxJurisdiction" text,
        "Currency" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_BankDetails" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_BankDetails_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Certifications" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Name" text NOT NULL,
        "IssuingOrganization" text,
        "IssueDate" timestamp with time zone,
        "ExpiryDate" timestamp with time zone,
        "CredentialId" text,
        "CredentialUrl" text,
        "DoesNotExpire" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Certifications" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Certifications_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Dependents" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Relationship" text NOT NULL,
        "DateOfBirth" timestamp with time zone,
        "Gender" integer,
        "IsInsuranceBeneficiary" boolean NOT NULL,
        "PhoneNumber" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Dependents" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Dependents_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Educations" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Institution" text NOT NULL,
        "Degree" text NOT NULL,
        "FieldOfStudy" text,
        "StartDate" timestamp with time zone,
        "EndDate" timestamp with time zone,
        "Grade" text,
        "Percentage" numeric,
        "IsHighest" boolean NOT NULL,
        "Country" text,
        "University" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Educations" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Educations_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmergencyContacts" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Relationship" text NOT NULL,
        "PhoneNumber" text NOT NULL,
        "SecondaryPhone" text,
        "Email" text,
        "Address" text,
        "IsPrimary" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_EmergencyContacts" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmergencyContacts_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmployeeDocuments" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "DocumentType" integer NOT NULL,
        "FileName" text NOT NULL,
        "FileUrl" text NOT NULL,
        "FileSize" bigint NOT NULL,
        "MimeType" text NOT NULL,
        "ExpiryDate" timestamp with time zone,
        "IsVerified" boolean NOT NULL,
        "VerifiedBy" uuid,
        "VerifiedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_EmployeeDocuments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeeDocuments_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmployeeHistories" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Action" integer NOT NULL,
        "ActionDate" timestamp with time zone NOT NULL,
        "PreviousValue" text,
        "NewValue" text,
        "Reason" text,
        "ApprovedBy" uuid,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_EmployeeHistories" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeeHistories_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "SalaryStructures" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "BasicSalary" numeric NOT NULL,
        "GrossSalary" numeric NOT NULL,
        "CTC" numeric NOT NULL,
        "Currency" text NOT NULL,
        "EffectiveFrom" timestamp with time zone NOT NULL,
        "EffectiveTo" timestamp with time zone,
        "IsCurrent" boolean NOT NULL,
        "ComponentDetails" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_SalaryStructures" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_SalaryStructures_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Skills" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Category" text,
        "Proficiency" integer NOT NULL,
        "YearsOfExperience" integer,
        "LastUsedDate" timestamp with time zone,
        "IsEndorsed" boolean NOT NULL,
        "EndorsedBy" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Skills" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Skills_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WorkExperiences" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "CompanyName" text NOT NULL,
        "Designation" text,
        "StartDate" timestamp with time zone,
        "EndDate" timestamp with time zone,
        "Description" text,
        "IsCurrent" boolean NOT NULL,
        "ReasonForLeaving" text,
        "ReferenceName" text,
        "ReferencePhone" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_WorkExperiences" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_WorkExperiences_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ExpenseApprovals" (
        "Id" uuid NOT NULL,
        "ClaimId" uuid NOT NULL,
        "ApproverId" uuid NOT NULL,
        "Level" integer NOT NULL,
        "Status" integer NOT NULL,
        "Comments" text,
        "ApprovedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "ExpenseClaimId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_ExpenseApprovals" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ExpenseApprovals_ExpenseClaims_ExpenseClaimId" FOREIGN KEY ("ExpenseClaimId") REFERENCES "ExpenseClaims" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ExpenseItems" (
        "Id" uuid NOT NULL,
        "ClaimId" uuid NOT NULL,
        "Category" integer NOT NULL,
        "Description" text NOT NULL,
        "Amount" numeric NOT NULL,
        "Currency" text NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "ReceiptUrl" text,
        "IsReimbursable" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "ExpenseClaimId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ExpenseItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ExpenseItems_ExpenseClaims_ExpenseClaimId" FOREIGN KEY ("ExpenseClaimId") REFERENCES "ExpenseClaims" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "FeedbackAnswers" (
        "Id" uuid NOT NULL,
        "Feedback360Id" uuid NOT NULL,
        "Question" text NOT NULL,
        "Rating" numeric,
        "Comments" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_FeedbackAnswers" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_FeedbackAnswers_Feedback360s_Feedback360Id" FOREIGN KEY ("Feedback360Id") REFERENCES "Feedback360s" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TicketAttachments" (
        "Id" uuid NOT NULL,
        "TicketId" uuid NOT NULL,
        "FileName" text NOT NULL,
        "FileUrl" text NOT NULL,
        "FileSize" bigint NOT NULL,
        "ContentType" text NOT NULL,
        "UploadedAt" timestamp with time zone NOT NULL,
        "TenantId" text NOT NULL,
        "HelpdeskTicketId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TicketAttachments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_TicketAttachments_HelpdeskTickets_HelpdeskTicketId" FOREIGN KEY ("HelpdeskTicketId") REFERENCES "HelpdeskTickets" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TicketComments" (
        "Id" uuid NOT NULL,
        "TicketId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Content" text NOT NULL,
        "IsInternal" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "HelpdeskTicketId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TicketComments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_TicketComments_HelpdeskTickets_HelpdeskTicketId" FOREIGN KEY ("HelpdeskTicketId") REFERENCES "HelpdeskTickets" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "JobApplications" (
        "Id" uuid NOT NULL,
        "JobPostingId" uuid NOT NULL,
        "CandidateId" uuid NOT NULL,
        "AppliedAt" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "ScreeningScore" numeric,
        "RecruiterNotes" text,
        "AssignedTo" uuid,
        "RejectionReason" text,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_JobApplications" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_JobApplications_Candidates_CandidateId" FOREIGN KEY ("CandidateId") REFERENCES "Candidates" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_JobApplications_JobPostings_JobPostingId" FOREIGN KEY ("JobPostingId") REFERENCES "JobPostings" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LearningPathCourses" (
        "Id" uuid NOT NULL,
        "LearningPathId" uuid NOT NULL,
        "CourseId" uuid NOT NULL,
        "Order" integer NOT NULL,
        "IsRequired" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LearningPathCourses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LearningPathCourses_LearningPaths_LearningPathId" FOREIGN KEY ("LearningPathId") REFERENCES "LearningPaths" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LeaveComments" (
        "Id" uuid NOT NULL,
        "LeaveApplicationId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Comment" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LeaveComments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LeaveComments_LeaveApplications_LeaveApplicationId" FOREIGN KEY ("LeaveApplicationId") REFERENCES "LeaveApplications" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "OKRItems" (
        "Id" uuid NOT NULL,
        "OKRId" uuid NOT NULL,
        "ObjectiveTitle" text NOT NULL,
        "ObjectiveDescription" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_OKRItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_OKRItems_OKRs_OKRId" FOREIGN KEY ("OKRId") REFERENCES "OKRs" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmployeePayrolls" (
        "Id" uuid NOT NULL,
        "PayrollRunId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "DepartmentId" uuid NOT NULL,
        "DesignationId" uuid NOT NULL,
        "BasicSalary" numeric NOT NULL,
        "GrossSalary" numeric NOT NULL,
        "TotalEarnings" numeric NOT NULL,
        "TotalDeductions" numeric NOT NULL,
        "NetPayable" numeric NOT NULL,
        "AttendanceDays" integer NOT NULL,
        "LOPDays" integer NOT NULL,
        "WorkingDays" integer NOT NULL,
        "PaidDays" integer NOT NULL,
        "OvertimeHours" numeric NOT NULL,
        "OvertimeAmount" numeric NOT NULL,
        "IsProcessed" boolean NOT NULL,
        "ProcessedAt" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_EmployeePayrolls" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeePayrolls_PayrollRuns_PayrollRunId" FOREIGN KEY ("PayrollRunId") REFERENCES "PayrollRuns" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "PayrollAuditLogs" (
        "Id" uuid NOT NULL,
        "PayrollRunId" uuid NOT NULL,
        "Action" text NOT NULL,
        "PerformedBy" text NOT NULL,
        "PerformedAt" timestamp with time zone NOT NULL,
        "OldValue" text,
        "NewValue" text,
        "Details" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_PayrollAuditLogs" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PayrollAuditLogs_PayrollRuns_PayrollRunId" FOREIGN KEY ("PayrollRunId") REFERENCES "PayrollRuns" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ReviewCriterias" (
        "Id" uuid NOT NULL,
        "PerformanceReviewId" uuid NOT NULL,
        "Category" text NOT NULL,
        "CriteriaName" text NOT NULL,
        "Rating" numeric,
        "Weight" numeric NOT NULL,
        "Comments" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ReviewCriterias" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ReviewCriterias_PerformanceReviews_PerformanceReviewId" FOREIGN KEY ("PerformanceReviewId") REFERENCES "PerformanceReviews" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Boards" (
        "Id" uuid NOT NULL,
        "ProjectId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Type" integer NOT NULL,
        "Columns" text,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Boards" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Boards_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Epics" (
        "Id" uuid NOT NULL,
        "ProjectId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "StartDate" timestamp with time zone,
        "TargetDate" timestamp with time zone,
        "ProgressPercentage" numeric NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Epics" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Epics_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ProjectMembers" (
        "Id" uuid NOT NULL,
        "ProjectId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Role" integer NOT NULL,
        "AllocationPercentage" numeric NOT NULL,
        "JoinedAt" timestamp with time zone NOT NULL,
        "LeftAt" timestamp with time zone,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ProjectMembers" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ProjectMembers_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Sprints" (
        "Id" uuid NOT NULL,
        "ProjectId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Goal" text,
        "StartDate" timestamp with time zone NOT NULL,
        "EndDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Sprints" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Sprints_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ReportAccesses" (
        "Id" uuid NOT NULL,
        "TemplateId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Permission" text NOT NULL,
        "GrantedAt" timestamp with time zone NOT NULL,
        "GrantedBy" uuid NOT NULL,
        "TenantId" text NOT NULL,
        "ReportTemplateId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_ReportAccesses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ReportAccesses_ReportTemplates_ReportTemplateId" FOREIGN KEY ("ReportTemplateId") REFERENCES "ReportTemplates" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "ReportInstances" (
        "Id" uuid NOT NULL,
        "TemplateId" uuid NOT NULL,
        "GeneratedBy" uuid NOT NULL,
        "GeneratedAt" timestamp with time zone NOT NULL,
        "Parameters" text,
        "FileUrl" text,
        "FileSize" bigint,
        "Status" integer NOT NULL,
        "TenantId" text NOT NULL,
        "ReportTemplateId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_ReportInstances" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ReportInstances_ReportTemplates_ReportTemplateId" FOREIGN KEY ("ReportTemplateId") REFERENCES "ReportTemplates" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "RolePermissions" (
        "Id" uuid NOT NULL,
        "RoleId" uuid NOT NULL,
        "Permission" text NOT NULL,
        "Module" text,
        "Description" text,
        CONSTRAINT "PK_RolePermissions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_RolePermissions_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "UserRoles" (
        "UserId" uuid NOT NULL,
        "RoleId" uuid NOT NULL,
        "AssignedAt" timestamp with time zone NOT NULL,
        "AssignedBy" uuid,
        CONSTRAINT "PK_UserRoles" PRIMARY KEY ("UserId", "RoleId"),
        CONSTRAINT "FK_UserRoles_ApplicationUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "ApplicationUsers" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_UserRoles_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "EmployeeSalaryAssignments" (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ComponentDefId" uuid NOT NULL,
        "Amount" numeric NOT NULL,
        "Percentage" numeric,
        "EffectiveFrom" date NOT NULL,
        "EffectiveTo" date,
        "IsCurrent" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_EmployeeSalaryAssignments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeeSalaryAssignments_SalaryComponentDefs_ComponentDefId" FOREIGN KEY ("ComponentDefId") REFERENCES "SalaryComponentDefs" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "AuditTrails" (
        "Id" uuid NOT NULL,
        "AuditLogId" uuid NOT NULL,
        "FieldName" text NOT NULL,
        "OldValue" text,
        "NewValue" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_AuditTrails" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_AuditTrails_SystemAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES "SystemAuditLogs" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TravelApprovals" (
        "Id" uuid NOT NULL,
        "TravelRequestId" uuid NOT NULL,
        "ApproverId" uuid NOT NULL,
        "Level" integer NOT NULL,
        "Status" integer NOT NULL,
        "Comments" text,
        "ApprovedAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_TravelApprovals" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_TravelApprovals_TravelRequests_TravelRequestId" FOREIGN KEY ("TravelRequestId") REFERENCES "TravelRequests" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TravelExpenses" (
        "Id" uuid NOT NULL,
        "TravelRequestId" uuid NOT NULL,
        "Category" text NOT NULL,
        "Description" text NOT NULL,
        "Amount" numeric NOT NULL,
        "Currency" text NOT NULL,
        "ReceiptUrl" text,
        "Date" timestamp with time zone NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TravelExpenses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_TravelExpenses_TravelRequests_TravelRequestId" FOREIGN KEY ("TravelRequestId") REFERENCES "TravelRequests" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "TravelItineraries" (
        "Id" uuid NOT NULL,
        "TravelRequestId" uuid NOT NULL,
        "Day" integer NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "Activity" text NOT NULL,
        "Location" text NOT NULL,
        "StartTime" timestamp with time zone NOT NULL,
        "EndTime" timestamp with time zone NOT NULL,
        "Notes" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_TravelItineraries" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_TravelItineraries_TravelRequests_TravelRequestId" FOREIGN KEY ("TravelRequestId") REFERENCES "TravelRequests" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "NotificationRules" (
        "Id" uuid NOT NULL,
        "WorkflowDefinitionId" uuid NOT NULL,
        "EventType" text NOT NULL,
        "Channel" text NOT NULL,
        "TemplateId" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_NotificationRules" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_NotificationRules_WorkflowDefinitions_WorkflowDefinitionId" FOREIGN KEY ("WorkflowDefinitionId") REFERENCES "WorkflowDefinitions" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WorkflowSteps" (
        "Id" uuid NOT NULL,
        "WorkflowDefinitionId" uuid NOT NULL,
        "StepNumber" integer NOT NULL,
        "Name" text NOT NULL,
        "ApproverType" integer NOT NULL,
        "ApproverId" uuid,
        "Action" integer NOT NULL,
        "TimeoutHours" integer,
        "IsRequired" boolean NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_WorkflowSteps" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_WorkflowSteps_WorkflowDefinitions_WorkflowDefinitionId" FOREIGN KEY ("WorkflowDefinitionId") REFERENCES "WorkflowDefinitions" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "WorkflowActions" (
        "Id" uuid NOT NULL,
        "WorkflowInstanceId" uuid NOT NULL,
        "StepId" uuid NOT NULL,
        "ApproverId" uuid NOT NULL,
        "Action" integer NOT NULL,
        "Comments" text,
        "ActionedAt" timestamp with time zone NOT NULL,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_WorkflowActions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_WorkflowActions_WorkflowInstances_WorkflowInstanceId" FOREIGN KEY ("WorkflowInstanceId") REFERENCES "WorkflowInstances" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "MessageReactions" (
        "Id" uuid NOT NULL,
        "MessageId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Emoji" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_MessageReactions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_MessageReactions_Messages_MessageId" FOREIGN KEY ("MessageId") REFERENCES "Messages" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "MessageReads" (
        "Id" uuid NOT NULL,
        "MessageId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "ReadAt" timestamp with time zone NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_MessageReads" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_MessageReads_Messages_MessageId" FOREIGN KEY ("MessageId") REFERENCES "Messages" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Lessons" (
        "Id" uuid NOT NULL,
        "ModuleId" uuid NOT NULL,
        "Title" text NOT NULL,
        "ContentType" integer NOT NULL,
        "ContentUrl" text,
        "ContentText" text,
        "Duration" integer NOT NULL,
        "Order" integer NOT NULL,
        "IsPreview" boolean NOT NULL,
        "CourseModuleId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Lessons" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Lessons_CourseModules_CourseModuleId" FOREIGN KEY ("CourseModuleId") REFERENCES "CourseModules" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LessonProgresses" (
        "Id" uuid NOT NULL,
        "EnrollmentId" uuid NOT NULL,
        "LessonId" uuid NOT NULL,
        "Status" integer NOT NULL,
        "StartedAt" timestamp with time zone,
        "CompletedAt" timestamp with time zone,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_LessonProgresses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LessonProgresses_Enrollments_EnrollmentId" FOREIGN KEY ("EnrollmentId") REFERENCES "Enrollments" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DocumentAccessLogs" (
        "Id" uuid NOT NULL,
        "DocumentId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "AccessedAt" timestamp with time zone NOT NULL,
        "Action" integer NOT NULL,
        "IpAddress" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DocumentAccessLogs" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DocumentAccessLogs_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DocumentShares" (
        "Id" uuid NOT NULL,
        "DocumentId" uuid NOT NULL,
        "SharedWithUserId" uuid NOT NULL,
        "Permission" integer NOT NULL,
        "SharedBy" uuid NOT NULL,
        "SharedAt" timestamp with time zone NOT NULL,
        "ExpiresAt" timestamp with time zone,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DocumentShares" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DocumentShares_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "DocumentVersions" (
        "Id" uuid NOT NULL,
        "DocumentId" uuid NOT NULL,
        "VersionNumber" integer NOT NULL,
        "FileUrl" text NOT NULL,
        "FileSize" bigint NOT NULL,
        "UploadedBy" uuid NOT NULL,
        "ChangeNotes" text,
        "TenantId" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_DocumentVersions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DocumentVersions_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Interviews" (
        "Id" uuid NOT NULL,
        "JobApplicationId" uuid NOT NULL,
        "CandidateId" uuid NOT NULL,
        "InterviewerIds" text NOT NULL,
        "Round" integer NOT NULL,
        "Type" integer NOT NULL,
        "ScheduledAt" timestamp with time zone NOT NULL,
        "Duration" integer NOT NULL,
        "Location" text,
        "MeetingUrl" text,
        "Status" integer NOT NULL,
        "Rating" numeric,
        "OverallRecommendation" integer,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_Interviews" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Interviews_Candidates_CandidateId" FOREIGN KEY ("CandidateId") REFERENCES "Candidates" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Interviews_JobApplications_JobApplicationId" FOREIGN KEY ("JobApplicationId") REFERENCES "JobApplications" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "OfferLetters" (
        "Id" uuid NOT NULL,
        "JobApplicationId" uuid NOT NULL,
        "CandidateId" uuid NOT NULL,
        "EmployeeId" uuid,
        "Position" text NOT NULL,
        "DepartmentId" uuid NOT NULL,
        "DesignationId" uuid NOT NULL,
        "CTC" numeric NOT NULL,
        "BasicSalary" numeric NOT NULL,
        "JoiningDate" timestamp with time zone NOT NULL,
        "OfferExpiryDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "SentAt" timestamp with time zone,
        "AcceptedAt" timestamp with time zone,
        "RejectedAt" timestamp with time zone,
        "RejectionReason" text,
        "DocumentUrl" text,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_OfferLetters" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_OfferLetters_Candidates_CandidateId" FOREIGN KEY ("CandidateId") REFERENCES "Candidates" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_OfferLetters_JobApplications_JobApplicationId" FOREIGN KEY ("JobApplicationId") REFERENCES "JobApplications" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "KeyResults" (
        "Id" uuid NOT NULL,
        "GoalId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "TargetValue" numeric NOT NULL,
        "CurrentValue" numeric NOT NULL,
        "Unit" text,
        "Weight" numeric NOT NULL,
        "Status" integer NOT NULL,
        "TenantId" text NOT NULL,
        "OKRItemId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        CONSTRAINT "PK_KeyResults" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_KeyResults_Goals_GoalId" FOREIGN KEY ("GoalId") REFERENCES "Goals" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_KeyResults_OKRItems_OKRItemId" FOREIGN KEY ("OKRItemId") REFERENCES "OKRItems" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Allowances" (
        "Id" uuid NOT NULL,
        "EmployeePayrollId" uuid NOT NULL,
        "ComponentDefId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Amount" numeric NOT NULL,
        "Type" integer NOT NULL,
        "IsTaxable" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Allowances" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Allowances_EmployeePayrolls_EmployeePayrollId" FOREIGN KEY ("EmployeePayrollId") REFERENCES "EmployeePayrolls" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Deductions" (
        "Id" uuid NOT NULL,
        "EmployeePayrollId" uuid NOT NULL,
        "ComponentDefId" uuid NOT NULL,
        "Name" text NOT NULL,
        "Amount" numeric NOT NULL,
        "Type" integer NOT NULL,
        "IsStatutory" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Deductions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Deductions_EmployeePayrolls_EmployeePayrollId" FOREIGN KEY ("EmployeePayrollId") REFERENCES "EmployeePayrolls" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "LoanRepayments" (
        "Id" uuid NOT NULL,
        "LoanId" uuid NOT NULL,
        "EmployeePayrollId" uuid NOT NULL,
        "Amount" numeric NOT NULL,
        "PaidDate" timestamp with time zone NOT NULL,
        "RemainingBalance" numeric NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_LoanRepayments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LoanRepayments_EmployeePayrolls_EmployeePayrollId" FOREIGN KEY ("EmployeePayrollId") REFERENCES "EmployeePayrolls" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_LoanRepayments_Loans_LoanId" FOREIGN KEY ("LoanId") REFERENCES "Loans" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Payslips" (
        "Id" uuid NOT NULL,
        "EmployeePayrollId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Month" integer NOT NULL,
        "Year" integer NOT NULL,
        "PdfUrl" text NOT NULL,
        "GeneratedAt" timestamp with time zone NOT NULL,
        "IsViewed" boolean NOT NULL,
        "ViewedAt" timestamp with time zone,
        "TenantId" uuid NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Payslips" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Payslips_EmployeePayrolls_EmployeePayrollId" FOREIGN KEY ("EmployeePayrollId") REFERENCES "EmployeePayrolls" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "Stories" (
        "Id" uuid NOT NULL,
        "EpicId" uuid NOT NULL,
        "ProjectId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text,
        "AcceptanceCriteria" text,
        "StoryPoints" integer NOT NULL,
        "Status" integer NOT NULL,
        "Priority" integer NOT NULL,
        "AssignedTo" uuid,
        "SprintId" uuid,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        CONSTRAINT "PK_Stories" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Stories_Epics_EpicId" FOREIGN KEY ("EpicId") REFERENCES "Epics" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE TABLE "InterviewFeedbacks" (
        "Id" uuid NOT NULL,
        "InterviewId" uuid NOT NULL,
        "InterviewerId" uuid NOT NULL,
        "TechnicalRating" integer NOT NULL,
        "CommunicationRating" integer NOT NULL,
        "CulturalFitRating" integer NOT NULL,
        "ProblemSolvingRating" integer NOT NULL,
        "OverallRating" integer NOT NULL,
        "Strengths" text NOT NULL,
        "Weaknesses" text NOT NULL,
        "Comments" text,
        "Recommendation" integer NOT NULL,
        "SubmittedAt" timestamp with time zone NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        "CreatedBy" text,
        "UpdatedBy" text,
        "IsDeleted" boolean NOT NULL,
        "TenantId" uuid NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_InterviewFeedbacks" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_InterviewFeedbacks_Interviews_InterviewId" FOREIGN KEY ("InterviewId") REFERENCES "Interviews" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Allowances_EmployeePayrollId" ON "Allowances" ("EmployeePayrollId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Allowances_TenantId" ON "Allowances" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AnalyticsEvents_TenantId" ON "AnalyticsEvents" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Appraisals_TenantId" ON "Appraisals" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ApprovalMatrices_TenantId" ON "ApprovalMatrices" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AssessmentAttempts_TenantId" ON "AssessmentAttempts" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AssessmentQuestions_AssessmentId" ON "AssessmentQuestions" ("AssessmentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AssessmentQuestions_TenantId" ON "AssessmentQuestions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Assessments_TenantId" ON "Assessments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AttendancePolicies_TenantId" ON "AttendancePolicies" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AttendanceRecords_TenantId" ON "AttendanceRecords" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AttendanceRegularizations_TenantId" ON "AttendanceRegularizations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AttendanceSummaries_TenantId" ON "AttendanceSummaries" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AuditTrails_AuditLogId" ON "AuditTrails" ("AuditLogId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_AuditTrails_TenantId" ON "AuditTrails" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_BankDetails_EmployeeId" ON "BankDetails" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_BankDetails_TenantId" ON "BankDetails" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Boards_ProjectId" ON "Boards" ("ProjectId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Boards_TenantId" ON "Boards" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Bonuses_TenantId" ON "Bonuses" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Branches_TenantId" ON "Branches" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Bugs_TenantId" ON "Bugs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CalibrationEntries_CalibrationSessionId" ON "CalibrationEntries" ("CalibrationSessionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CalibrationEntries_TenantId" ON "CalibrationEntries" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CalibrationSessions_TenantId" ON "CalibrationSessions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Candidates_TenantId" ON "Candidates" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Certificates_TenantId" ON "Certificates" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Certifications_EmployeeId" ON "Certifications" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Certifications_TenantId" ON "Certifications" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ChatChannels_TenantId" ON "ChatChannels" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ChatNotifications_TenantId" ON "ChatNotifications" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Comments_TenantId" ON "Comments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Companies_TenantId" ON "Companies" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CompanyPolicies_TenantId" ON "CompanyPolicies" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CompOffs_TenantId" ON "CompOffs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ConversationParticipants_ChatChannelId" ON "ConversationParticipants" ("ChatChannelId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ConversationParticipants_ConversationId" ON "ConversationParticipants" ("ConversationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ConversationParticipants_TenantId" ON "ConversationParticipants" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Conversations_TenantId" ON "Conversations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CourseModules_CourseId" ON "CourseModules" ("CourseId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_CourseModules_TenantId" ON "CourseModules" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Courses_TenantId" ON "Courses" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Dashboards_TenantId" ON "Dashboards" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DashboardShares_DashboardId" ON "DashboardShares" ("DashboardId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DashboardShares_TenantId" ON "DashboardShares" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DashboardWidgets_DashboardId" ON "DashboardWidgets" ("DashboardId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DashboardWidgets_TenantId" ON "DashboardWidgets" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DataChangeLogs_TenantId" ON "DataChangeLogs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Deductions_EmployeePayrollId" ON "Deductions" ("EmployeePayrollId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Deductions_TenantId" ON "Deductions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Delegations_TenantId" ON "Delegations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Departments_DepartmentId" ON "Departments" ("DepartmentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Departments_TenantId" ON "Departments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Dependents_EmployeeId" ON "Dependents" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Dependents_TenantId" ON "Dependents" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Designations_TenantId" ON "Designations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentAccessLogs_DocumentId" ON "DocumentAccessLogs" ("DocumentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentAccessLogs_TenantId" ON "DocumentAccessLogs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentFolders_DocumentFolderId" ON "DocumentFolders" ("DocumentFolderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentFolders_TenantId" ON "DocumentFolders" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Documents_DocumentFolderId" ON "Documents" ("DocumentFolderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Documents_TenantId" ON "Documents" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentShares_DocumentId" ON "DocumentShares" ("DocumentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentShares_TenantId" ON "DocumentShares" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentTemplates_TenantId" ON "DocumentTemplates" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentVersions_DocumentId" ON "DocumentVersions" ("DocumentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_DocumentVersions_TenantId" ON "DocumentVersions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Educations_EmployeeId" ON "Educations" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Educations_TenantId" ON "Educations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmailQueues_TenantId" ON "EmailQueues" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmergencyContacts_EmployeeId" ON "EmergencyContacts" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmergencyContacts_TenantId" ON "EmergencyContacts" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeDocuments_EmployeeId" ON "EmployeeDocuments" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeDocuments_TenantId" ON "EmployeeDocuments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeHistories_EmployeeId" ON "EmployeeHistories" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeHistories_TenantId" ON "EmployeeHistories" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeePayrolls_PayrollRunId" ON "EmployeePayrolls" ("PayrollRunId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeePayrolls_TenantId" ON "EmployeePayrolls" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Employees_TenantId" ON "Employees" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeSalaryAssignments_ComponentDefId" ON "EmployeeSalaryAssignments" ("ComponentDefId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeSalaryAssignments_TenantId" ON "EmployeeSalaryAssignments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_EmployeeTaxDeclarations_TenantId" ON "EmployeeTaxDeclarations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Enrollments_CourseId" ON "Enrollments" ("CourseId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Enrollments_TenantId" ON "Enrollments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Epics_ProjectId" ON "Epics" ("ProjectId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Epics_TenantId" ON "Epics" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpenseApprovals_ExpenseClaimId" ON "ExpenseApprovals" ("ExpenseClaimId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpenseApprovals_TenantId" ON "ExpenseApprovals" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpenseCategories_TenantId" ON "ExpenseCategories" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpenseClaims_TenantId" ON "ExpenseClaims" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpenseItems_ExpenseClaimId" ON "ExpenseItems" ("ExpenseClaimId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpenseItems_TenantId" ON "ExpenseItems" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ExpensePolicies_TenantId" ON "ExpensePolicies" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Faqs_TenantId" ON "Faqs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Feedback360s_TenantId" ON "Feedback360s" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_FeedbackAnswers_Feedback360Id" ON "FeedbackAnswers" ("Feedback360Id");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_FeedbackAnswers_TenantId" ON "FeedbackAnswers" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_GeoFences_TenantId" ON "GeoFences" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Goals_TenantId" ON "Goals" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Grades_TenantId" ON "Grades" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_HelpdeskTickets_TenantId" ON "HelpdeskTickets" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_HolidayCalendarEntries_TenantId" ON "HolidayCalendarEntries" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Holidays_TenantId" ON "Holidays" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_InterviewFeedbacks_InterviewId" ON "InterviewFeedbacks" ("InterviewId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_InterviewFeedbacks_TenantId" ON "InterviewFeedbacks" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Interviews_CandidateId" ON "Interviews" ("CandidateId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Interviews_JobApplicationId" ON "Interviews" ("JobApplicationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Interviews_TenantId" ON "Interviews" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_JobApplications_CandidateId" ON "JobApplications" ("CandidateId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_JobApplications_JobPostingId" ON "JobApplications" ("JobPostingId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_JobApplications_TenantId" ON "JobApplications" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_JobPostings_TenantId" ON "JobPostings" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_KeyResults_GoalId" ON "KeyResults" ("GoalId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_KeyResults_OKRItemId" ON "KeyResults" ("OKRItemId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_KeyResults_TenantId" ON "KeyResults" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_KnowledgeArticles_TenantId" ON "KnowledgeArticles" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_KPIs_TenantId" ON "KPIs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LearningPathCourses_LearningPathId" ON "LearningPathCourses" ("LearningPathId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LearningPathCourses_TenantId" ON "LearningPathCourses" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LearningPaths_TenantId" ON "LearningPaths" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveAccrualPolicies_TenantId" ON "LeaveAccrualPolicies" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveApplications_TenantId" ON "LeaveApplications" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveApprovalMatrices_TenantId" ON "LeaveApprovalMatrices" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveBalances_TenantId" ON "LeaveBalances" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveComments_LeaveApplicationId" ON "LeaveComments" ("LeaveApplicationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveComments_TenantId" ON "LeaveComments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeavePolicies_TenantId" ON "LeavePolicies" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LeaveTypes_TenantId" ON "LeaveTypes" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LessonProgresses_EnrollmentId" ON "LessonProgresses" ("EnrollmentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LessonProgresses_TenantId" ON "LessonProgresses" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Lessons_CourseModuleId" ON "Lessons" ("CourseModuleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Lessons_TenantId" ON "Lessons" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LoanRepayments_EmployeePayrollId" ON "LoanRepayments" ("EmployeePayrollId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LoanRepayments_LoanId" ON "LoanRepayments" ("LoanId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LoanRepayments_TenantId" ON "LoanRepayments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Loans_TenantId" ON "Loans" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_LoginHistories_TenantId" ON "LoginHistories" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_MessageReactions_MessageId" ON "MessageReactions" ("MessageId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_MessageReactions_TenantId" ON "MessageReactions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_MessageReads_MessageId" ON "MessageReads" ("MessageId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_MessageReads_TenantId" ON "MessageReads" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Messages_ConversationId" ON "Messages" ("ConversationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Messages_TenantId" ON "Messages" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_NotificationDeliveryLogs_TenantId" ON "NotificationDeliveryLogs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_NotificationGroups_TenantId" ON "NotificationGroups" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_NotificationPreferences_TenantId" ON "NotificationPreferences" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_NotificationRules_TenantId" ON "NotificationRules" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_NotificationRules_WorkflowDefinitionId" ON "NotificationRules" ("WorkflowDefinitionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Notifications_TenantId" ON "Notifications" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_NotificationTemplates_TenantId" ON "NotificationTemplates" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OfferLetters_CandidateId" ON "OfferLetters" ("CandidateId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OfferLetters_JobApplicationId" ON "OfferLetters" ("JobApplicationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OfferLetters_TenantId" ON "OfferLetters" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OKRItems_OKRId" ON "OKRItems" ("OKRId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OKRItems_TenantId" ON "OKRItems" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OKRs_TenantId" ON "OKRs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_OnboardingChecklists_TenantId" ON "OnboardingChecklists" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_PayrollAuditLogs_PayrollRunId" ON "PayrollAuditLogs" ("PayrollRunId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_PayrollAuditLogs_TenantId" ON "PayrollAuditLogs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_PayrollRuns_TenantId" ON "PayrollRuns" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Payslips_EmployeePayrollId" ON "Payslips" ("EmployeePayrollId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Payslips_TenantId" ON "Payslips" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_PerformanceReviews_TenantId" ON "PerformanceReviews" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ProjectMembers_ProjectId" ON "ProjectMembers" ("ProjectId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ProjectMembers_TenantId" ON "ProjectMembers" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Projects_TenantId" ON "Projects" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_PushNotifications_TenantId" ON "PushNotifications" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReportAccesses_ReportTemplateId" ON "ReportAccesses" ("ReportTemplateId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReportAccesses_TenantId" ON "ReportAccesses" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReportInstances_ReportTemplateId" ON "ReportInstances" ("ReportTemplateId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReportInstances_TenantId" ON "ReportInstances" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReportTemplates_TenantId" ON "ReportTemplates" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReviewCriterias_PerformanceReviewId" ON "ReviewCriterias" ("PerformanceReviewId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ReviewCriterias_TenantId" ON "ReviewCriterias" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_RolePermissions_RoleId" ON "RolePermissions" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_SalaryComponentDefs_TenantId" ON "SalaryComponentDefs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_SalaryStructures_EmployeeId" ON "SalaryStructures" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_SalaryStructures_TenantId" ON "SalaryStructures" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ScheduledReports_TenantId" ON "ScheduledReports" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_ShiftAssignments_TenantId" ON "ShiftAssignments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Shifts_TenantId" ON "Shifts" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Skills_EmployeeId" ON "Skills" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Skills_TenantId" ON "Skills" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_SmsQueues_TenantId" ON "SmsQueues" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Sprints_ProjectId" ON "Sprints" ("ProjectId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Sprints_TenantId" ON "Sprints" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Stories_EpicId" ON "Stories" ("EpicId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_Stories_TenantId" ON "Stories" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_SystemAuditLogs_TenantId" ON "SystemAuditLogs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TaskItems_TenantId" ON "TaskItems" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TaxConfigurations_TenantId" ON "TaxConfigurations" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TicketAttachments_HelpdeskTicketId" ON "TicketAttachments" ("HelpdeskTicketId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TicketAttachments_TenantId" ON "TicketAttachments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TicketCategories_TenantId" ON "TicketCategories" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TicketComments_HelpdeskTicketId" ON "TicketComments" ("HelpdeskTicketId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TicketComments_TenantId" ON "TicketComments" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TimeLogs_TenantId" ON "TimeLogs" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TrainingSchedules_TenantId" ON "TrainingSchedules" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelApprovals_TenantId" ON "TravelApprovals" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelApprovals_TravelRequestId" ON "TravelApprovals" ("TravelRequestId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelExpenses_TenantId" ON "TravelExpenses" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelExpenses_TravelRequestId" ON "TravelExpenses" ("TravelRequestId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelItineraries_TenantId" ON "TravelItineraries" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelItineraries_TravelRequestId" ON "TravelItineraries" ("TravelRequestId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_TravelRequests_TenantId" ON "TravelRequests" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_UserPermissions_UserId" ON "UserPermissions" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_UserPresences_TenantId" ON "UserPresences" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_UserRoles_RoleId" ON "UserRoles" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_VisaRequests_TenantId" ON "VisaRequests" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WidgetPresets_TenantId" ON "WidgetPresets" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WifiNetworks_TenantId" ON "WifiNetworks" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkExperiences_EmployeeId" ON "WorkExperiences" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkExperiences_TenantId" ON "WorkExperiences" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkflowActions_TenantId" ON "WorkflowActions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkflowActions_WorkflowInstanceId" ON "WorkflowActions" ("WorkflowInstanceId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkflowDefinitions_TenantId" ON "WorkflowDefinitions" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkflowInstances_TenantId" ON "WorkflowInstances" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkflowSteps_TenantId" ON "WorkflowSteps" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkflowSteps_WorkflowDefinitionId" ON "WorkflowSteps" ("WorkflowDefinitionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    CREATE INDEX "IX_WorkFromHomes_TenantId" ON "WorkFromHomes" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260716082003_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260716082003_InitialCreate', '9.0.0');
    END IF;
END $EF$;
COMMIT;

