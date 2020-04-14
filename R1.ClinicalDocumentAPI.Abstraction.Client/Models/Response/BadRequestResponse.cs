namespace R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response
{
    /// <summary>
    /// Bad request response (400) error message for client
    /// </summary>
    public class BadRequestResponse
    {
        /// <summary>
        /// Message for bad request object
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Boolean value indicating if its bad request or valid response
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Bad request validations errors from clinical document api
        /// </summary>
        public ValidationErrorDetails[] Errors { get; set; }
    }
}
