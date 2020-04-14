using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using R1.ClinicalDocumentAPI.Abstraction.Client;
using R1.ClinicalDocumentAPI.Abstraction.Client.Configuration;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Request;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response;
using R1.ClinicalDocumentAPI.Client.Rest;
using Xunit;

namespace Test.R1.ClinicalDocumentAPI.Client.Rest
{
    /// <summary>
    /// Unit test class to test Clinical Document API client
    /// </summary>
    public class RestClinicalDocumentAPIShould
    {
        #region variable declarations
        private const string validUrl = "http://devrhubwweb11:90/api/v1";
        private const string inValidUrl = "http://devrhubwweb1121:90/api/v1";
        #endregion

        #region Save Clinical Documents

        [Fact(Skip = "Skipped since this is integration test and it inserts into database")]
        public async void SaveClinicalDocumentDetails()
        {
            var mockILogger = new Mock<ILoggerFactory>();
            var mockIOptionsClinicalDocumentAPIConfig = new Mock<IOptions<ClinicalDocumentAPIConfig>>();
            var mockIRestClinicalDocumentAPI = new Mock<IRestClinicalDocumentAPI>();
            DocumentDetailRequest documentDetailRequest = CreateDocumentDetailRequestObject();

            SetClinicalDocumentApiConfig(validUrl, mockIOptionsClinicalDocumentAPIConfig);
            var documentDetailResponse = new DocumentDetailResponse { ResponseMessage = "Success" };

            var restClinicalDocumentAPI = new RestClinicalDocumentAPI(mockIOptionsClinicalDocumentAPIConfig.Object, mockILogger.Object);

            var result = await restClinicalDocumentAPI.SaveClinicalDocumentDetails(documentDetailRequest);
            Assert.True(result.IsValid);
            Assert.True(result.ResponseMessage.Length > 0);
            Assert.Contains("successfully", result.ResponseMessage);
        }

        [Fact]
        public async void SaveClinicalDocumentDetailsReturnsBadRequestWhenParameterPassedIsInvalid()
        {
            var mockILogger = new Mock<ILoggerFactory>();
            var mockIOptionsClinicalDocumentAPIConfig = new Mock<IOptions<ClinicalDocumentAPIConfig>>();

            DocumentDetailRequest documentDetailRequest = CreateDocumentDetailRequestObject();
            documentDetailRequest.FacilityCode = "TESTWA";

            SetClinicalDocumentApiConfig(validUrl, mockIOptionsClinicalDocumentAPIConfig);
            var documentDetailResponse = new DocumentDetailResponse { ResponseMessage = "Success" };

            var restClinicalDocumentAPI = new RestClinicalDocumentAPI(mockIOptionsClinicalDocumentAPIConfig.Object, mockILogger.Object);

            var result = await restClinicalDocumentAPI.SaveClinicalDocumentDetails(documentDetailRequest);
            Assert.True((!result.IsValid));
            Assert.Equal("Validation Failed", result.ValidationResponse.Message);
        }



        [Fact]
        public async void ThrowsExceptionFromSaveClinicalDocumentDetailsWhenURLIsInvalid()
        {
            var mockILogger = new Mock<ILoggerFactory>();
            var mockIOptionsClinicalDocumentAPIConfig = new Mock<IOptions<ClinicalDocumentAPIConfig>>();
            var mockIRestClinicalDocumentAPI = new Mock<IRestClinicalDocumentAPI>();
            DocumentDetailRequest documentDetailRequest = CreateDocumentDetailRequestObject();

            SetClinicalDocumentApiConfig(inValidUrl, mockIOptionsClinicalDocumentAPIConfig);
            var documentDetailResponse = new DocumentDetailResponse { ResponseMessage = "Success" };

            var restClinicalDocumentAPI = new RestClinicalDocumentAPI(mockIOptionsClinicalDocumentAPIConfig.Object, mockILogger.Object);

            await Assert.ThrowsAsync<NullReferenceException>(() => restClinicalDocumentAPI.SaveClinicalDocumentDetails(documentDetailRequest));
        }

        #endregion

        #region Get Clinical Documents
        [Fact]
        public async void GetClinicalDocuments()
        {
            var mockILogger = new Mock<ILoggerFactory>();
            var mockIOptionsClinicalDocumentAPIConfig = new Mock<IOptions<ClinicalDocumentAPIConfig>>();
            var mockIRestClinicalDocumentAPI = new Mock<IRestClinicalDocumentAPI>();

            SetClinicalDocumentApiConfig(validUrl, mockIOptionsClinicalDocumentAPIConfig);

            var restClinicalDocumentAPI = new RestClinicalDocumentAPI(mockIOptionsClinicalDocumentAPIConfig.Object, mockILogger.Object);
            var result = await restClinicalDocumentAPI.GetClinicalDocumentDetails("SURG", "89455455");
            Assert.Contains("Records found", result.Status);
            Assert.True(result.ClinicalDocumentMetadata.Count > 0);
        }

        [Fact]
        public async void GetClinicalDocumentsReturnsBadRequestWhenParameterPassedIsNull()
        {
            var mockILogger = new Mock<ILoggerFactory>();
            var mockIOptionsClinicalDocumentAPIConfig = new Mock<IOptions<ClinicalDocumentAPIConfig>>();

            SetClinicalDocumentApiConfig(validUrl, mockIOptionsClinicalDocumentAPIConfig);

            var restClinicalDocumentAPI = new RestClinicalDocumentAPI(mockIOptionsClinicalDocumentAPIConfig.Object, mockILogger.Object);
            var result = await restClinicalDocumentAPI.GetClinicalDocumentDetails(null, "123456");
            Assert.True(!(result.IsValid));
            Assert.Null(result.ClinicalDocumentMetadata);
        }

        [Fact]
        public async void ThrowsExceptionFromGetClinicalDocumentsWhenURLIsInvalid()
        {
            var mockILogger = new Mock<ILoggerFactory>();
            var mockIOptionsClinicalDocumentAPIConfig = new Mock<IOptions<ClinicalDocumentAPIConfig>>();
            var mockIRestClinicalDocumentAPI = new Mock<IRestClinicalDocumentAPI>();

            SetClinicalDocumentApiConfig(inValidUrl, mockIOptionsClinicalDocumentAPIConfig);

            var restClinicalDocumentAPI = new RestClinicalDocumentAPI(mockIOptionsClinicalDocumentAPIConfig.Object, mockILogger.Object);

            await Assert.ThrowsAsync<NullReferenceException>(() => restClinicalDocumentAPI.GetClinicalDocumentDetails("TEST", "123456"));
        }
        #endregion

        #region Private methods - Test helpers
        private static void SetClinicalDocumentApiConfig(string url, Mock<IOptions<ClinicalDocumentAPIConfig>> mockIOptionsClinicalDocumentAPIConfig)
        {
            ClinicalDocumentAPIConfig clinicalDocumentAPIConfig = new ClinicalDocumentAPIConfig { ClinicalDocumentApi = url };
            mockIOptionsClinicalDocumentAPIConfig.Setup(o => o.Value).Returns(clinicalDocumentAPIConfig);
        }

        private static DocumentDetailRequest CreateDocumentDetailRequestObject()
        {
            return new DocumentDetailRequest
            {
                AccountNumber = "12345",
                FacilityCode = "TEST",
                DocumentType = "OPR",
                DocumentName = "OPReport",
                MimeType = "text/plain",
                SourceSystem = "GCR",
                Document = new byte[5]
            };
        }
        #endregion
    }
}
