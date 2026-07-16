using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetApprovalMatrices;

public class GetApprovalMatricesQueryHandler : IRequestHandler<GetApprovalMatricesQuery, List<ApprovalMatrixDto>>
{
    private readonly IWorkflowDbContext _context;
    private readonly IMapper _mapper;

    public GetApprovalMatricesQueryHandler(IWorkflowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ApprovalMatrixDto>> Handle(GetApprovalMatricesQuery request, CancellationToken cancellationToken)
    {
        var matrices = await _context.ApprovalMatrices
            .OrderBy(m => m.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ApprovalMatrixDto>>(matrices);
    }
}
