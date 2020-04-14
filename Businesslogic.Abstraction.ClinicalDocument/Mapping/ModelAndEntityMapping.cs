using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using businesslogic=BusinessLogic.Abstraction.ClinicalDocument.Model;
using entities=Database.ClinicalDocument.Entities;
using service=Services.Contract.Model;

namespace BusinessLogic.Abstraction.ClinicalDocument.Mapping
{
    /// <summary>
    /// Model Entity Mapping
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ModelAndEntityMapping : Profile
    {
        /// <summary>
        /// Constructor for Model Entity Mapping
        /// </summary>
        public ModelAndEntityMapping()
        {
            //model to entity
            CreateMap<businesslogic.ClinicalDocumentInputModel, entities.ClinicalDocumentMetadata>()
           .ForMember(dest => dest.FacilityCode, opt => opt.MapFrom(src => src.FacilityCode))
           .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
           .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.DocumentName))
           .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
           .ForMember(dest => dest.SourceSystem, opt => opt.MapFrom(src => src.SourceSystem))
           .ForMember(dest => dest.IsDocumentReceived, opt => opt.MapFrom(src => src.DocumentReceived))
           .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.MimeType))
           .ForMember(dest => dest.ClinicalDocumentMetadataId, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore());

            //entity to model
            CreateMap<entities.ClinicalDocumentMetadata, businesslogic.ClinicalDocumentMetadata>()
            .ForMember(dest => dest.FacilityCode, opt => opt.MapFrom(src => src.FacilityCode))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.DocumentName))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => src.CreatedDateTime));

            //model to service
            CreateMap<businesslogic.ClinicalDocumentInputModel, service.DocumentMetadata>()
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.DocumentName))
                .ForMember(dest => dest.SourceSystem, opt => opt.MapFrom(src => src.SourceSystem))
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.MimeType));

        }
    }
}
