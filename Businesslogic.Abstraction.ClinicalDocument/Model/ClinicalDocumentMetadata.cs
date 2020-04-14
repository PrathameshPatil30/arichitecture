
namespace BusinessLogic.Abstraction.ClinicalDocument.Model
{
    /// <summary>
    /// ClinicalDocumentMetadata
    /// </summary>
    public class ClinicalDocumentMetadata
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
