using System;
using System.Collections.Generic;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
{
    /// <summary>
    /// ClinicalDocumentResponse
    /// </summary>
    public class ClinicalDocumentResponse : ValidationResponseBase
    {
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// ClinicalDocumentMetadata
        /// </summary>
        public ICollection<ClinicalDocumentMetadataResponse> ClinicalDocumentMetadata { get; set; }

        /// <summary>
        /// 400 validation messages
        /// </summary>
        public override bool IsValid => ValidationResponse == null;

        public override bool IsException => ValidationResponse == null && !(ClinicalDocumentMetadata.Count > 0);
    }
}
