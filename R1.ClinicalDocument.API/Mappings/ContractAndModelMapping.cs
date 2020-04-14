using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BusinessLogic.Abstraction.ClinicalDocument.Model;
using Contract.ClinicalDocument.Response;
using Contract.CliniclaDocument.Request;
using Contract.CliniclaDocument.Response;

namespace R1.ClinicalDocument.API.Mappings
{

    /// <summary>
    /// Contract Model Mapping
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ContractAndModelMapping : Profile
    {
        /// <summary>
        /// Constructor Contract Model Mapping
        /// </summary>
        public ContractAndModelMapping()
        {
            //Contract to business mapping
            CreateMap<DocumentDetailRequest, ClinicalDocumentInputModel>()
               .ForMember(dest => dest.FacilityCode, opt => opt.MapFrom(src => src.FacilityCode))
               .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
               .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src =>src.DocumentName.Trim()))
               .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
               .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.MimeType))
               .ForMember(dest => dest.SourceSystem, opt => opt.MapFrom(src => src.SourceSystem));

            //model to contract mapping
            CreateMap<ClinicalDocumentOutputModel, DocumentDetailResponse>()
               .ForMember(dest => dest.ResponseMessage, opt => opt.MapFrom(src => src.ResponseMessage));

            //model to contract mapping
            CreateMap<ClinicalDocumentMetadata, ClinicalDocumentMetadataResponse>()
                .ForMember(dest => dest.FacilityCode, opt => opt.MapFrom(src => src.FacilityCode))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.DocumentName))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => src.CreatedDateTime));

            //model to contract mapping
            CreateMap<ClinicalDocumentDetails, ClinicalDocumentResponse>();

        }

    }
}