using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Abstraction.ClinicalDocument.Contract.Repository;
using Database.ClinicalDocument.Common;
using Database.ClinicalDocument.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.ClinicalDocument.DataAccess.Repository
{

    /// <summary>
    /// Document Crosswalk Repository
    /// </summary>
    public class DocumentTypeRepository : RepositoryBase<DocumentType>, IDocumentTypeRepository
    {


        /// <summary>
        /// DocumentTypeRepository
        /// </summary>
        /// <param name="dbContext">dbContext</param>
        public DocumentTypeRepository(DbContext dbContext) : base(dbContext)
        {

        }


        /// <summary>
        /// Document Type
        /// </summary>
        /// <returns>collection of Document Type </returns>
        public async Task<ICollection<DocumentType>> GetDocumentCrosswalkDetails()
        {
            return await Task.Run(() =>
            {
                return GetAll().ToList();
            });
        }

    }
}
