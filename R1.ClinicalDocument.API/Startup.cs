using AutoMapper;
using BusinessLogic.Abstraction.ClinicalDocument;
using BusinessLogic.Abstraction.ClinicalDocument.Mapping;
using BusinessLogic.ClinicalDocument;
using Database.Abstraction.ClinicalDocument.Common;
using Database.Abstraction.ClinicalDocument.Contract.Repository;
using Database.Abstraction.ClinicalDocument.Contract.UnitOfWork;
using Database.ClinicalDocument;
using Database.ClinicalDocument.DataAccess;
using Database.ClinicalDocument.DataAccess.Repository;
using Database.ClinicalDocument.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using R1.ClinicalDocument.API.Extensions;
using R1.ClinicalDocument.API.Mappings;
using R1.Core.Logging;
using R1.DocumentService.Contracts.Clients;
using Services.Contract;
using Services.DocumentService;

namespace R1.ClinicalDocument.API
{
    /// <summary>
    /// Start Up 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// hosting environment
        /// </summary>
        public IWebHostEnvironment Environment { get; }


        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<DbContext, ClinicalDocumentContext>(options => options.UseSqlServer(Configuration.GetValue<string>("ClinicalDocumentConnectionString")));
            services.Configure<RestTransferUtilityOptions>(Configuration.GetSection("DocumentServiceClient"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddTransient<ILogManager, NLogLogger>();
            services.AddScoped<IClinicalDocumentBusinessLogic, ClinicalDocumentBusinessLogic>();
            services.AddScoped<IClinicalDocumentUnitOfWork, ClinicalDocumentUnitOfWork>();
            services.AddScoped<IClinicalDocumentRepository, ClinicalDocumentRepository>();
            services.AddScoped<IDocumentServiceHelper, DocumentServiceHelper>();
            services.AddScoped(typeof(IRepository<Database.ClinicalDocument.Entities.ClinicalDocumentMetadata>), typeof(ClinicalDocumentRepository));
            services.AddScoped<IDocumentTypeXWalkRepository, DocumentTypeXwalkRepository>();
            services.AddScoped(typeof(IRepository<DocumentTypeXwalk>), typeof(DocumentTypeXwalkRepository));
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped(typeof(IRepository<DocumentType>), typeof(DocumentTypeRepository));

            services.AddMvcCore().AddApiExplorer();

            //Custom validations
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(actionContext.ModelState.ToValidationResultModel());
                };
            });

            services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ContractAndModelMapping());
                mc.AddProfile(new ModelAndEntityMapping());
                mc.AllowNullCollections = true;
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services
             .AddLocalization()
             .AddMvc()
             .AddDataAnnotationsLocalization();
            services.AddRazorPages();

            services.AddSwaggerGenerator(Environment);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<Middleware.ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "ClinicalDocumentAPI");
            });
        }
    }
}
