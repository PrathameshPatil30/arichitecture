using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Abstraction.ClinicalDocument.Model;
using BusinessLogic.ClinicalDocument;
using Database.Abstraction.ClinicalDocument.Contract.UnitOfWork;
using entities=Database.ClinicalDocument.Entities;
using Microsoft.Extensions.Localization;
using Moq;
using Resources;
using Services.Contract;
using Services.Contract.Model;
using Xunit;

namespace Test.R1.BusinessLogic.ClinicalDocument
{

    /// <summary>
    ///  ClinicalDocumentBusinessLogicShould
    /// </summary>    
    public class ClinicalDocumentBusinessLogicShould
    {


        [Fact]
        public void Exception()
        {
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            mockClinicalDocumentUnitOfWork.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentEntity)).ReturnsAsync(TestConstants.ClinicalDocumentId);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<entities.ClinicalDocumentMetadata>(TestConstants.ClinicalDocumentInputModel)).Returns(TestConstants.ClinicalDocumentEntity);


            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentBusinessLogic(null, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object));

            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, null, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object));

            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, null, mockDocumentServiceHelper.Object));

            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, null));
        }

        /// <summary>
        /// Save ClinicalDetails NotNull
        /// </summary>
        [Fact]
        public async void SaveClinicalDetailsNotNull()
        {
            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            mockClinicalDocumentUnitOfWork.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentEntity)).ReturnsAsync(TestConstants.ClinicalDocumentId);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<entities.ClinicalDocumentMetadata>(TestConstants.ClinicalDocumentInputModel)).Returns(TestConstants.ClinicalDocumentEntity);

            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));

            var localizedString = new LocalizedString("SuccessMessage", "Record : {0} saved successfully.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["SuccessMessage"]).Returns(localizedString);

            ClinicalDocumentBusinessLogic _serviceSave =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultSave = await _serviceSave.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel);

            //Assert
            Assert.NotNull(resultSave);
        }

        /// <summary>
        /// Save Clinical Details OKResult
        /// </summary>
        [Fact]
        public async void SaveClinicalDetailsOKResult()
        {
            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            mockClinicalDocumentUnitOfWork.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentEntity)).ReturnsAsync(TestConstants.ClinicalDocumentId);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<entities.ClinicalDocumentMetadata>(TestConstants.ClinicalDocumentInputModel)).Returns(TestConstants.ClinicalDocumentEntity);

            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));

            var localizedString = new LocalizedString("SuccessMessage", "Record : {0} saved successfully.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["SuccessMessage"]).Returns(localizedString);

            ClinicalDocumentBusinessLogic _serviceSave =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultSave = await _serviceSave.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel);

            Assert.Equal("Record : a4a0d3a2-779a-4d59-8a26-dd566b7195f1 saved successfully.", resultSave.ResponseMessage);
        }


        /// <summary>
        /// SaveClinicalDetails Throws Exception When Details NotSaved
        /// </summary>
        [Fact]
        public void SaveClinicalDetailsThrowsExceptionWhenDetailsNotSaved()
        {
            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            mockClinicalDocumentUnitOfWork.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentEntity)).ReturnsAsync(TestConstants.ClinicalDocumentId);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<entities.ClinicalDocumentMetadata>(TestConstants.ClinicalDocumentInputModel)).Returns(TestConstants.ClinicalDocumentEntityEmpty);

            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));

            var localizedString = new LocalizedString("Exception_Error_Message", "Error occured while saving the data");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["Exception_Error_Message"]).Returns(localizedString);

            ClinicalDocumentBusinessLogic _serviceSave =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            Assert.ThrowsAsync<Exception>(() => new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object).SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel));
        }

        /// <summary>
        /// SaveClinicalDetails Throws Exception When Document not saved by Document Service
        /// </summary>
        [Fact]
        public async void SaveClinicalDetailsThrowsExceptionDocumentNotSaved()
        {
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            mockClinicalDocumentUnitOfWork.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentEntityInvalid)).ReturnsAsync(TestConstants.ClinicalDocumentId);

            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            mapClinicalDocument.Setup(m => m.Map<DocumentMetadata>(It.IsAny<object>())).Returns(TestConstants.DocumentMetadataInvalid);
            mapClinicalDocument.Setup(m => m.Map<entities.ClinicalDocumentMetadata>(TestConstants.ClinicalDocumentInputModelInvalid)).Returns(TestConstants.ClinicalDocumentEntityInvalid);

            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadataInvalid)).ReturnsAsync((TestConstants.StatusInvlaid, TestConstants.KeyInvalid));

            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            var localizedString = new LocalizedString("SuccessMessage", "Record : {0} saved successfully.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["SuccessMessage"]).Returns(localizedString);

            var localizedStringDocumentServiceSuccess = new LocalizedString("DocumentServiceSuccessMessage", "Completed");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(m => m["DocumentServiceSuccessMessage"]).Returns(localizedStringDocumentServiceSuccess);

            var localizedStringDocumentServiceFailure = new LocalizedString("DocumentServiceFailureMessage", "Error Saving Document.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(m => m["DocumentServiceFailureMessage"]).Returns(localizedStringDocumentServiceFailure);

            ClinicalDocumentBusinessLogic _serviceSave =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultSave = await _serviceSave.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModelInvalid);

            //Assert
            Assert.Contains("Error Saving Document.", resultSave.ResponseMessage);
        }


        /// <summary>
        /// When clincial document metadata is null then returns no record found
        /// </summary>
        [Fact]
        public async void GetWhenClinicalDocumentMetadataNullThenReturnsNoRecordResult()
        {
            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPR, TestConstants.AccountNumberForNullRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsNull);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<ICollection<ClinicalDocumentMetadata>>(TestConstants.ListOfClinicalDocumentsNull)).Returns(TestConstants.ListOfClinicalDocumentsMetadataNull);

            var localizedString = new LocalizedString("EmptyMetadataMessage", "No records found.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["EmptyMetadataMessage"]).Returns(localizedString);
            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));

            ClinicalDocumentBusinessLogic _serviceGet =
            new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultGet = await _serviceGet.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPR, TestConstants.AccountNumberForNullRecord);

            //Assert
            Assert.Empty(resultGet.ClinicalDocumentMetadata);
            Assert.Equal(TestConstants.ClinicalDocumentDetailsWithNoRecords.Status, resultGet.Status);
        }

        /// <summary>
        /// When clinical document metadata is empty returns no record found
        /// </summary>
        [Fact]
        public async void GetWhenClinicalDocumentMetadataEmptyThenReturnsNoRecordResult()
        {
            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPK, TestConstants.AccountNumberForEmptyRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsEmptyRecords);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<ICollection<ClinicalDocumentMetadata>>(TestConstants.ListOfClinicalDocumentsEmptyRecords)).Returns(TestConstants.ListOfClinicalDocumentsMetadataEmptyRecords);

            var localizedString = new LocalizedString("EmptyMetadataMessage", "No records found.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["EmptyMetadataMessage"]).Returns(localizedString);
            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));
            ClinicalDocumentBusinessLogic _serviceGet =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultGet = await _serviceGet.GetClinicalDocumentDetails(TestConstants.FaclityCodeSJPK, TestConstants.AccountNumberForEmptyRecord);

            //Assert
            Assert.Empty(resultGet.ClinicalDocumentMetadata);
            Assert.Equal(TestConstants.ClinicalDocumentDetailsWithNoRecords.Status, resultGet.Status);
        }


        /// <summary>
        /// when clinical document is having records but cross walk configuration is null or empty then return Clinical Document Record present at database
        /// </summary>
        [Fact]
        public async void GetWhenClinicalDocumentMetadataCrosswalkConfigurationNullOrEmptyThenReturnsClinicalDocumentRecordResult()
        {
            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsWithRecords);
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetDocumentCrosswalkConfiguration()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkConfigurationEmpty);
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetDocumentCrosswalkDetails()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkWithRecords);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<ICollection<ClinicalDocumentMetadata>>(TestConstants.ListOfClinicalDocumentsWithRecords)).Returns(TestConstants.ListOfClinicalDocumentsMetadataWithRecords);

            var localizedString = new LocalizedString("ValidMetadataMessage", "Records found.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["ValidMetadataMessage"]).Returns(localizedString);
            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));
            ClinicalDocumentBusinessLogic _serviceGet =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultGet = await _serviceGet.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord);

            //Assert
            Assert.NotNull(resultGet);
            Assert.Equal(TestConstants.ClinicalDocumentDetailsWithRecords.Status, resultGet.Status);
        }

        /// <summary>
        /// when clinical document is having records but cross walk  is null or empty then return no record found
        /// </summary>
        [Fact]
        public async void GetWhenClinicalDocumentMetadataCrosswalkNullOrEmptyThenReturnsClinicalDocumentRecordResult()
        {

            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsWithRecords);
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetDocumentCrosswalkConfiguration()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkConfigurationWithRecords);
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetDocumentCrosswalkDetails()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkEmpty);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<ICollection<ClinicalDocumentMetadata>>(TestConstants.ListOfClinicalDocumentsWithRecords)).Returns(TestConstants.ListOfClinicalDocumentsMetadataWithRecords);

            var localizedString = new LocalizedString("ValidMetadataMessage", "Records found.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["ValidMetadataMessage"]).Returns(localizedString);
            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));
            ClinicalDocumentBusinessLogic _serviceGet =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultGet = await _serviceGet.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord);

            //Assert
            Assert.NotNull(resultGet);
            Assert.Equal(TestConstants.ClinicalDocumentDetailsWithRecords.Status, resultGet.Status);
        }

        /// <summary>
        /// when clinical document is having records and match with cross walk configuration then return result as record found with cross walk document name
        /// and record that not match with document cross walk return document name as is present at clinical documents
        /// </summary>
        [Fact]
        public async void GetWhenClinicalDocumentMetadataWithCrosswalkMatchThenReturnRecordFoundResult()
        {

            //Act
            Mock<IClinicalDocumentUnitOfWork> mockClinicalDocumentUnitOfWork = new Mock<IClinicalDocumentUnitOfWork>();
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord)).ReturnsAsync(TestConstants.ListOfClinicalDocumentsWithRecords);
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetDocumentCrosswalkConfiguration()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkConfigurationWithRecords);
            mockClinicalDocumentUnitOfWork.Setup(_ => _.GetDocumentCrosswalkDetails()).ReturnsAsync(TestConstants.ListOfDocumentCrosswalkWithRecords);
            Mock<IMapper> mapClinicalDocument = new Mock<IMapper>();
            Mock<IDocumentServiceHelper> mockDocumentServiceHelper = new Mock<IDocumentServiceHelper>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentBusinessLogicLocalizer = new Mock<IStringLocalizer<SharedResources>>();
            mapClinicalDocument.Setup(m => m.Map<ICollection<ClinicalDocumentMetadata>>(TestConstants.ListOfClinicalDocumentsWithRecords)).Returns(TestConstants.ListOfClinicalDocumentsMetadataWithRecords);

            var localizedString = new LocalizedString("ValidMetadataMessage", "Records found.");
            mockIClinicalDocumentBusinessLogicLocalizer.Setup(x => x["ValidMetadataMessage"]).Returns(localizedString);
            mockDocumentServiceHelper.Setup(m => m.UploadDocuments(TestConstants.DocumentMetadata)).ReturnsAsync((TestConstants.Status, TestConstants.Key));
            ClinicalDocumentBusinessLogic _serviceGet =
                new ClinicalDocumentBusinessLogic(mockClinicalDocumentUnitOfWork.Object, mapClinicalDocument.Object, mockIClinicalDocumentBusinessLogicLocalizer.Object, mockDocumentServiceHelper.Object);

            var resultGet = await _serviceGet.GetClinicalDocumentDetails(TestConstants.FaclityCodeBOMC, TestConstants.AccountNumberWithRecord);

            //Assert
            Assert.NotNull(resultGet);
            Assert.Equal(TestConstants.ClinicalDocumentDetailsWithRecords.Status, resultGet.Status);
        }
    }
}

