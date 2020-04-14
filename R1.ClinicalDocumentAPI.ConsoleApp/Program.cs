using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Extensions.Logging;
using R1.ClinicalDocumentAPI.Abstraction.Client;
using R1.ClinicalDocumentAPI.Abstraction.Client.Configuration;
using R1.ClinicalDocumentAPI.Abstraction.Client.Models.Request;
using R1.ClinicalDocumentAPI.Client.Rest;



namespace R1.ClinicalDocumentAPI.ConsoleApp
{
    public class Program
    {
        #region Private properties

        /// <summary>
        /// Clinical document API rest class
        /// </summary>
        private IRestClinicalDocumentAPI _restClinicalDocumentAPI { get; set; }

        //Document detail request object for save clinical documents
        private DocumentDetailRequest _documentDetailRequest { get; set; }

        /// <summary>
        /// Facility code to be used to save and get clinical documents
        /// </summary>
        private const string _facilityCode = "SJMA";

        /// <summary>
        /// Account number to be used to save and get clinical documents
        /// </summary>
        private const string _accountNumber = "00123456789";
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// This is used to reference nonstatic method inside static method Main
        /// </summary>
        public Program()
        {

        }
        #endregion

        #region Methods
        static void Main(string[] args)
        {
            Program program = new Program();
            program.MainAsync(args).GetAwaiter().GetResult();
            Console.ReadKey();
        }

        public async Task MainAsync(string[] args)
        {
            LogManager.LoadConfiguration("nlog.config");
            ILoggerFactory loggerFactory = new NLogLoggerFactory();
            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                var configuration = CreateConfigurationObject();
                var clinicalDocumentAPIConfig = new ClinicalDocumentAPIConfig();
                configuration.Bind(clinicalDocumentAPIConfig);
                var options = Options.Create(clinicalDocumentAPIConfig);

                _restClinicalDocumentAPI = new RestClinicalDocumentAPI(options, loggerFactory);
                _documentDetailRequest = CreateDocumentDetailRequest();

                //POST endpoint of clinical document - Save Clinical Documents
                var saveClinicalDocumentsResult = await _restClinicalDocumentAPI.SaveClinicalDocumentDetails(_documentDetailRequest);

                //GET endpoint of clinical document - Get Clinical Documents
                var getClinicalDocumentsResult = await _restClinicalDocumentAPI.GetClinicalDocumentDetails("TEST", "12345");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Clinical Document Console application");
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Setting up appsetting.json file
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot CreateConfigurationObject()
        {
            return new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();
        }

        /// <summary>
        /// Document detail request object for POST endpoint
        /// </summary>
        /// <returns></returns>
        private static DocumentDetailRequest CreateDocumentDetailRequest()
        {
            return new DocumentDetailRequest
            {
                FacilityCode = _facilityCode,
                AccountNumber = _accountNumber,
                DocumentType = "OPR",
                DocumentName = "OperationalReport",
                DocumentReceived = true,
                DocumentId = null,
                MimeType = "text/plain",
                SourceSystem = "GCR",
                Document = Convert.FromBase64String("")
            };
        }
        #endregion
    }
}
