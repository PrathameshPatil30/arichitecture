using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstraction.ClinicalDocument;
using BusinessLogic.Abstraction.ClinicalDocument.Model;
using Database.Abstraction.ClinicalDocument.Contract.UnitOfWork;
using entities=Database.ClinicalDocument.Entities;
using Microsoft.Extensions.Localization;
using Resources;
using Services.Contract;
using Services.Contract.Model;

namespace BusinessLogic.ClinicalDocument
{
    /// <summary>
    /// Clinical Document Business Logic
    /// </summary>
    public class ClinicalDocumentBusinessLogic : IClinicalDocumentBusinessLogic
    {
        #region Declarations

        /// <summary>
        /// Private property for injecting dependency ClinicalDocumentUnitOfWork interface
        /// </summary>
        private readonly IClinicalDocumentUnitOfWork _iClinicalDocumentUnitOfWork;

        /// <summary>
        /// Private property for injecting dependency mapper interface
        /// </summary>
        private readonly IMapper _iMapper;


        /// <summary>
        /// Private property for injecting dependency IStringLocalizer interface
        /// </summary>
        private readonly IStringLocalizer<SharedResources> _localizer;

        /// <summary>
        /// Private property for injecting dependency IDocumentServiceHelper interface
        /// </summary>
        private readonly IDocumentServiceHelper _iDocumentServiceHelper;

        #endregion

        #region Constructor

        /// <summary>
        /// Clinica Document BusinessLogic Constructor
        /// </summary>
        /// <param name="clinicalDocumentUnitOfWork">clinicalDocumentUnitOfWork</param>
        /// <param name="mapper">mapper</param>
        /// <param name="localizer">localizer</param>
        public ClinicalDocumentBusinessLogic(IClinicalDocumentUnitOfWork clinicalDocumentUnitOfWork,
                            IMapper mapper, IStringLocalizer<SharedResources> localizer, IDocumentServiceHelper iDocumentServiceHelper)
        {
            _iClinicalDocumentUnitOfWork = clinicalDocumentUnitOfWork ?? throw new ArgumentNullException(nameof(clinicalDocumentUnitOfWork));
            _iDocumentServiceHelper = iDocumentServiceHelper ?? throw new ArgumentNullException(nameof(iDocumentServiceHelper));
            _iMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        #endregion

        #region Methods


        #region Save Clinical Document
        /// <summary>
        /// SaveClinicalDetails
        /// </summary>
        /// <param name="clinicalDocumentInputModel">clinicalDocumentInputModel</param>
        /// <returns>response</returns>
        public async Task<ClinicalDocumentOutputModel> SaveClinicalDetails(ClinicalDocumentInputModel clinicalDocumentInputModel)
        {
            string status = await SaveClinicalDocumentDetailsForDocumentService(clinicalDocumentInputModel);

            //Map ClinicalDocumentInputModel to ClinicalDocuments entity
            entities.ClinicalDocumentMetadata clinicalDocuments = _iMapper.Map<entities.ClinicalDocumentMetadata>(clinicalDocumentInputModel);

            //Save metadat details
            var details = await _iClinicalDocumentUnitOfWork.SaveClinicalDetails(clinicalDocuments);

            //check for null or empty
            if (!string.IsNullOrEmpty(details))
            {
                //If the metadata details are saved but have error saving the document
                return SaveClinicalDocumentsResponse(status, details);
            }
            else
            {
                throw new Exception(message: _localizer["Exception_Error_Message"]);
            }
        }

        /// <summary>
        /// Set document inforamtion in clinical document model for saving document via document service
        /// </summary>
        /// <param name="clinicalDocumentInputModel"></param>
        /// <returns></returns>
        private async Task<string> SaveClinicalDocumentDetailsForDocumentService(ClinicalDocumentInputModel clinicalDocumentInputModel)
        {
            string status = string.Empty;
            //GUID doesnt needs assignment
            Guid key;

            //Map the busniess model to the documeny service model
            DocumentMetadata metadata = _iMapper.Map<DocumentMetadata>(clinicalDocumentInputModel);

            //Check if the document blob is null 
            if (clinicalDocumentInputModel.Document != null && clinicalDocumentInputModel.Document.Length > 0)
            {
                //check if the mimetype and sourcesystem is null or contain white spaces
                if (!string.IsNullOrWhiteSpace(clinicalDocumentInputModel.MimeType) && !string.IsNullOrWhiteSpace(clinicalDocumentInputModel.SourceSystem))
                {
                    // Call document service
                    (status, key) = await _iDocumentServiceHelper.UploadDocuments(metadata);
                    if (status == _localizer["DocumentServiceSuccessMessage"])
                    {
                        //Document saved successfully by Doc service
                        clinicalDocumentInputModel.DocumentReceived = true;
                        clinicalDocumentInputModel.DocumentId = key;
                    }
                    else
                    {
                        //Document service failed while saving document
                        clinicalDocumentInputModel.DocumentReceived = true;
                    }
                }
                else
                {
                    //Document blob received, but either of mime type or source system is missing
                    clinicalDocumentInputModel.DocumentReceived = true;
                }
            }
            else
            {
                //document not received
                clinicalDocumentInputModel.DocumentReceived = false;
            }

            return status;
        }

        /// <summary>
        /// Response message from doc service and saving to clinical document database
        /// </summary>
        /// <param name="status"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        private ClinicalDocumentOutputModel SaveClinicalDocumentsResponse(string status, string details)
        {
            if (status != _localizer["DocumentServiceSuccessMessage"])
            {
                var response = new ClinicalDocumentOutputModel
                {
                    ResponseMessage = string.Format("{0} {1}", (string.Format(_localizer["SuccessMessage"], details)), _localizer["DocumentServiceFailureMessage"])
                };

                return response;
            }
            else
            {
                //succesfully save the meta data details including the document
                var response = new ClinicalDocumentOutputModel
                {
                    ResponseMessage = string.Format(_localizer["SuccessMessage"], details)
                };

                return response;
            }
        }
        #endregion


        #region Get Clinical document details
        /// <summary>
        /// GetClinicalDocumentDetails
        /// </summary>
        /// <param name="accountDetails">accountDetails</param>
        /// <returns></returns>
        public async Task<ClinicalDocumentDetails> GetClinicalDocumentDetails(string facilityCode, string accountNumber)
        {
            var documentDetails = await _iClinicalDocumentUnitOfWork.GetClinicalDocumentDetails(facilityCode, accountNumber);
            var response = _iMapper.Map<ICollection<ClinicalDocumentMetadata>>(documentDetails);

            if (response != null && response.Any())
            {
                return await GetCrossWalkConfigurationResult(response);
            }
            else
            {
                return GetResponseResult(_localizer["EmptyMetadataMessage"], new List<ClinicalDocumentMetadata>());
            }

        }

        /// <summary>
        /// GetNullOrEmptyResponseResult
        /// </summary>
        /// <param name="ClinicalDocumentMetadata">ClinicalDocumentMetadata</param>
        /// <returns>Clinical Document Details</returns>
        private ClinicalDocumentDetails GetResponseResult(string Status, ICollection<ClinicalDocumentMetadata> ClinicalDocumentMetadata)
        {
            return new ClinicalDocumentDetails
            {
                Status = Status,
                ClinicalDocumentMetadata = ClinicalDocumentMetadata
            };
        }



        /// <summary>
        /// GetCrossWalkConfigurationResult
        /// </summary>
        /// <param name="clinicalDocumentMetadata">ClinicalDocumentMetadata</param>
        /// <returns>Clinical Document Details</returns>
        private async Task<ClinicalDocumentDetails> GetCrossWalkConfigurationResult(ICollection<ClinicalDocumentMetadata> clinicalDocumentMetadata)
        {
            ICollection<entities.DocumentTypeXwalk> clinicalDocumentConfiguration = await _iClinicalDocumentUnitOfWork.GetDocumentCrosswalkConfiguration();
            ICollection<entities.DocumentType> clinicalDocumentCrosswalk = await _iClinicalDocumentUnitOfWork.GetDocumentCrosswalkDetails();

            if (clinicalDocumentConfiguration != null && clinicalDocumentConfiguration.Any() && clinicalDocumentCrosswalk != null && clinicalDocumentCrosswalk.Any())
            {
                ICollection<ClinicalDocumentMetadata> latestClinicalDocumentmetadata = new List<ClinicalDocumentMetadata>();
                foreach (var documentdata in clinicalDocumentMetadata)
                {
                    if (!string.IsNullOrWhiteSpace(documentdata.FacilityCode) && !string.IsNullOrWhiteSpace(documentdata.DocumentName))
                    {
                        var crosswalkConfiguration = clinicalDocumentConfiguration.Where(x => x.DocumentName?.ToUpper() == documentdata.DocumentName?.ToUpper() && x.FacilityCode?.ToUpper() == documentdata.FacilityCode?.ToUpper() && x.IsActive)?.FirstOrDefault();
                        if (crosswalkConfiguration != null)
                        {
                            var crosswalkData = clinicalDocumentCrosswalk.Where(x => x.DocumentTypeId == crosswalkConfiguration.DocumentTypeId)?.FirstOrDefault();
                            if (crosswalkData != null)
                            {
                                documentdata.DocumentName = crosswalkData.StandardDocumentName;
                            }
                        }
                    }
                    latestClinicalDocumentmetadata.Add(documentdata);
                }
                return GetResponseResult(_localizer["ValidMetadataMessage"], latestClinicalDocumentmetadata);
            }
            else
            {
                return GetResponseResult(_localizer["ValidMetadataMessage"], clinicalDocumentMetadata);
            }

        }

        #endregion


        #endregion

    }
}
