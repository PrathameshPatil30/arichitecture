using System.Threading.Tasks;
using BusinessLogic.Abstraction.ClinicalDocument.Model;

namespace BusinessLogic.Abstraction.ClinicalDocument
{
    /// <summary>
    /// ClinicalDocumentBusinessLogic Interface
    /// </summary>
    public interface IClinicalDocumentBusinessLogic
    {
        /// <summary>
        /// SaveClinicaldetails
        /// </summary>
        /// <param name="clinicalDocumentInputModel">clinicalDocumentInputModel</param>
        /// <returns>Clinical Document </returns>
        Task<ClinicalDocumentOutputModel> SaveClinicalDetails(ClinicalDocumentInputModel clinicalDocumentInputModel);

        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical documents</returns>
        Task<ClinicalDocumentDetails> GetClinicalDocumentDetails(string facilityCode, string accountNumber);
    }
}
