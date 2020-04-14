namespace Services.Contract.Model
{
    /// <summary>
    /// This class is to pass document and metadata to IBM cloud
    /// </summary>
    public class DocumentMetadata
    {
        /// <summary>
        /// Document
        /// </summary>
        public byte[] Document { get; set; }

        /// <summary>
        /// SourceSystem
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// ContentType
        /// </summary>
        public string MimeType { get; set; }
    }
}
