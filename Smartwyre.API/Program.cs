using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DataLayer.DataStores.Interfaces;
using Smartwyre.DataLayer.DataStores;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Interfaces;
using Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;
using Smartwyre.DeveloperTest.Services.RebateCalculations;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IRebateDataStoreReader, RebateDataStoreReader>();
        services.AddScoped<IProductDataStoreReader, ProductDataStoreReader>();
        services.AddScoped<IRebateDataStoreWriter, RebateDataStoreWriter>();
        services.AddScoped<IRebateCalculator, FixedCashAmountCalculator>();
        services.AddScoped<IRebateCalculator, FixedRateRebateCalculator>();
        services.AddScoped<IRebateCalculator, AmountPerUomCalculator>();
        services.AddScoped<IProductDataStoreReader, ProductDataStoreReader>();
        services.AddScoped<IRebateService, RebateService>();
    })
    .Build();

host.Run();
