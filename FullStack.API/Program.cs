using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace FullStack.API
{
    public class Program
    {

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .UseSerilog()
               .UseStartup<Startup>();

        public static void Main(string[] args)
        {

            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .WriteTo.Console(LogEventLevel.Verbose,
                    "{NewLine Timestamp:HH: mm: ss } [{Level}] ({ CorrelationToken }) {Message}{NewLine Exception")
                    .CreateLogger();
                try
                {
                    CreateWebHostBuilder(args)
                        .Build()
                        .Run();
                }
                finally
                {

                    Log.CloseAndFlush();
                }

            }
        }

    }
    
}
