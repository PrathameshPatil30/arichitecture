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
    /// Document Type XWalk Repository
    /// </summary>
    public class DocumentTypeXwalkRepository : RepositoryBase<DocumentTypeXwalk>, IDocumentTypeXWalkRepository
    {


        /// <summary>
        /// DocumentTypeXWalk Repository
        /// </summary>
        /// <param name="dbContext">dbContext</param>
        public DocumentTypeXwalkRepository(DbContext dbContext) : base(dbContext)
        {

        }


        /// <summary>
        /// Document Type XWalk 
        /// </summary>
        /// <returns>collection of Document Type XWalk Configuration</returns>
        public async Task<ICollection<DocumentTypeXwalk>> GetDocumentCrosswalkConfiguration()
        {
            return await Task.Run(() =>
            {
                return GetAll().ToList();
            });
        }

    }
}
