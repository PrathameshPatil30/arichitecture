using System.Collections.Generic;

namespace Contract.ClinicalDocument.Response
{
    /// <summary>
    /// ClinicalDocumentResponse
    /// </summary>
    public class ClinicalDocumentResponse
    {
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// ClinicalDocumentMetadata
        /// </summary>
        public ICollection<ClinicalDocumentMetadataResponse> ClinicalDocumentMetadata { get; set; }
    }
}
