using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Domain.Entities;

namespace HRMS.Services.Payroll.Application.Mappings;

public class PayrollMappingProfile : Profile
{
    public PayrollMappingProfile()
    {
        CreateMap<PayrollRun, PayrollRunDto>();
        CreateMap<EmployeePayroll, EmployeePayrollDto>();
        CreateMap<Allowance, AllowanceDto>();
        CreateMap<Deduction, DeductionDto>();
        CreateMap<SalaryComponentDef, SalaryComponentDefDto>();
        CreateMap<SalaryComponentDef, SalaryComponentItemDto>()
            .ForMember(d => d.ComponentDefId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Amount, o => o.Ignore())
            .ForMember(d => d.Percentage, o => o.Ignore());
        CreateMap<Bonus, BonusDto>();
        CreateMap<Loan, LoanDto>();
        CreateMap<LoanRepayment, LoanRepaymentDto>();
        CreateMap<TaxConfiguration, TaxConfigurationDto>();
        CreateMap<EmployeeTaxDeclaration, EmployeeTaxDeclarationDto>();
        CreateMap<Payslip, PayslipDto>();
    }
}
