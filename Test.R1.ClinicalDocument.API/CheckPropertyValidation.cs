using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.R1.ClinicalDocument.API
{
    public class CheckPropertyValidation
    {
        /// <summary>
        /// ValidateModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
