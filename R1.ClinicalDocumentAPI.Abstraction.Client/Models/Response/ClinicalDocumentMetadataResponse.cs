using System;
using System.Collections.Generic;
using System.Text;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
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
        /// CreatedDate
        /// </summary>
        public string CreatedDate { get; set; }
    }
}
