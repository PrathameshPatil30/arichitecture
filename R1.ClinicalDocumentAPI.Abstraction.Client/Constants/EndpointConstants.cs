namespace R1.ClinicalDocumentAPI.Abstraction.Client.Constants
{
    /// <summary>
    /// constant url for clinical document api 
    /// </summary>
    public class EndpointConstants
    {
        /// <summary>
        /// save clinical document api for version 1 url
        /// this wil be fixed here since api v1 url will be fixed and not change
        /// incase of url change it will we newer version of nuget to be changed and made here
        /// </summary>
        public const string SaveClinicalDocument = "ClinicalDocument/Save";
        /// <summary>
        /// get clinical document api for version 1 url
        /// this wil be fixed here since api v1 url will be fixed and not change
        /// incase of url change it will we newer version of nuget to be changed and made here
        /// </summary>
        public const string GetClinicalDocument = "ClinicalDocument/Get?facilityCode={0}&accountNumber={1}";
    }
}
