using System;
using System.Linq;
using AutoMapper;
using BusinessLogic.Abstraction.ClinicalDocument;
using BusinessLogic.Abstraction.ClinicalDocument.Model;
using Contract.CliniclaDocument.Request;
using Contract.CliniclaDocument.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using R1.ClinicalDocument.API.Controllers.V1;
using Resources;
using Xunit;

namespace Test.R1.ClinicalDocument.API
{
    public class ClinicalDocumentControllerShould
    {
        #region Validation
        /// <summary>
        /// unit test case method for validating DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateDocumentDetailRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = "SJPR"
            };

            var validDetails = val.ValidateModel(activityObject).Count();
            Assert.Equal("0", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating required account number in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateRequiredaccountNumberRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = "SJPR"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Account Number is required", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating invalid account number in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateMaxLengthaccountNumberRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "WE@#$%QWEQ",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = "SJPR"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Please enter valid Account Number", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating required facility code in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateRequiredFacilityCodeRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = ""
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Facility Code is required", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating length of facility code in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateMaxLengthFacilityCodeRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = "ASSDDFD"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Facility Code must be 4 characters only", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating invalid facility code in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateInvalidFacilityCodeRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = "SHB#"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Please enter valid Facility Code", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating required document type in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateRequiredDocumentTypeRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "",
                FacilityCode = "SJPR"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Document Type is required", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating invalid document type in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateInvalidDocumentTypeRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "!@@#$",
                FacilityCode = "SJPR"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Please enter valid Document Type", validDetails.ToString());
        }

        /// <summary>
        /// unit test case method for validating irequired document name in DocumentDetail class
        /// </summary>
        [Fact]
        public void ValidateRequiredDocumentNameRequest()
        {
            CheckPropertyValidation val = new CheckPropertyValidation();

            var activityObject = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "",
                DocumentType = "JPEG",
                FacilityCode = "SJPR"
            };

            var validDetails = val.ValidateModel(activityObject).ToList().FirstOrDefault();
            Assert.Equal("Document Name is required", validDetails.ToString());
        }
        #endregion


        #region Method


        [Fact]
        public void Exception()
        {
            var mockIMapper = new Mock<IMapper>();
            var mockIClinicalDocumentBusinessLogic = new Mock<IClinicalDocumentBusinessLogic>();
            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentControllerLocalizer = new Mock<IStringLocalizer<SharedResources>>();

            mockIMapper.Setup(m => m.Map<ClinicalDocumentInputModel>(It.IsAny<object>())).Returns(TestConstants.ClinicalDocumentInputModel);

            mockIMapper.Setup(m => m.Map<DocumentDetailResponse>(It.IsAny<object>())).Returns(TestConstants.DocResponse);

            mockIClinicalDocumentBusinessLogic.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel)).ReturnsAsync(TestConstants.ClinicalDocumentOutputModel);

            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentController(null, mockIMapper.Object, mockIClinicalDocumentControllerLocalizer.Object));
            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentController(mockIClinicalDocumentBusinessLogic.Object, null, mockIClinicalDocumentControllerLocalizer.Object));
            Assert.Throws<ArgumentNullException>(() => new ClinicalDocumentController(mockIClinicalDocumentBusinessLogic.Object, mockIMapper.Object, null));

        }


        [Fact]
        public async void SaveClinicalDocumentNotNullResult()
        {
            var mockIMapper = new Mock<IMapper>();

            var mockIClinicalDocumentBusinessLogic = new Mock<IClinicalDocumentBusinessLogic>();

            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentControllerLocalizer = new Mock<IStringLocalizer<SharedResources>>();

            mockIMapper.Setup(m => m.Map<ClinicalDocumentInputModel>(It.IsAny<object>())).Returns(TestConstants.ClinicalDocumentInputModel);

            mockIMapper.Setup(m => m.Map<DocumentDetailResponse>(It.IsAny<object>())).Returns(TestConstants.DocResponse);

            mockIClinicalDocumentBusinessLogic.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel)).ReturnsAsync(TestConstants.ClinicalDocumentOutputModel);

            var clinicalDocumentController = new ClinicalDocumentController(mockIClinicalDocumentBusinessLogic.Object, mockIMapper.Object, mockIClinicalDocumentControllerLocalizer.Object);

            var result = await clinicalDocumentController.SaveClinicaldetails(TestConstants.DocRequest);

            Assert.NotNull(result);
        }

        [Fact]
        public async void SaveClinicalDocumentOkResult()
        {
            var mockIMapper = new Mock<IMapper>();

            var mockIClinicalDocumentBusinessLogic = new Mock<IClinicalDocumentBusinessLogic>();

            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentControllerLocalizer = new Mock<IStringLocalizer<SharedResources>>();

            mockIMapper.Setup(m => m.Map<ClinicalDocumentInputModel>(It.IsAny<object>())).Returns(TestConstants.ClinicalDocumentInputModel);

            mockIMapper.Setup(m => m.Map<DocumentDetailResponse>(It.IsAny<object>())).Returns(TestConstants.DocResponse);

            mockIClinicalDocumentBusinessLogic.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel)).ReturnsAsync(TestConstants.ClinicalDocumentOutputModel);

            var clinicalDocumentController = new ClinicalDocumentController(mockIClinicalDocumentBusinessLogic.Object, mockIMapper.Object, mockIClinicalDocumentControllerLocalizer.Object);

            var result = await clinicalDocumentController.SaveClinicaldetails(TestConstants.DocRequest);

            var responseMessage = ((DocumentDetailResponse)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value).ResponseMessage;

            Assert.Equal("Record : 465A6717 - 9956 - 4DC3 - 8C94 - FE9B09CB2B84 saved successfully.", responseMessage);
        }


        [Fact]
        public async void SaveClinicalDocumentReturnsBadRequest()
        {
            var mockIMapper = new Mock<IMapper>();

            var mockIClinicalDocumentBusinessLogic = new Mock<IClinicalDocumentBusinessLogic>();

            Mock<IStringLocalizer<SharedResources>> mockIClinicalDocumentControllerLocalizer = new Mock<IStringLocalizer<SharedResources>>();

            mockIMapper.Setup(m => m.Map<ClinicalDocumentInputModel>(It.IsAny<object>())).Returns(TestConstants.ClinicalDocumentInputModel);

            mockIMapper.Setup(m => m.Map<DocumentDetailResponse>(It.IsAny<object>())).Returns(TestConstants.DocResponse);

            mockIClinicalDocumentBusinessLogic.Setup(x => x.SaveClinicalDetails(TestConstants.ClinicalDocumentInputModel)).ReturnsAsync(TestConstants.ClinicalDocumentOutputModelNull);

            var clinicalDocumentController = new ClinicalDocumentController(mockIClinicalDocumentBusinessLogic.Object, mockIMapper.Object, mockIClinicalDocumentControllerLocalizer.Object);

            var result = await clinicalDocumentController.SaveClinicaldetails(TestConstants.DocRequest);

            Assert.IsType<BadRequestObjectResult>(result);

        }


        #endregion
    }
}
