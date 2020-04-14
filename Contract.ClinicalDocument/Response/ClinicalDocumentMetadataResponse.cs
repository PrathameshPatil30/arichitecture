namespace Contract.ClinicalDocument.Response
{
    /// <summary>
    /// ClinicalDocumentMetadataResponse
    /// </summary>
    public class ClinicalDocumentMetadataResponse
    {
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
        /// CreatedDateTime
        /// </summary>
        public string CreatedDateTime { get; set; }
    }
}
