using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.EnrollEmployee;

public class EnrollEmployeeCommandHandler : IRequestHandler<EnrollEmployeeCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public EnrollEmployeeCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(EnrollEmployeeCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

        if (course == null)
            throw new KeyNotFoundException($"Course with Id {request.CourseId} not found.");

        var existingEnrollment = await _context.Enrollments
            .AnyAsync(e => e.CourseId == request.CourseId && e.EmployeeId == request.EmployeeId && !e.IsDeleted, cancellationToken);

        if (existingEnrollment)
            throw new InvalidOperationException("Employee is already enrolled in this course.");

        var enrollment = Enrollment.Create(
            request.CourseId,
            request.EmployeeId,
            request.TenantId);

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync(cancellationToken);

        return enrollment.Id;
    }
}
