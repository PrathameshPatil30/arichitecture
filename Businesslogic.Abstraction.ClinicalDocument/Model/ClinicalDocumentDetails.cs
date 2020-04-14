
using System.Collections.Generic;

namespace BusinessLogic.Abstraction.ClinicalDocument.Model
{
    /// <summary>
    /// ClinicalDocumentDetails
    /// </summary>
    public class ClinicalDocumentDetails
    {
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// ClinicalDocumentMetadata
        /// </summary>
        public ICollection<ClinicalDocumentMetadata> ClinicalDocumentMetadata { get; set; }
    }
}
