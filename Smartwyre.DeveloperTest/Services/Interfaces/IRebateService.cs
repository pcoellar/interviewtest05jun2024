using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.BusinessEntities.Responses;

namespace Smartwyre.DeveloperTest.Services.Interfaces;

public interface IRebateService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
