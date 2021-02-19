using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Task1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService <ILogger<Startup>>();
                    await context.Response.WriteAsync("Primes app by Oleksii Safroniuk");
                    logger.LogInformation($"Write information.");
                });
                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var number = int.Parse((string) context.Request.RouteValues["number"]);
                    logger.LogInformation($"Check is {number} prime.");
                    var isPrime = await IsPrime(number);
                    var statusCode = isPrime
                        ? (int)HttpStatusCode.OK
                        : (int)HttpStatusCode.NotFound;
                    context.Response.StatusCode = statusCode;
                    logger.LogInformation($"Status code: {statusCode}");

                });
                endpoints.MapGet("/primes", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var fromString = context.Request.Query["from"].FirstOrDefault();
                    var toString = context.Request.Query["to"].FirstOrDefault();
                    if (int.TryParse(toString, out var to) && int.TryParse(fromString, out var from))
                    {
                        logger.LogInformation($"Get primes in range [{from}, {to}]");
                        context.Response.StatusCode = (int) HttpStatusCode.OK;
                        var primes = await GetPrimesAsync(from, to);
                        await context.Response.WriteAsync("["+string.Join(',', primes)+"]");
                        logger.LogInformation($"Got {primes.Count} prime numbers");
                    }
                    else
                    {
                        logger.LogInformation($"Wrong range.");
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                });
            });
        }

        private async Task<List<int>> GetPrimesAsync(int from, int to)
        {
            var primes = new List<int>();
            for (int i = from; i <= to; i++)
            {
                var isPrime = await IsPrime(i);
                if (isPrime)
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        private Task<bool> IsPrime(int number)
        {
            if (number < 2) return  Task.FromResult(false);

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
