using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Resources;

namespace Contract.CliniclaDocument.Request
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Clinical Document API Request will accept the below mentioned parameters while saving the metadata of a particular Visit
    /// </summary>
    public class DocumentDetailRequest
    {
        /// <summary>
        /// Facility Code
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "FacilityCode_Required")]
        [MaxLength(4, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "FacilityCode_Length"),
         MinLength(4, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "FacilityCode_Length")]
        [RegularExpression(@"^[a-zA-Z][^<>.,?;:'()!~%\-_@#/*""\s]+$", ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "FacilityCode_Pattern")]
        public string FacilityCode { get; set; }

        /// <summary>
        /// Account Number
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "AccountNumber_Required")]
        [MaxLength(20, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "AccountNumber_Length")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$", ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "AccountNumber_Pattern")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "DocumentType_Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "DocumenType_Length")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$", ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "DocumentType_Pattern")]
        public string DocumentType { get; set; }

        /// <summary>
        /// Document Name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "DocumentName_Required")]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "DocumentName_Length")]
        public string DocumentName { get; set; }

        /// <summary>
        /// MIME type of the document
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "MimeType_Length")]
        public string MimeType { get; set; }

        /// <summary>
        /// Bucket name for IBM cloud
        /// </summary>
        [MaxLength(20, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "SourceSystem_Length")]
        public string SourceSystem { get; set; }

        /// <summary>
        /// Actual document in byte array
        /// </summary>
        [MaxLength(16777216, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "DocumentImage_Size")]
        public byte[] Document { get; set; }

    }
}
