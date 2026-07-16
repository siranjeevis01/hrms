using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Interfaces;

public interface ITrainingDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<CourseModule> CourseModules { get; }
    DbSet<Lesson> Lessons { get; }
    DbSet<Enrollment> Enrollments { get; }
    DbSet<LessonProgress> LessonProgresses { get; }
    DbSet<Assessment> Assessments { get; }
    DbSet<AssessmentQuestion> AssessmentQuestions { get; }
    DbSet<AssessmentAttempt> AssessmentAttempts { get; }
    DbSet<Certificate> Certificates { get; }
    DbSet<LearningPath> LearningPaths { get; }
    DbSet<LearningPathCourse> LearningPathCourses { get; }
    DbSet<TrainingSchedule> TrainingSchedules { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
