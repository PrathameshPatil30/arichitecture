using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Abstraction.ClinicalDocument.Common;
using Database.ClinicalDocument.Entities;

namespace Database.Abstraction.ClinicalDocument.Contract.Repository
{

    /// <summary>
    /// Document Type XWalk Repository Interface
    /// </summary>
    public interface IDocumentTypeXWalkRepository : IRepository<DocumentTypeXwalk>
    {

        /// <summary>
        /// Document Type XWalk
        /// </summary>
        /// <returns>Document Type XWalk collection</returns>
        Task<ICollection<DocumentTypeXwalk>> GetDocumentCrosswalkConfiguration();
    }
}
