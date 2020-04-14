using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Contract.ClinicalDocument.ErrorResponse
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Validation Result Model
    /// </summary>
    public class ValidationResultModel
    {
        /// <summary>
        /// Represents error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represents IsValid return type
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Represents list of validation errors to be showcased if validation fails
        /// </summary>
        public ICollection<ValidationErrorDetails> Errors { get; set; }

        /// <summary>
        /// Constructor initiates the model state to bring up the error message and property for which validation fails
        /// </summary>
        public ValidationResultModel() { }
    }
}
