using HRMS.Services.Report.Domain.Enums;

namespace HRMS.Services.Report.Application.DTOs;

public class ReportCategoryDto
{
    public ReportCategory Category { get; set; }
    public int ReportCount { get; set; }
}
