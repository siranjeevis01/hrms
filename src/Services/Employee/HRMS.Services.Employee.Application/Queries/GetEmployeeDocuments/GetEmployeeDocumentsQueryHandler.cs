using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeDocuments;

public class GetEmployeeDocumentsQueryHandler : IRequestHandler<GetEmployeeDocumentsQuery, List<EmployeeDocumentDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeDocumentsQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDocumentDto>> Handle(GetEmployeeDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _context.Documents
            .Where(d => d.EmployeeId == request.EmployeeId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmployeeDocumentDto>>(documents);
    }
}
