using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Abstraction.ClinicalDocument.Contract.Repository;
using Database.Abstraction.ClinicalDocument.Contract.UnitOfWork;
using Database.ClinicalDocument.Common;
using Database.ClinicalDocument.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.ClinicalDocument.DataAccess
{
    /// <summary>
    /// Clinical Document Unit Of Work
    /// </summary>
    public class ClinicalDocumentUnitOfWork : UnitOfWork, IClinicalDocumentUnitOfWork
    {
        #region Declaration

        /// <summary>
        /// Private property for injecting dependency IClinicalDocumentRepository interface
        /// </summary>
        private readonly IClinicalDocumentRepository _iClinicalDocumentRepository;

        /// <summary>
        ///  Private property for injecting dependency IDocumentTypeXWalkRepository interface
        /// </summary>
        private readonly IDocumentTypeXWalkRepository _iDocumentTypeXWalkRepository;

        /// <summary>
        ///Private property for injecting dependency IDocumentTypeRepository interface
        /// </summary>
        private readonly IDocumentTypeRepository _iDocumentTypeRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for initializing private properties
        /// </summary>
        public ClinicalDocumentUnitOfWork(IClinicalDocumentRepository iClinicalDocumentRepository, IDocumentTypeXWalkRepository iDocumentTypeXWalkRepository, IDocumentTypeRepository iDocumentTypeRepository, DbContext dbContext) : base(dbContext)
        {
            _iClinicalDocumentRepository = iClinicalDocumentRepository
                ?? throw new ArgumentNullException(nameof(iClinicalDocumentRepository));

            _iDocumentTypeXWalkRepository = iDocumentTypeXWalkRepository
                ?? throw new ArgumentNullException(nameof(iDocumentTypeXWalkRepository));

            _iDocumentTypeRepository = iDocumentTypeRepository
                ?? throw new ArgumentNullException(nameof(iDocumentTypeRepository));
        }

        #endregion

        #region Method
        /// <summary>
        /// Saves Clicnical details
        /// </summary>
        /// <param name="clinicalDocuments"></param>
        /// <returns></returns>
        public async Task<string> SaveClinicalDetails(Entities.ClinicalDocumentMetadata clinicalDocuments)
        {

            //convert facility code to uppercase
            clinicalDocuments.FacilityCode = clinicalDocuments.FacilityCode.ToUpper();

            //save clinical details
            _iClinicalDocumentRepository.Add(clinicalDocuments);

            //commit changes
            Commit();

            //return id
            return await Task.Run(() => clinicalDocuments.ClinicalDocumentMetadataId.ToString());

        }

        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical documents</returns>
        public async Task<ICollection<Entities.ClinicalDocumentMetadata>> GetClinicalDocumentDetails(string facilityCode, string accountNumber)
        {
            return await _iClinicalDocumentRepository.GetClinicalDocumentDetails(facilityCode, accountNumber);
        }


        /// <summary>
        /// Document Crosswalk Configuration
        /// </summary>
        /// <returns>collection of Document Crosswalk Configuration</returns>
        public async Task<ICollection<DocumentTypeXwalk>> GetDocumentCrosswalkConfiguration()
        {
            return await _iDocumentTypeXWalkRepository.GetDocumentCrosswalkConfiguration();
        }

        /// <summary>
        /// Document Crosswalk
        /// </summary>
        /// <returns>Document Crosswalk collection</returns>
        public async Task<ICollection<DocumentType>> GetDocumentCrosswalkDetails()
        {
            return await _iDocumentTypeRepository.GetDocumentCrosswalkDetails();
        }
        #endregion


    }
}
