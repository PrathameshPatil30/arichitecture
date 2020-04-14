using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Database.Abstraction.ClinicalDocument.Contract.Repository;
using Database.ClinicalDocument.Common;
using Database.ClinicalDocument.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.ClinicalDocument.DataAccess.Repository
{
    /// <summary>
    /// Clinical Document Repository
    /// </summary>
    public class ClinicalDocumentRepository : RepositoryBase<Entities.ClinicalDocumentMetadata>, IClinicalDocumentRepository
    {
        /// <summary>
        /// Constructor Clinical Document Repository
        /// </summary>
        /// <param name="dbContext"></param>
        public ClinicalDocumentRepository(DbContext dbContext) : base(dbContext)
        {

        }


        /// <summary>
        /// Fetch clinical document metadata from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical document metadata</returns>
        public async Task<ICollection<Entities.ClinicalDocumentMetadata>> GetClinicalDocumentDetails([Required]string facilityCode, string accountNumber)
        {
            return await Task.Run(() =>
            {

                return Find(x => x.AccountNumber == accountNumber && x.FacilityCode == facilityCode).ToList();
            });
        }

    }
}
