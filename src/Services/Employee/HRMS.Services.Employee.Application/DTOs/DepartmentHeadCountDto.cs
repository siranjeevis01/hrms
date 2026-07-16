namespace HRMS.Services.Employee.Application.DTOs;

public class DepartmentHeadCountDto
{
    public Guid DepartmentId { get; set; }
    public int HeadCount { get; set; }
    public int ActiveCount { get; set; }
    public int OnNoticeCount { get; set; }
}
