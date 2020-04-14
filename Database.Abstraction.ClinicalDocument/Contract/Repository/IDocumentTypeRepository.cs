using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Abstraction.ClinicalDocument.Common;
using Database.ClinicalDocument.Entities;

namespace Database.Abstraction.ClinicalDocument.Contract.Repository
{


    /// <summary>
    /// Document Type Repository Interface
    /// </summary>
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {

        /// <summary>
        /// Document Type 
        /// </summary>
        /// <returns>Document Type collection</returns>
        Task<ICollection<DocumentType>> GetDocumentCrosswalkDetails();
    }
}
