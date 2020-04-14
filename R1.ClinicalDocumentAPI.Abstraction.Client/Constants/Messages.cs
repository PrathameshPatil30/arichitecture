namespace R1.ClinicalDocumentAPI.Abstraction.Client.Constants
{
    /// <summary>
    /// Constant messages for client
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// exception message for save clinical documents
        /// </summary>
        public const string SaveExceptionMessage = "Exception while saving clinical documents from Clinical Document Rest client";
        /// <summary>
        /// exception message for get clinical documents 
        /// </summary>
        public const string GetExceptionMessage = "Exception while fetching clinical documents from Clinical Document Rest client";

        /// <summary>
        /// Generic Exception message
        /// </summary>
        public const string ExceptionMessage = "Some error occured ! Please contact administrator.";
    }
}
