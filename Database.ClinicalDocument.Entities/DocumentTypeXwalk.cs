using System;
using System.Collections.Generic;

namespace Database.ClinicalDocument.Entities
{
    public partial class DocumentTypeXwalk
    {

        /// <summary>
        /// DocumentTypeXwalkId
        /// </summary>
        public int DocumentTypeXwalkId { get; set; }

        /// <summary>
        /// FacilityCode
        /// </summary>
        public string FacilityCode { get; set; }

        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// DocumentTypeId
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// DeActivatedDateTime
        /// </summary>
        public DateTime? DeActivatedDateTime { get; set; }

        /// <summary>
        /// DocumentType
        /// </summary>
        public virtual DocumentType DocumentType { get; set; }
    }
}
