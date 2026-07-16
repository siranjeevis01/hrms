-- ============================================
-- HRMS Pro - Initial Database Setup
-- ============================================
-- This script creates all required databases
-- for the HRMS Pro microservices architecture.
-- ============================================

-- Set character set and collation
SET NAMES utf8mb4;
SET CHARACTER SET utf8mb4;

-- ============================================
-- Create Databases
-- ============================================

-- Identity Service Database
-- Handles authentication, authorization, users, roles, permissions
CREATE DATABASE IF NOT EXISTS `hrms_identity`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Organization Service Database
-- Manages company structure, departments, designations, branches
CREATE DATABASE IF NOT EXISTS `hrms_organization`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Employee Service Database
-- Employee profiles, personal info, documents, history
CREATE DATABASE IF NOT EXISTS `hrms_employee`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Attendance Service Database
-- Clock in/out, attendance records, overtime, shifts
CREATE DATABASE IF NOT EXISTS `hrms_attendance`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Leave Service Database
-- Leave types, balances, requests, approvals
CREATE DATABASE IF NOT EXISTS `hrms_leave`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Payroll Service Database
-- Salary structures, payslips, deductions, tax calculations
CREATE DATABASE IF NOT EXISTS `hrms_payroll`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Recruitment Service Database
-- Job postings, applications, candidates, interviews
CREATE DATABASE IF NOT EXISTS `hrms_recruitment`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Notification Service Database
-- Email templates, push notifications, SMS, alerts
CREATE DATABASE IF NOT EXISTS `hrms_notification`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Project Service Database
-- Projects, tasks, milestones, time tracking
CREATE DATABASE IF NOT EXISTS `hrms_project`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Training Service Database
-- Training programs, courses, certifications, enrollments
CREATE DATABASE IF NOT EXISTS `hrms_training`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Performance Service Database
-- Performance reviews, goals, KPIs, feedback
CREATE DATABASE IF NOT EXISTS `hrms_performance`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Report Service Database
-- Report configurations, generated reports, analytics
CREATE DATABASE IF NOT EXISTS `hrms_report`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Audit Service Database
-- Audit logs, compliance tracking, activity history
CREATE DATABASE IF NOT EXISTS `hrms_audit`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- Dashboard Service Database
-- Dashboard widgets, configurations, widgets data
CREATE DATABASE IF NOT EXISTS `hrms_dashboard`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

-- ============================================
-- Create User and Grant Privileges
-- ============================================

-- Create application user if not exists
CREATE USER IF NOT EXISTS 'hrms_admin'@'%' IDENTIFIED BY 'Hrms@123456';

-- Grant privileges to all HRMS databases
GRANT ALL PRIVILEGES ON `hrms_identity`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_organization`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_employee`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_attendance`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_leave`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_payroll`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_recruitment`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_notification`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_project`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_training`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_performance`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_report`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_audit`.* TO 'hrms_admin'@'%';
GRANT ALL PRIVILEGES ON `hrms_dashboard`.* TO 'hrms_admin'@'%';

-- Also grant access to the init databases
GRANT ALL PRIVILEGES ON `hrms_identity`.* TO 'hrms_admin'@'localhost';

-- Flush privileges to apply changes
FLUSH PRIVILEGES;

-- ============================================
-- Create Common Tables
-- ============================================

-- Common audit columns table structure
-- These can be referenced by microservices

-- Error logs table (shared across services)
CREATE DATABASE IF NOT EXISTS `hrms_shared`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

GRANT ALL PRIVILEGES ON `hrms_shared`.* TO 'hrms_admin'@'%';
FLUSH PRIVILEGES;

USE `hrms_shared`;

CREATE TABLE IF NOT EXISTS `error_logs` (
    `id` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `service_name` VARCHAR(100) NOT NULL,
    `error_type` VARCHAR(100) NOT NULL,
    `message` TEXT NOT NULL,
    `stack_trace` LONGTEXT NULL,
    `request_url` VARCHAR(500) NULL,
    `request_method` VARCHAR(10) NULL,
    `request_body` JSON NULL,
    `user_id` VARCHAR(45) NULL,
    `ip_address` VARCHAR(45) NULL,
    `user_agent` TEXT NULL,
    `severity` ENUM('Trace', 'Debug', 'Information', 'Warning', 'Error', 'Fatal') NOT NULL DEFAULT 'Error',
    `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX `idx_error_logs_service` (`service_name`),
    INDEX `idx_error_logs_severity` (`severity`),
    INDEX `idx_error_logs_created` (`created_at`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- System health check table
CREATE TABLE IF NOT EXISTS `health_checks` (
    `id` BIGINT AUTO_INCREMENT PRIMARY KEY,
    `service_name` VARCHAR(100) NOT NULL,
    `status` ENUM('Healthy', 'Unhealthy', 'Degraded') NOT NULL,
    `response_time_ms` INT NULL,
    `message` TEXT NULL,
    `checked_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX `idx_health_checks_service` (`service_name`),
    INDEX `idx_health_checks_status` (`status`),
    INDEX `idx_health_checks_checked` (`checked_at`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- Display Results
-- ============================================

SELECT '========================================' AS '';
SELECT 'HRMS Pro - Database Setup Complete' AS 'Status';
SELECT '========================================' AS '';

SELECT 
    SCHEMA_NAME AS 'Database Name',
    DEFAULT_CHARACTER_SET_NAME AS 'Character Set',
    DEFAULT_COLLATION_NAME AS 'Collation'
FROM information_schema.SCHEMATA 
WHERE SCHEMA_NAME LIKE 'hrms_%'
ORDER BY SCHEMA_NAME;

SELECT '========================================' AS '';
SELECT COUNT(*) AS 'Total Databases Created'
FROM information_schema.SCHEMATA 
WHERE SCHEMA_NAME LIKE 'hrms_%';
SELECT '========================================' AS '';