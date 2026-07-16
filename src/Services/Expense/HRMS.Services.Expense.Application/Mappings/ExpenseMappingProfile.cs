using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Domain.Entities;

namespace HRMS.Services.Expense.Application.Mappings;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<ExpenseClaim, ExpenseClaimDto>();
        CreateMap<ExpenseItem, ExpenseItemDto>();
        CreateMap<ExpensePolicy, ExpensePolicyDto>();
        CreateMap<ExpenseCategoryEntity, ExpenseCategoryDto>();
        CreateMap<ExpenseApproval, ExpenseApprovalDto>();
    }
}
