using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetBoard;

public class GetBoardQuery : IRequest<BoardDto?>
{
    public Guid ProjectId { get; set; }
}
