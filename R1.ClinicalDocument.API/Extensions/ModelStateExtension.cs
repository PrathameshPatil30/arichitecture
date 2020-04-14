using System.Collections.Generic;
using System.Linq;
using Contract.ClinicalDocument.ErrorResponse;

namespace R1.ClinicalDocument.API.Extensions
{
    /// <summary>
    /// ModelStateExtension
    /// </summary>
    public static class ModelStateExtension
    {
        /// <summary>
        /// The method would validate the resultant model 
        /// </summary>
        /// <param name="modelState">The model state</param>
        /// <param name="errorMessage">The error message to be thrown if validation fails</param>
        public static ValidationResultModel ToValidationResultModel(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, string errorMessage = "Validation Failed")
        {
            if (modelState != null)
            {
                if (modelState.IsValid)
                {
                    return new ValidationResultModel { IsValid = true };
                }

                var result = new ValidationResultModel
                {
                    Message = errorMessage,
                    Errors = modelState.Keys
                        .Where(key => modelState[key].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                        .SelectMany(key => modelState[key].ToValidationResults(key))
                        .ToList()
                };

                if (!result.Errors.Any())
                {
                    result.IsValid = true;
                }

                return result;
            }
            return null;
        }

        /// <summary>
        /// The method would iterate through the errors and process it in the required format 
        /// </summary>
        /// <param name="modelStateEntry">The model state entry</param>
        /// <param name="key">The key depicting on whichkey the error is thrown</param>
        private static IEnumerable<ValidationErrorDetails> ToValidationResults(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry modelStateEntry, string key)
        {
            return modelStateEntry.Errors.Select(err => new ValidationErrorDetails
            {
                Message = err.ErrorMessage,
                Property = key
            }).ToArray();
        }

        /// <summary>
        /// The method would clear all the model errors
        /// </summary>
        /// <param name="modelState">The model state</param>
        /// <param name="keyName">The key name</param>
        /// <param name="exactMatch">The exact match of the error</param>
        public static void ClearModelError(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, string keyName, bool exactMatch = false)
        {
            var modelKey = modelState.FirstOrDefault(m => exactMatch
                ? string.Compare(m.Key, keyName, true) == 0 : m.Key.Contains(keyName));

            if (!string.IsNullOrEmpty(modelKey.Key))
            {
                modelKey.Value.Errors.Clear();
            }
        }

        /// <summary>
        /// The method would iterate through the errors
        /// </summary>
        /// <param name="modelState">The model state</param>
        /// <param name="prefix">The prefix to be used</param>
        public static void ClearModelErrors(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, string prefix)
        {
            foreach (var modelKey in modelState.Where(m => m.Key.StartsWith(prefix)))
            {
                modelKey.Value.Errors.Clear();
            }
        }
    }
}
