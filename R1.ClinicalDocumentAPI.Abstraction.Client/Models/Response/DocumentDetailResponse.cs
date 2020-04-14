using System;

namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
{
    /// <summary>
    /// Document Detail Response
    /// </summary>
    public class DocumentDetailResponse : ValidationResponseBase
    {
        /// <summary>
        /// Response Message
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// 400 validation messages
        /// </summary>
        public override bool IsValid => !String.IsNullOrWhiteSpace(ResponseMessage) && ValidationResponse == null;

        /// <summary>
        /// 500
        /// </summary>
        public override bool IsException => String.IsNullOrWhiteSpace(ResponseMessage) && ValidationResponse == null;
    }
}
