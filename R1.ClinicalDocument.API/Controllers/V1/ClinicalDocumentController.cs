using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Abstraction.ClinicalDocument;
using BusinessLogic.Abstraction.ClinicalDocument.Model;
using Contract.ClinicalDocument.Response;
using Contract.CliniclaDocument.Request;
using Contract.CliniclaDocument.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Resources;

namespace R1.ClinicalDocument.API.Controllers.V1
{
    /// <summary>
    /// ClinicalDocumentController
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class ClinicalDocumentController : ControllerBase
    {

        #region Declarations

        /// <summary>
        /// Private property for injecting dependency IClinicalDocumentBusinessLogic interface
        /// </summary>
        private readonly IClinicalDocumentBusinessLogic _iClinicalDocumentBusinessLogic;

        /// <summary>
        /// Private property for injecting dependency mapper interface
        /// </summary>
        private readonly IMapper _iMapper;

        /// <summary>
        /// Private property for injecting dependency IStringLocalizer interface
        /// </summary>
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for initializing private properties
        /// </summary>
        public ClinicalDocumentController(IClinicalDocumentBusinessLogic clinicalDocumentBusinessLogic, IMapper mapper, IStringLocalizer<SharedResources> localizer)
        {
            _iClinicalDocumentBusinessLogic = clinicalDocumentBusinessLogic ?? throw new ArgumentNullException(nameof(clinicalDocumentBusinessLogic));
            _iMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Save clinical details to database 
        /// </summary>
        /// <param name="documentDetailRequest"></param>
        /// <returns></returns>
        [Route("Save")]
        [HttpPost]
        public async Task<IActionResult> SaveClinicaldetails([FromBody] DocumentDetailRequest documentDetailRequest)
        {
            DocumentDetailResponse clinicalDetailsResponse = null;

            //Map Contract request model to clinical document input model
            ClinicalDocumentInputModel clinicalDocumentInputModel = _iMapper.Map<ClinicalDocumentInputModel>(documentDetailRequest);

            // Save document details
            ClinicalDocumentOutputModel clinicalDetails = await _iClinicalDocumentBusinessLogic.SaveClinicalDetails(clinicalDocumentInputModel);

            if (clinicalDetails != null)
            {
                //Map clinical document output model to Contract response model
                clinicalDetailsResponse = _iMapper.Map<DocumentDetailResponse>(clinicalDetails);

                //return response
                return Ok(clinicalDetailsResponse);
            }
            else
            {
                //return badrequest
                return BadRequest(_localizer["BadRequestErrorMessage"]);
            }
        }

        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical documents</returns>
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> GetClinicalDetails([Required] string facilityCode, [Required] string accountNumber)
        {
            var result = await _iClinicalDocumentBusinessLogic.GetClinicalDocumentDetails(facilityCode, accountNumber);
            return Ok(_iMapper.Map<ClinicalDocumentResponse>(result));
        }
        #endregion
    }
}