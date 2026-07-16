using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetDepartmentHeadCount;

public class GetDepartmentHeadCountQuery : IRequest<List<DepartmentHeadCountDto>>
{
    public Guid? CompanyId { get; set; }
}

public class DepartmentHeadCountDto
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int HeadCount { get; set; }
    public int ActiveCount { get; set; }
    public int OnNoticeCount { get; set; }
}
