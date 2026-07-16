using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateBranch;

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, BranchDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBranchCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BranchDto> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Branch with ID {request.Id} not found.");

        branch.UpdateDetails(
            name: request.Name,
            code: request.Code,
            phone: request.Phone,
            email: request.Email,
            managerId: request.ManagerId,
            isHeadquarters: request.IsHeadquarters);

        if (request.Address != null)
        {
            var address = new Address(
                request.Address.Street,
                request.Address.City,
                request.Address.State,
                request.Address.Country,
                request.Address.PostalCode,
                request.Address.Latitude,
                request.Address.Longitude);
            branch.UpdateAddress(address);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == branch.CompanyId, cancellationToken);

        var dto = _mapper.Map<BranchDto>(branch);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
