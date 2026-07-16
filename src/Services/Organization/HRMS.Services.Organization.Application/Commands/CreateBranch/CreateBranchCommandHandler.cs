using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Events;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateBranch;

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, BranchDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateBranchCommandHandler(
        IOrganizationDbContext context,
        IMapper mapper,
        IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<BranchDto> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        var branch = Branch.Create(
            request.CompanyId,
            request.Name,
            request.Code,
            request.TenantId,
            request.IsHeadquarters);

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

        branch.UpdateDetails(
            phone: request.Phone,
            email: request.Email,
            managerId: request.ManagerId);

        _context.Branches.Add(branch);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new BranchCreatedEvent
        {
            BranchId = branch.Id,
            CompanyId = branch.CompanyId,
            Name = branch.Name,
            Code = branch.Code,
            IsHeadquarters = branch.IsHeadquarters,
            TenantId = branch.TenantId
        }, cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<BranchDto>(branch);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
