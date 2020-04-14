using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using R1.DocumentService.Clients.Rest;
using R1.DocumentService.Contracts.Clients;
using R1.DocumentService.Contracts.Clients.Models;
using Services.Contract;
using Services.Contract.Model;

namespace Services.DocumentService
{
    public class DocumentServiceHelper : IDocumentServiceHelper
    {
        #region Properties
        /// <summary>
        /// private property for injecting RestTransferUtilityOptions dependency
        /// </summary>
        private readonly IOptions<RestTransferUtilityOptions> _restTransferUtilityOptions;
        #endregion

        #region Constructor
        /// <summary>
        /// Construcrtor to initialise restTransferUtilityOptions dependency
        /// </summary>
        /// <param name="restTransferUtilityOptions"></param>
        public DocumentServiceHelper(IOptions<RestTransferUtilityOptions> restTransferUtilityOptions)
        {
            _restTransferUtilityOptions = restTransferUtilityOptions;
        }
        #endregion

        #region Methods
        /// <summary>
        /// UplaodDocuments method to save document in IBM cloud
        /// </summary>
        /// <param name="documentMetadata"></param>
        /// <returns></returns>
        public async Task<(string status, Guid key)> UploadDocuments(DocumentMetadata documentMetadata)
        {
            string status = string.Empty;
            Guid key = Guid.Empty;
            using (var transferUtil = new RestTransferUtility(_restTransferUtilityOptions))
            {
                var request = new UploadDocumentRequest
                {
                    SourceSystem = documentMetadata.SourceSystem,
                    Filename = documentMetadata.DocumentName,
                    Name = documentMetadata.DocumentName,
                    ContentType = documentMetadata.MimeType
                };
                var response = await transferUtil.UploadAsync(request, new MemoryStream(documentMetadata.Document), new CancellationToken());

                if (response.Errors != null)
                {
                    status = response.Errors.Flatten().ToString();
                }
                else
                {
                    status = response.Status.ToString();
                    key = Guid.Parse(response.Key);
                }
                return (status, key);
            }
        }
        #endregion

    }
}
