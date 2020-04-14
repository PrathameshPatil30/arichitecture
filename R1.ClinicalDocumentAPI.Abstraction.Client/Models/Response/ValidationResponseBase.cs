using System;
using System.Collections.Generic;
using System.Text;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
{
    /// <summary>
    /// Base class for 400 - bad request response
    /// </summary>
    public class ValidationResponseBase : ErrorResponse
    {
        /// <summary>
        /// Boolen value indicating if its a bad request or valid response
        /// </summary>
        public virtual bool IsValid => ValidationResponse == null;

        /// <summary>
        /// Represents a BadRequest (HTTP 400) response
        /// </summary>
        public BadRequestResponse ValidationResponse { get; set; }
    }
}
