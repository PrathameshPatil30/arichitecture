using System;
using System.Collections.Generic;
using BusinessLogic.Abstraction.ClinicalDocument.Model;
using Database.ClinicalDocument.Entities;
using businessLogic = BusinessLogic.Abstraction.ClinicalDocument.Model;

namespace Test.R1.BusinessLogic.ClinicalDocument
{
    public static class TestConstants
    {
        public static string Status = "Completed";
        public static Guid Key = Guid.Parse("B418F32B-8B76-4995-B51A-4A27920C5E6A");

        public static string StatusInvlaid = "Error";
        public static Guid KeyInvalid = Guid.Empty;

        public static byte[] document = Convert.FromBase64String("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");

        public static Services.Contract.Model.DocumentMetadata DocumentMetadata => documentMetadata;

        private static readonly Services.Contract.Model.DocumentMetadata documentMetadata = new Services.Contract.Model.DocumentMetadata
        {
            DocumentName = "OpsReport",
            MimeType = "image/jpeg",
            SourceSystem = "GCR",
            Document = document
        };

        public static Services.Contract.Model.DocumentMetadata DocumentMetadataInvalid => documentMetadataInvalid;

        private static readonly Services.Contract.Model.DocumentMetadata documentMetadataInvalid = new Services.Contract.Model.DocumentMetadata
        {
            DocumentName = "OpsReport",
            MimeType = "image/jpeg",
            SourceSystem = "XYZ",
            Document = document
        };

        public static ClinicalDocumentInputModel ClinicalDocumentInputModel
        {
            get { return clinicalDocumentInputModel; }
        }

        readonly static ClinicalDocumentInputModel clinicalDocumentInputModel = new ClinicalDocumentInputModel
        {
            AccountNumber = "923998929",
            DocumentName = "OpsReport",
            DocumentType = "pdf",
            FacilityCode = "BOMC",
            DocumentReceived = true,
            MimeType = "image/jpeg",
            SourceSystem = "GCR",
            Document = document
        };

        public static ClinicalDocumentInputModel ClinicalDocumentInputModelInvalid
        {
            get { return clinicalDocumentInputModelInvalid; }
        }

        readonly static ClinicalDocumentInputModel clinicalDocumentInputModelInvalid = new ClinicalDocumentInputModel
        {
            AccountNumber = "923998929",
            DocumentName = "OpsReport",
            DocumentType = "pdf",
            FacilityCode = "BOMC",
            DocumentReceived = true,
            MimeType = "image/jpeg",
            SourceSystem = "XYZ",
            Document = document
        };

        public static Database.ClinicalDocument.Entities.ClinicalDocumentMetadata ClinicalDocumentEntity
        {
            get { return clinicalDocumentEntity; }
        }

        readonly static Database.ClinicalDocument.Entities.ClinicalDocumentMetadata clinicalDocumentEntity = new Database.ClinicalDocument.Entities.ClinicalDocumentMetadata
        {
            AccountNumber = "923998929",
            DocumentName = "OpsReport",
            DocumentType = "pdf",
            FacilityCode = "BOMC",
            IsDocumentReceived = true,
            MimeType = "image/jpeg",
            SourceSystem = "GCR",
            DocumentId = null
        };

        public static Database.ClinicalDocument.Entities.ClinicalDocumentMetadata ClinicalDocumentEntityInvalid
        {
            get { return clinicalDocumentEntityInvalid; }
        }

        readonly static Database.ClinicalDocument.Entities.ClinicalDocumentMetadata clinicalDocumentEntityInvalid = new Database.ClinicalDocument.Entities.ClinicalDocumentMetadata
        {
            AccountNumber = "923998929",
            DocumentName = "OpsReport",
            DocumentType = "pdf",
            FacilityCode = "BOMC",
            IsDocumentReceived = true,
            MimeType = "image/jpeg",
            SourceSystem = "XYZ",
            DocumentId = null
        };

        public static ClinicalDocumentOutputModel ClinicalDocumentOutputModel
        {
            get { return clinicalDocumentOutputModel; }
        }

        readonly static ClinicalDocumentOutputModel clinicalDocumentOutputModel = new ClinicalDocumentOutputModel
        {
            ResponseMessage = "Record : a4a0d3a2-779a-4d59-8a26-dd566b7195f1 saved successfully."
        };

        const string clinicalDocumentId = "a4a0d3a2-779a-4d59-8a26-dd566b7195f1";

        public static string ClinicalDocumentId
        {
            get { return clinicalDocumentId; }
        }

        public static Database.ClinicalDocument.Entities.ClinicalDocumentMetadata ClinicalDocumentEntityEmpty
        {
            get { return clinicalDocumentEntityEmpty; }
        }

        const Database.ClinicalDocument.Entities.ClinicalDocumentMetadata clinicalDocumentEntityEmpty = null;


        // Data for null clinical documents,metadata //
        public static string AccountNumberForNullRecord => accountnumberfornullrecord;
        private static readonly string accountnumberfornullrecord = "9878195615";

        public static string FaclityCodeSJPR => facilitycodesjpr;
        private static readonly string facilitycodesjpr = "SJPR";

        public static IList<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> ListOfClinicalDocumentsNull => null;


        public static IList<businessLogic.ClinicalDocumentMetadata> ListOfClinicalDocumentsMetadataNull => null;

        // Data for empty clinical documents,metadata //
        public static string AccountNumberForEmptyRecord => accountnumberforemptyrecord;
        private static readonly string accountnumberforemptyrecord = "DT3723943";

        public static string FaclityCodeSJPK => facilitycodesjpk;
        private static readonly string facilitycodesjpk = "sjpk";


        public static ICollection<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> ListOfClinicalDocumentsEmptyRecords => listOfclinicaldocument;

        private readonly static IList<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> listOfclinicaldocument = new List<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>()
        {

        };

        public static ICollection<businessLogic.ClinicalDocumentMetadata> ListOfClinicalDocumentsMetadataEmptyRecords => listOfclinicaldocumentmetadata;

        private readonly static IList<businessLogic.ClinicalDocumentMetadata> listOfclinicaldocumentmetadata = new List<businessLogic.ClinicalDocumentMetadata>()
        {

        };


        // Data for list of records for clinical documents,metadata //
        public static string AccountNumberWithRecord => accountnumberwithrecord;
        private static readonly string accountnumberwithrecord = "9029674086456123";

        public static string FaclityCodeBOMC => facilitycodebomc;
        private static readonly string facilitycodebomc = "BOMC";

        public static ICollection<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> ListOfClinicalDocumentsWithRecords => listOfclinicaldocumentwithrecords;

        private readonly static List<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> listOfclinicaldocumentwithrecords = new List<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>()
        {
            new Database.ClinicalDocument.Entities.ClinicalDocumentMetadata{ ClinicalDocumentMetadataId=1, AccountNumber="9029674086456123", FacilityCode="BOMC", DocumentName="Operative Report", DocumentType="pdf", CreatedDateTime= DateTime.Now},
            new Database.ClinicalDocument.Entities.ClinicalDocumentMetadata{ ClinicalDocumentMetadataId=2, AccountNumber="9029674086456123", FacilityCode="BOMC", DocumentName="Cardiac Catheterization", DocumentType="pdf", CreatedDateTime= DateTime.Now},
            new Database.ClinicalDocument.Entities.ClinicalDocumentMetadata{ ClinicalDocumentMetadataId=3, AccountNumber="9029674086456123", FacilityCode="BOMC", DocumentName="Ops Report", DocumentType="pdf", CreatedDateTime= DateTime.Now}
        };


        public static ICollection<businessLogic.ClinicalDocumentMetadata> ListOfClinicalDocumentsMetadataWithRecords => listOfclinicaldocumentmetadatawithrecords;

        private readonly static List<businessLogic.ClinicalDocumentMetadata> listOfclinicaldocumentmetadatawithrecords = new List<businessLogic.ClinicalDocumentMetadata>()
        {
            new businessLogic.ClinicalDocumentMetadata{AccountNumber="9029674086456123",FacilityCode="BOMC",DocumentName="Operative Report",DocumentType="pdf",CreatedDateTime=DateTime.Now.ToString()},
            new businessLogic.ClinicalDocumentMetadata{AccountNumber="9029674086456123",FacilityCode="BOMC",DocumentName="Cardiac Catheterization",DocumentType="pdf",CreatedDateTime=DateTime.Now.ToString()},
            new businessLogic.ClinicalDocumentMetadata{AccountNumber="9029674086456123",FacilityCode="BOMC",DocumentName="Ops Report",DocumentType="pdf",CreatedDateTime=DateTime.Now.ToString()}
        };


        // Data for clinical document detail send in response to get endpoint with no records, with recrods found 
        public static ClinicalDocumentDetails ClinicalDocumentDetailsWithNoRecords => clinicaldocumentdetails;

        private readonly static ClinicalDocumentDetails clinicaldocumentdetails = new ClinicalDocumentDetails()
        {
            Status = "No records found.",
            ClinicalDocumentMetadata = new List<businessLogic.ClinicalDocumentMetadata>()
            {
            }
        };

        public static ClinicalDocumentDetails ClinicalDocumentDetailsWithRecords => clinicaldocumentdetailswithrecords;

        private readonly static ClinicalDocumentDetails clinicaldocumentdetailswithrecords =
            new ClinicalDocumentDetails
            {
                Status = "Records found.",
                ClinicalDocumentMetadata = new List<businessLogic.ClinicalDocumentMetadata>()
            {
                new businessLogic.ClinicalDocumentMetadata{AccountNumber="9029674086456123",DocumentName="Operation Report",DocumentType="pdf",FacilityCode="BOMC",CreatedDateTime=DateTime.Now.ToString()},
                new businessLogic.ClinicalDocumentMetadata{AccountNumber="9029674086456123",DocumentName="Catherization Lab Report",DocumentType="pdf",FacilityCode="BOMC",CreatedDateTime=DateTime.Now.ToString()},
                new businessLogic.ClinicalDocumentMetadata{AccountNumber="9029674086456123",DocumentName="Ops Report",DocumentType="pdf",FacilityCode="BOMC",CreatedDateTime=DateTime.Now.ToString()}
            }
            };

        // Data for Document crosswalk configuration and document crosswalk- master records
        public static ICollection<DocumentTypeXwalk> ListOfDocumentCrosswalkConfigurationWithRecords => listofdocumentcrosswalkconfigurationwithrecords;
        private readonly static IList<DocumentTypeXwalk> listofdocumentcrosswalkconfigurationwithrecords = new List<DocumentTypeXwalk>()
        {
            new DocumentTypeXwalk{DocumentTypeXwalkId=1,DocumentName="OPERATIVE REPORT",DocumentTypeId=1,FacilityCode="BOMC" },
            new DocumentTypeXwalk{DocumentTypeXwalkId=1,DocumentName="Cardiac Catheterization",DocumentTypeId=3,FacilityCode="BOMC" }
        };

        public static ICollection<DocumentType> ListOfDocumentCrosswalkWithRecords => listofdocumentcrosswalkwithrecords;
        private readonly static IList<DocumentType> listofdocumentcrosswalkwithrecords = new List<DocumentType>()
        {
            new DocumentType{DocumentTypeId=1,StandardDocumentName="Operation Report",Description="This is an Operation Report",IsActive=true,DeActivatedDateTime=null},
            new DocumentType{DocumentTypeId=1,StandardDocumentName="Catherization Lab Report",Description="Catherization Lab Report",IsActive=true,DeActivatedDateTime=null}
        };


        //Data for empty crosswalk configuration and empty crosswalk

        public static ICollection<DocumentTypeXwalk> ListOfDocumentCrosswalkConfigurationEmpty => listofdocumentcrosswalkconfigurationempty;

        private readonly static IList<DocumentTypeXwalk> listofdocumentcrosswalkconfigurationempty = new List<DocumentTypeXwalk>()
        {

        };

        public static ICollection<DocumentType> ListOfDocumentCrosswalkEmpty => listofdocumentcrosswalkempty;

        private readonly static IList<DocumentType> listofdocumentcrosswalkempty = new List<DocumentType>()
        {

        };


    }
}
