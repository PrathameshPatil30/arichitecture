
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Contract.ClinicalDocument.ErrorResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using R1.Core.Logging;
using R1.Core.Logging.Entity;
using Resources;

namespace R1.ClinicalDocument.API.Middleware
{
    /// <summary>
    /// Handles the exceptions at the centralized level
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        /// <summary>
        /// Json content type 
        /// </summary>
        private const string JsonContentType = "application/json";

        /// <summary>
        /// Request delegate
        /// </summary>
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// Http Context Accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogManager _logManager;

        /// <summary>
        /// Private property for injecting dependency IMapper interface
        /// </summary>
        private readonly IStringLocalizer<SharedResources> _localizer;

        /// <summary>
        /// Constructor for Exception handler middleware
        /// </summary>
        /// <param name="requestDelegate">Request delegate object</param>
        /// <param name="httpContextAccessor">HttpContext accessor</param>
        /// <param name="logManager">logger</param>
        /// <param name="localizer">localizer</param>
        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, IHttpContextAccessor httpContextAccessor, ILogManager logManager, IStringLocalizer<SharedResources> localizer)
        {
            _requestDelegate = requestDelegate;
            _httpContextAccessor = httpContextAccessor;
            _logManager = logManager;
            _localizer = localizer;
        }

        /// <summary>
        /// Method which would get invoked whenany exception occured
        /// </summary>
        /// <param name="context">Http context object</param>
        public Task Invoke(HttpContext context) => InvokeAsync(context);

        /// <summary>
        /// Invoke async
        /// </summary>
        /// <param name="context">context</param>
        /// <returns></returns>
        async Task InvokeAsync(HttpContext context)
        {

            string location = string.Empty;
            int userID = 0;
            int registrationID = 0;
            try
            {
                if (context != null)
                {

                    if (context.Request.Path.Value.Contains("favicon.ico"))
                    {
                        return;
                    }

                    await _requestDelegate(context).ConfigureAwait(true);
                }
                else
                {
                    throw new Exception(message: _localizer["InvalidContextMessage"]);
                }
            }
            catch (Exception exception)
            {
                int httpStatusCode = (int)HttpStatusCode.InternalServerError;
                if (exception != null)
                {
                    _logManager.LogException(exception, _localizer["ExceptionTitle"], new LogInfo() { UserId = userID, RecordId = registrationID, Location = location, ClassFullName = this.GetType().FullName.ToString() });
                    httpStatusCode = ConfigurateExceptionTypes(exception);
                }
                // set http status code and content type
                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = JsonContentType;

                // writes / returns error model to the response
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ServerErrorDetails
                {
                    ResponseMessage = _localizer["InternalServerErrorMessage"]
                })).ConfigureAwait(true);
            }

        }


        /// <summary>
        /// Method would configure the types of exception when any exception occured
        /// </summary>
        /// <param name="exception">Exception object</param>
        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode;

            // Exception type To Http Status configuration 
            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            return httpStatusCode;
        }

    }
}
