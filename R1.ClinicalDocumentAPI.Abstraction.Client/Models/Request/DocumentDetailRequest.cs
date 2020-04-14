using System;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Request
{
    /// <summary>
    /// Clinical Document API Request will accept the below mentioned parameters while saving the metadata of a particular visit
    /// This is same as that for clinical document POST endpoint
    /// </summary>
    public class DocumentDetailRequest
    {
        /// <summary>
        /// Facility Code
        /// </summary>
        public string FacilityCode { get; set; }

        /// <summary>
        /// Account Number
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Document Name
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Flag to check actual document received or not 
        /// </summary>
        public bool? DocumentReceived { get; set; }

        /// <summary>
        /// document Id received from document service
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// MIME type of the document
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Bucket name for IBM cloud
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// Actual document in byte array
        /// </summary>
        public byte[] Document { get; set; }
    }
}
