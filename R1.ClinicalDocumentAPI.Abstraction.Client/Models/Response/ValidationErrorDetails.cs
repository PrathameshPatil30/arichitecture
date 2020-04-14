using System;
using System.Collections.Generic;
using System.Text;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
{
    /// <summary>
    /// Validation messages object for bad request 400
    /// </summary>
    public class ValidationErrorDetails
    {
        /// <summary>
        /// Validation errors 400 messages properties
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// Message for bad request 400 for validatins errors
        /// </summary>
        public string Message { get; set; }
    }
}
