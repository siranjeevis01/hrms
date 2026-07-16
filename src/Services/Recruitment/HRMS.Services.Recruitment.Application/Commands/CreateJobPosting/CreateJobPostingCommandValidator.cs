using FluentValidation;

namespace HRMS.Services.Recruitment.Application.Commands.CreateJobPosting;

public class CreateJobPostingCommandValidator : AbstractValidator<CreateJobPostingCommand>
{
    public CreateJobPostingCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("Department is required.");

        RuleFor(x => x.DesignationId)
            .NotEmpty().WithMessage("Designation is required.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("Branch is required.");

        RuleFor(x => x.HiringManagerId)
            .NotEmpty().WithMessage("Hiring manager is required.");

        RuleFor(x => x.RecruiterId)
            .NotEmpty().WithMessage("Recruiter is required.");

        RuleFor(x => x.HeadCount)
            .GreaterThan(0).WithMessage("Head count must be greater than 0.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
