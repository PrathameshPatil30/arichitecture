using System;
using System.Collections.Generic;

namespace Database.ClinicalDocument.Entities
{
    public partial class DocumentType
    {
        /// <summary>
        /// initialization of document type
        /// </summary>
        public DocumentType()
        {
            DocumentTypeXwalk = new HashSet<DocumentTypeXwalk>();
        }

        /// <summary>
        /// DocumentTypeId
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// StandardDocumentName
        /// </summary>
        public string StandardDocumentName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// DeActivatedDateTime
        /// </summary>
        public DateTime? DeActivatedDateTime { get; set; }

        /// <summary>
        /// collection of DocumentTypeXwalk
        /// </summary>

        public virtual ICollection<DocumentTypeXwalk> DocumentTypeXwalk { get; set; }
    }
}
