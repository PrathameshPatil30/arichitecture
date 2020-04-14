using System;
using System.Threading.Tasks;
using Services.Contract.Model;

namespace Services.Contract
{
    public interface IDocumentServiceHelper
    {
        /// <summary>
        /// UplaodDocuments method to save document in IBM cloud
        /// </summary>
        /// <param name="documentMetadata"></param>
        /// <returns></returns>
        Task<(string status, Guid key)> UploadDocuments(DocumentMetadata documentMetadata);
    }
}
