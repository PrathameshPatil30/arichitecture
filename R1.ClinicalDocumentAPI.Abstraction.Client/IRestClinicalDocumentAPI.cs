using System.Net.Http;
using System.Threading.Tasks;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Request;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response;

namespace R1.ClinicalDocumentAPI.Abstraction.Client
{
    /// <summary>
    /// Clinical Document HTTP client interface
    /// </summary>
    public interface IRestClinicalDocumentAPI
    {
        /// <summary>
        /// Save clinical documents to database & IBM storage via doc service
        /// </summary>
        /// <param name="documentDetailRequest">data from HL7 feed to be saved to clinical document database</param>
        /// <returns></returns>
        Task<DocumentDetailResponse> SaveClinicalDocumentDetails(DocumentDetailRequest documentDetailRequest);

        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode">facility code</param>
        /// <param name="accountNumber">account number or visit number</param>
        /// <returns>Collection of clinical documents</returns>
        Task<ClinicalDocumentResponse> GetClinicalDocumentDetails(string facilityCode, string accountNumber);
    }
}
