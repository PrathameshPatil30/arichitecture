using System.Diagnostics.CodeAnalysis;

namespace Contract.ClinicalDocument.ErrorResponse
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Validation Error Details
    /// </summary>
    public class ValidationErrorDetails
    {
        /// <summary>
        /// Represents property which throws error
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Represents error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error Details for validations
        /// </summary>
        public ValidationErrorDetails() { }
    }
}
