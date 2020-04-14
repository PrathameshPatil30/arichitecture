using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Contract.ClinicalDocument.ErrorResponse;
using Contract.CliniclaDocument.Request;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using R1.ClinicalDocument.API;
using TechTalk.SpecFlow;
using Xunit;

namespace Test.R1.BDD.StepDefinition
{
    /// <summary>
    /// Step Definition for Saving clinical documents
    /// </summary>
    [Binding]
    public class SaveClinicalDocument_Step : WebApplicationFactory<Startup>
    {

        private HttpClient _client { get; set; }
        private HttpResponseMessage _responseMessage;
        private readonly TestServer _server;
        private readonly DocumentDetailRequest _documentDetailRequest_Contract;
        private const string _baseAddress = "http://localhost:5001/api/v1/ClinicalDocument/";
        private const string _saveClinicalDocument = "Save";
        private ValidationResultModel _validationResultModel;


        /// <summary>
        /// Save Clinical Document Constructor
        /// </summary>
        public SaveClinicalDocument_Step()
        {
            _server = new TestServer(
                               WebHost.CreateDefaultBuilder()
                               .UseStartup<Startup>()
                           );
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _documentDetailRequest_Contract = new DocumentDetailRequest
            {
                AccountNumber = "1234567",
                DocumentName = "OpsReport",
                DocumentType = "JPEG",
                FacilityCode = "SJPR",
                MimeType = "image/jpeg",
                SourceSystem = "GCR",
                Document = Convert.FromBase64String("")
            };
            _validationResultModel = new ValidationResultModel();
        }

        /// <summary>
        ///  Given function of BDD scenario
        /// </summary>
        [Given(@"I have received Patients Clinical Document Details")]
        public void GivenIHaveReceivedPatientsClinicalDocumentDetails()
        {
            /*Created request object at the parent level*/
        }

        /// <summary>
        ///  When function of BDD scenario
        /// </summary>
        /// <returns></returns>
        [When(@"I consume JSON and make API call")]
        public async System.Threading.Tasks.Task WhenIConsumeJSONAndMakeAPICall()
        {
            var requestJson = JsonConvert.SerializeObject(_documentDetailRequest_Contract);
            var stringContent = new StringContent(requestJson.ToString(), Encoding.UTF8, "application/json");
            Uri requestUrl = new Uri(_baseAddress + _saveClinicalDocument);
            _responseMessage = await _client.PostAsync(requestUrl, stringContent);
        }

        /// <summary>
        ///  Then function of BDD scenario
        /// </summary>
        [Then(@"the result should be saved in database")]
        public void ThenTheResultShouldBeSavedInDatabase()
        {
            _responseMessage.EnsureSuccessStatusCode();
            Assert.NotNull(_responseMessage);
        }

        /// <summary>
        /// Then function of BDD scenario
        /// </summary>
        [Then(@"We receive response message in response body")]
        public void ThenWeReceiveResponseMessageInResponseBody()
        {
            Assert.Contains("saved successfully", _responseMessage.Content.ReadAsStringAsync().Result);
        }


        /// <summary>
        /// Given function of BDD scenario
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        [Given(@"I have request JSON for saving clinical documents with ""(.*)"" ""(.*)""")]
        public void GivenIHaveRequestJSONForSavingClinicalDocumentsWith(string parameter, string value)
        {
            if (parameter == "AccountNumber")
            {
                _documentDetailRequest_Contract.AccountNumber = value;
            }
            else if (parameter == "FacilityCode")
            {
                _documentDetailRequest_Contract.FacilityCode = value;
            }
            else if (parameter == "DocumentType")
            {
                _documentDetailRequest_Contract.DocumentType = value;
            }
            else if (parameter == "DocumentName")
            {
                _documentDetailRequest_Contract.DocumentName = value;
            }
        }


        /// <summary>
        /// When function of BDD scenario
        /// </summary>
        [When(@"I call the API service")]
        public void WhenICallTheAPIService()
        {
            var requestJson = JsonConvert.SerializeObject(_documentDetailRequest_Contract);
            var stringContent = new StringContent(requestJson.ToString(), Encoding.UTF8, "application/json");
            Uri requestUrl = new Uri(_baseAddress + _saveClinicalDocument);
            _responseMessage = _client.PostAsync(requestUrl, stringContent).Result;
        }


        /// <summary>
        /// Then function of BDD scenario
        /// </summary>
        /// <param name="statusCode"></param>
        [Then(@"user receive HTTP response (.*)")]
        public void ThenUserReceiveHTTPResponse(int statusCode)
        {
            Assert.NotNull(_responseMessage);
            Assert.Equal(statusCode, (int)_responseMessage.StatusCode);
        }

        /// <summary>
        /// Then function of BDD scenario
        /// </summary>
        /// <param name="errorMessage"></param>
        [Then(@"""(.*)"" in response body")]
        public void ThenInResponseBody(string errorMessage)
        {
            _validationResultModel = JsonConvert.DeserializeObject<ValidationResultModel>(_responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Contains(errorMessage, _validationResultModel.Errors.FirstOrDefault().Message);
        }

        /// <summary>
        /// Given function of BDD scenario
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        [Given(@"I have request JSON for saving clinical documents in API with ""(.*)"" ""(.*)""")]
        public void GivenIHaveRequestJSONForSavingClinicalDocumentsInAPIWith(string parameter, string value)
        {
            if (parameter == "AccountNumber")
            {
                _documentDetailRequest_Contract.AccountNumber = value;
            }
            else if (parameter == "FacilityCode")
            {
                _documentDetailRequest_Contract.FacilityCode = value;
            }
            else if (parameter == "DocumentType")
            {
                _documentDetailRequest_Contract.DocumentType = value;
            }
            else if (parameter == "DocumentName")
            {
                _documentDetailRequest_Contract.DocumentName = value;
            }
        }

        /// <summary>
        /// When function of BDD scenario
        /// </summary>
        [When(@"I call the Clinical Document API service")]
        public void WhenICallTheClinicalDocumentAPIService()
        {
            var requestJson = JsonConvert.SerializeObject(_documentDetailRequest_Contract);
            var stringContent = new StringContent(requestJson.ToString(), Encoding.UTF8, "application/json");
            Uri requestUrl = new Uri(_baseAddress + _saveClinicalDocument);
            _responseMessage = _client.PostAsync(requestUrl, stringContent).Result;
        }

        /// <summary>
        /// Then function of BDD scenario
        /// </summary>
        /// <param name="statusCode"></param>
        [Then(@"user receives the HTTP response (.*)")]
        public void ThenUserReceivesTheHTTPResponse(int statusCode)
        {
            Assert.NotNull(_responseMessage);
            Assert.Equal(statusCode, (int)_responseMessage.StatusCode);
        }

        /// <summary>
        /// Then function of BDD scenario
        /// </summary>
        /// <param name="errorMessage"></param>
        [Then(@"""(.*)"" in the response body")]
        public void ThenInTheResponseBody(string errorMessage)
        {
            _validationResultModel = JsonConvert.DeserializeObject<ValidationResultModel>(_responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Contains(errorMessage, _validationResultModel.Errors.FirstOrDefault().Message);
        }

    }
}
