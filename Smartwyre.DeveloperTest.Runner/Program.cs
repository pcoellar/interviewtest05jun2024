using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DataLayer.DataStores.Interfaces;
using Smartwyre.DataLayer.DataStores;
using Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;
using Smartwyre.DeveloperTest.Services.RebateCalculations;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.BusinessEntities.Responses;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        // Create service collection and configure Dependency Injection
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        Console.Write("Enter RebateIdentifier: ");
        string rebateIdentifier = Console.ReadLine();
        Console.Write("Enter ProductIdentifier: ");
        string productIdentifier = Console.ReadLine();
        Console.Write("Enter Volume: ");
        decimal volume = decimal.Parse(Console.ReadLine());

        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        // Resolve the class that depends on the service
        IRebateService rebateService = serviceProvider.GetRequiredService<IRebateService>();
        CalculateRebateResult result = rebateService.Calculate(request);
        Console.WriteLine($"Result: {result.Success.ToString()}");
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register services
        services.AddScoped<IRebateDataStoreReader, RebateDataStoreReader>();
        services.AddScoped<IProductDataStoreReader, ProductDataStoreReader>();
        services.AddScoped<IRebateDataStoreWriter, RebateDataStoreWriter>();
        services.AddScoped<IRebateCalculator, FixedCashAmountCalculator>();
        services.AddScoped<IRebateCalculator, FixedRateRebateCalculator>();
        services.AddScoped<IRebateCalculator, AmountPerUomCalculator>();
        services.AddScoped<IProductDataStoreReader, ProductDataStoreReader>();
        services.AddScoped<IRebateService, RebateService>();

    }
}
