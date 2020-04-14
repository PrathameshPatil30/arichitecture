using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace R1.ClinicalDocument.API
{
    /// <summary>
    /// 
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Web HostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
           .UseNLog();
    }
}
