using AutoMapper;
using HRMS.Services.Document.Application.DTOs;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Application.Mappings;

public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<DocEntities.Document, DocumentDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.ToString()))
            .ForMember(d => d.VersionCount, opt => opt.MapFrom(s => s.Versions.Count));

        CreateMap<DocEntities.DocumentFolder, DocumentFolderDto>()
            .ForMember(d => d.DocumentCount, opt => opt.MapFrom(s => s.Documents.Count))
            .ForMember(d => d.SubFolderCount, opt => opt.MapFrom(s => s.SubFolders.Count));

        CreateMap<DocEntities.DocumentVersion, DocumentVersionDto>();

        CreateMap<DocEntities.DocumentShare, DocumentShareDto>()
            .ForMember(d => d.Permission, opt => opt.MapFrom(s => s.Permission.ToString()));

        CreateMap<DocEntities.DocumentAccessLog, DocumentAccessLogDto>()
            .ForMember(d => d.Action, opt => opt.MapFrom(s => s.Action.ToString()));

        CreateMap<DocEntities.DocumentTemplate, DocumentTemplateDto>()
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.ToString()));
    }
}
