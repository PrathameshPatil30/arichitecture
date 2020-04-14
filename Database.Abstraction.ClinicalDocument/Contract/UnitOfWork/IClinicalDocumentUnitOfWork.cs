using System.Collections.Generic;
using System.Threading.Tasks;
using Database.ClinicalDocument.Entities;

namespace Database.Abstraction.ClinicalDocument.Contract.UnitOfWork
{
    /// <summary>
    /// Clinical Document Unit Of Work Interface
    /// </summary>
    public interface IClinicalDocumentUnitOfWork
    {
        /// <summary>
        /// Save Clinical Document Details
        /// </summary>
        /// <param name="clinicalDocuments">clinicalDocuments</param>
        /// <returns></returns>
        Task<string> SaveClinicalDetails(Database.ClinicalDocument.Entities.ClinicalDocumentMetadata clinicalDocuments);

        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical document metadata</returns>
        Task<ICollection<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>> GetClinicalDocumentDetails(string facilityCode, string accountNumber);

        /// <summary>
        /// Document Type XWalk Configuration
        /// </summary>
        /// <returns>collection of Document Type XWalk Configuration</returns>
        Task<ICollection<DocumentTypeXwalk>> GetDocumentCrosswalkConfiguration();

        /// <summary>
        /// Document Type
        /// </summary>
        /// <returns>Document Type collection</returns>
        Task<ICollection<DocumentType>> GetDocumentCrosswalkDetails();
    }
}
