using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.Entities.BusinessEntities.Requests;
using System.Text.Json;

namespace Smartwyre.API
{
    public class Rebate
    {

        private readonly ILogger<Rebate> _logger;
        private IRebateService _rebateService;

        public Rebate(ILogger<Rebate> logger, IRebateService rebateService)
        {
            _logger = logger;
            _rebateService = rebateService;
        }
        

        [Function("CalculateRebate")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CalculateRebateRequest? request;
            try
            {
                request = JsonSerializer.Deserialize<CalculateRebateRequest>(requestBody);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("An error has ocurred. Bad Request: " + ex.Message);
            }
            var result = _rebateService.Calculate(request);
            return new OkObjectResult(result);
        }
    }
}
