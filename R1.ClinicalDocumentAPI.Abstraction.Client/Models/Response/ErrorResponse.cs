using System;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
{
    /// <summary>
    /// 500 internal server error exceptions object will be handled in this error response object
    /// </summary>
    public class ErrorResponse
    {
        public virtual bool IsException => !String.IsNullOrWhiteSpace(ExceptionMessage);
        public string ExceptionMessage { get; set; }
    }
}
