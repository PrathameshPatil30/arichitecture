using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Contract.ClinicalDocument.ErrorResponse;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using R1.ClinicalDocument.API;
using TechTalk.SpecFlow;
using Xunit;

namespace Test.R1.BDD.StepDefinition
{
    /// <summary>
    /// 
    /// </summary>
    [Binding]
    public class GetClinicalDocument_Step
    {
        private HttpClient _client { get; set; }
        private HttpResponseMessage _responseMessage;
        private readonly TestServer _server;
        private const string _baseAddress = "http://localhost:5001/api/v1/ClinicalDocument/";
        private const string _getClinicalDocumentDetails = "Get";
        private readonly string _facilityCode = "SJPK";
        private readonly string _accountNumber = "Acoount12345";
        private ValidationResultModel _validationResultModel;

        /// <summary>
        /// constructor to initialise properties
        /// </summary>
        public GetClinicalDocument_Step()
        {
            _server = new TestServer(
                               WebHost.CreateDefaultBuilder()
                               .UseStartup<Startup>()
                           );
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _validationResultModel = new ValidationResultModel();
        }

        /// <summary>
        /// Given function of BDD scenario
        /// </summary>
        [Given(@"I have received Patients Facility Code and Account Number")]
        public void GivenIHaveReceivedPatientsFacilityCodeAndAccountNumber()
        {
            /*No actions required*/
        }


        /// <summary>
        /// When function of BDD scenario
        /// </summary>
        [When(@"I make API call and pass ""(.*)"" and ""(.*)""")]
        public void WhenIMakeAPICallAndPassAnd(string FacilityCode, string AccountNumber)
        {
            Uri requestUrl = new Uri(_baseAddress + _getClinicalDocumentDetails + "?facilityCode=" + FacilityCode + "&accountNumber=" + AccountNumber);
            _responseMessage = _client.GetAsync(requestUrl).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        [When(@"I make API call")]
        public void WhenIMakeAPICall()
        {
            Uri requestUrl = new Uri(_baseAddress + _getClinicalDocumentDetails + "?facilityCode=" + _facilityCode + "&accountNumber=" + _accountNumber);
            _responseMessage = _client.GetAsync(requestUrl).Result;
        }



        /// <summary>
        /// Then function of BDD scenario
        /// </summary>
        [Then(@"We receive document details in response body")]
        public void ThenWeReceiveDocumentDetailsInResponseBody()
        {
            _responseMessage.EnsureSuccessStatusCode();
            Assert.NotNull(_responseMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        [Then(@"We should receive status ""(.*)""")]
        public void ThenWeShouldReceiveStatus(string response)
        {
            Assert.Contains(response, _responseMessage.Content.ReadAsStringAsync().Result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        [Then(@"The GET API call user receive HTTP response (.*)")]
        public void ThenTheGETAPICallUserReceiveHTTPResponse(int statusCode)
        {
            Assert.NotNull(_responseMessage);
            Assert.Equal(statusCode, (int)_responseMessage.StatusCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        [Then(@"""(.*)"" in response body of GET API call")]
        public void ThenInResponseBodyOfGETAPICall(string errorMessage)
        {
            _validationResultModel = JsonConvert.DeserializeObject<ValidationResultModel>(_responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Contains(errorMessage, _validationResultModel.Errors.FirstOrDefault().Message);
        }



    }
}
