using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using R1.ClinicalDocumentAPI.Abstraction.Client;
using R1.ClinicalDocumentAPI.Abstraction.Client.Configuration;
using R1.ClinicalDocumentAPI.Abstraction.Client.Constants;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Request;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Response;


namespace R1.ClinicalDocumentAPI.Client.Rest
{
    /// <summary>
    /// HTTP client for Clinical Document API
    /// </summary>
    public class RestClinicalDocumentAPI : IRestClinicalDocumentAPI
    {
        #region Properties

        /// <summary>
        /// logger
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// IOptions used here to read value from config file
        /// </summary>
        private ClinicalDocumentAPIConfig _options;

        /// <summary>
        /// base url of the clinical document api
        /// </summary>
        private string _baseUrl { get; set; }


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">R1 core logger to log exceptions to database</param>
        /// <param name="options">IOptions to read config values</param>
        public RestClinicalDocumentAPI(IOptions<ClinicalDocumentAPIConfig> options, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RestClinicalDocumentAPI>();
            _options = options.Value;
            _baseUrl = _options.ClinicalDocumentApi;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Save clinical documents to database
        /// </summary>
        /// <param name="documentDetailRequest"></param>
        /// <returns></returns>
        public async Task<DocumentDetailResponse> SaveClinicalDocumentDetails(DocumentDetailRequest documentDetailRequest)
        {
            try
            {
                string postURL = $"{_baseUrl}/{EndpointConstants.SaveClinicalDocument}";
                var postContent = new StringContent(JsonConvert.SerializeObject(documentDetailRequest), System.Text.Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                using (var httpResponse = await httpClient.PostAsync(postURL, postContent))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<DocumentDetailResponse>(content);
                    }
                    else if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var badResponse = await BadRequestAsync(httpResponse);
                        return new DocumentDetailResponse { ValidationResponse = badResponse };
                    }
                    else
                    {
                        var errorContent = await httpResponse.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                        return new DocumentDetailResponse { ExceptionMessage = errorResponse.ExceptionMessage };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.SaveExceptionMessage);
                return new DocumentDetailResponse { ExceptionMessage = Messages.ExceptionMessage };
            }
        }

        /// <summary>
        /// Fetch clinical document data from database
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="accountNumber"></param>
        /// <returns>Collection of clinical documents</returns>
        public async Task<ClinicalDocumentResponse> GetClinicalDocumentDetails(string facilityCode, string accountNumber)
        {
            try
            {
                string getURL = $"{_baseUrl}/{EndpointConstants.GetClinicalDocument}";
                string requestUrl = String.Format(getURL, facilityCode, accountNumber);
                HttpClient httpClient = new HttpClient();
                using (var httpResponse = await httpClient.GetAsync(requestUrl))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ClinicalDocumentResponse>(content);
                    }
                    else if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var badResponse = await BadRequestAsync(httpResponse);
                        return new ClinicalDocumentResponse { ValidationResponse = badResponse };
                    }
                    else
                    {
                        var errorContent = await httpResponse.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                        return new ClinicalDocumentResponse { ExceptionMessage = errorResponse.ExceptionMessage, ClinicalDocumentMetadata = null };
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, Messages.GetExceptionMessage);
                return new ClinicalDocumentResponse { ExceptionMessage = Messages.ExceptionMessage };
            }
        }


        /// <summary>
        /// Create bad request object for http client
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task<BadRequestResponse> BadRequestAsync(HttpResponseMessage result)
        {
            var content = string.Empty;
            content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BadRequestResponse>(content);
        }


        #endregion




    }
}
