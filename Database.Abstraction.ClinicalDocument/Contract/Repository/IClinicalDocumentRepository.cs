using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Abstraction.ClinicalDocument.Common;
using Database.ClinicalDocument.Entities;

namespace Database.Abstraction.ClinicalDocument.Contract.Repository
{
    /// <summary>
    /// Clinical Document Repository Interface
    /// </summary>
    public interface IClinicalDocumentRepository : IRepository<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>
    {
        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical documents</returns>
        Task<ICollection<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>> GetClinicalDocumentDetails(string facilityCode, string accountNumber);
    }
}
