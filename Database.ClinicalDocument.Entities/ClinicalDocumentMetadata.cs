using System;
using System.Collections.Generic;

namespace Database.ClinicalDocument.Entities
{
    public partial class ClinicalDocumentMetadata
    {
        /// <summary>
        /// ClinicalDocumentMetadataId
        /// </summary>
        public int ClinicalDocumentMetadataId { get; set; }

        /// <summary>
        /// AccountNumber
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// FacilityCode
        /// </summary>
        public string FacilityCode { get; set; }

        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// DocumentType
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// IsDocumentReceived
        /// </summary>
        public bool IsDocumentReceived { get; set; }


        /// <summary>
        /// DocumentId
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// SourceSystem
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// MimeType
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// DocumentStatus
        /// </summary>
        public string DocumentStatus { get; set; }

        /// <summary>
        /// CreatedDateTime
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
    }
}
