using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetBoard;

public class GetBoardQueryHandler : IRequestHandler<GetBoardQuery, BoardDto?>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetBoardQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BoardDto?> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards
            .FirstOrDefaultAsync(b => b.ProjectId == request.ProjectId, cancellationToken);

        if (board == null) return null;

        return _mapper.Map<BoardDto>(board);
    }
}
