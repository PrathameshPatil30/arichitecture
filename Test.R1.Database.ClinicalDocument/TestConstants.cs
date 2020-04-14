using System;
using System.Collections.Generic;
using Database.ClinicalDocument.Entities;

namespace Test.R1.Database.ClinicalDocument
{
    public class TestConstants
    {
        public static global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata clinicalDocuments => requestModelValues;

        private static readonly global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata requestModelValues = new global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata
        {
            ClinicalDocumentMetadataId =1,
            AccountNumber = "1234567",
            DocumentName = "OpsReport",
            DocumentType = "JPEG",
            FacilityCode = "SJPR",
            CreatedDateTime = DateTime.Now
        };

        public static string AccountNumberForNullRecord => accountnumberfornullrecord;
        private static readonly string accountnumberfornullrecord = "9878195615";

        public static string FaclityCodeSJPR => facilitycodesjpr;
        private static readonly string facilitycodesjpr = "SJPR";

        public static IList<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> ListOfClinicalDocumentsNull => null;




        public static string AccountNumberForEmptyRecord => accountnumberforemptyrecord;
        private static readonly string accountnumberforemptyrecord = "DT3723943";

        public static string FaclityCodeSJPK => facilitycodesjpk;
        private static readonly string facilitycodesjpk = "SJPK";


        public static IList<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> ListOfClinicalDocumentsEmptyRecords => listOfclinicaldocuments;

        private readonly static IList<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> listOfclinicaldocuments = new List<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>()
        {

        };

        public static string AccountNumberWithRecord => accountnumberwithrecord;
        private static readonly string accountnumberwithrecord = "9029674086456123";

        public static string FaclityCodeBOMC => facilitycodebomc;
        private static readonly string facilitycodebomc = "BOMC";

        public static IList<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> ListOfClinicalDocumentsWithRecords => listOfclinicaldocumentwithrecords;

        private readonly static IList<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata> listOfclinicaldocumentwithrecords = new List<global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>()
        {
            new global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata{ ClinicalDocumentMetadataId=1, AccountNumber="9029674086456123", FacilityCode="BOMC", DocumentName="Operative Report", DocumentType="pdf", CreatedDateTime= DateTime.Now},
            new global::Database.ClinicalDocument.Entities.ClinicalDocumentMetadata{ ClinicalDocumentMetadataId=2, AccountNumber="9029674086456123", FacilityCode="BOMC", DocumentName="Cardiac Catheterization", DocumentType="pdf", CreatedDateTime= DateTime.Now}
        };


        public static IList<DocumentTypeXwalk> ListOfDocumentCrosswalkConfigurationNull => null;

        public static IList<DocumentTypeXwalk> ListOfDocumentCrosswalkConfigurationEmpty => listofdocumentcrosswalkconfigurationempty;

        private readonly static IList<DocumentTypeXwalk> listofdocumentcrosswalkconfigurationempty = new List<DocumentTypeXwalk>()
        {

        };

        public static IList<DocumentTypeXwalk> ListOfDocumentCrosswalkConfigurationWithRecords => listofdocumentcrosswalkconfigurationwithrecords;
        private readonly static IList<DocumentTypeXwalk> listofdocumentcrosswalkconfigurationwithrecords = new List<DocumentTypeXwalk>()
        {
            new DocumentTypeXwalk{DocumentTypeXwalkId=1,DocumentName="OPERATIVE REPORT",DocumentTypeId=1,FacilityCode="BOMC" },
            new DocumentTypeXwalk{DocumentTypeXwalkId=2,DocumentName="Cardiac Catheterization",DocumentTypeId=3,FacilityCode="SJPR" }
        };



        public static IList<DocumentType> ListOfDocumentCrosswalkNull => null;

        public static IList<DocumentType> ListOfDocumentCrosswalkEmpty => listofdocumentcrosswalkempty;

        private readonly static IList<DocumentType> listofdocumentcrosswalkempty = new List<DocumentType>()
        {

        };

        public static IList<DocumentType> ListOfDocumentCrosswalkWithRecords => listofdocumentcrosswalkwithrecords;
        private readonly static IList<DocumentType> listofdocumentcrosswalkwithrecords = new List<DocumentType>()
        {
            new DocumentType{DocumentTypeId=1,StandardDocumentName="Operation Report",Description="This is an Operation Report",IsActive=true,DeActivatedDateTime=null},
            new DocumentType{DocumentTypeId=1,StandardDocumentName="Catherization Lab Report",Description="Catherization Lab Report",IsActive=true,DeActivatedDateTime=null}
        };

    }
}
