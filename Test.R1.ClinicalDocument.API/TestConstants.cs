using BusinessLogic.Abstraction.ClinicalDocument.Model;
using Contract.CliniclaDocument.Request;
using Contract.CliniclaDocument.Response;

namespace Test.R1.ClinicalDocument.API
{
    public class TestConstants
    {
        public static DocumentDetailRequest DocRequest => documentDetailRequest;

        private static readonly DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest
        {
            AccountNumber = "1234567",
            DocumentName = "OpsReport",
            DocumentType = "JPEG",
            FacilityCode = "SJPR"
        };

        public static ClinicalDocumentInputModel ClinicalDocumentInputModel => requestModelValues;

        private static readonly ClinicalDocumentInputModel requestModelValues = new ClinicalDocumentInputModel
        {
            AccountNumber = "1234567",
            DocumentName = "OpsReport",
            DocumentType = "JPEG",
            FacilityCode = "SJPR"
        };

        public static DocumentDetailResponse DocResponse => responseValues;

        private static readonly DocumentDetailResponse responseValues = new DocumentDetailResponse
        {
            ResponseMessage = "Record : 465A6717 - 9956 - 4DC3 - 8C94 - FE9B09CB2B84 saved successfully."
        };

        public static ClinicalDocumentOutputModel ClinicalDocumentOutputModel => businessResponseValues;

        private static readonly ClinicalDocumentOutputModel businessResponseValues = new ClinicalDocumentOutputModel
        {
            ResponseMessage = "Record : 465A6717 - 9956 - 4DC3 - 8C94 - FE9B09CB2B84 saved successfully."
        };


        public static ClinicalDocumentOutputModel ClinicalDocumentOutputModelNull => returnNull;

        private static readonly ClinicalDocumentOutputModel returnNull = null;
    }
}
