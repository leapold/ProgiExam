using Calculation.API.Mapping;
using Calculation.Application.Services;
using Calculation.Application.Services.GetPrice;
using Calculation.Contract.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Calculation.Common.Common;

namespace Calculation.API.Controllers
{
    [ApiController]

    public class PriceController : ControllerBase
    {

        private readonly ILogger<PriceController> _logger;
        private readonly ISender _vehicleMediator;

        IVehicleService _vehicleCalculator;
        public PriceController(ISender vehicleMediator, ILogger<PriceController> logger)
        {
            _logger = logger;
            _vehicleMediator = vehicleMediator;
        }

        [HttpPost(ApiEndpoints.CalculatePrice)]
        public async Task<IActionResult> Price([FromBody] CalculationRequest request, CancellationToken token)
        {
            try
            {
                VehicleType vehicleType;
                if (Enum.TryParse(request.Type, true, out vehicleType))
                {
                    if (request.BasePrice > 0)
                    {
                        var updatedPrice = await _vehicleMediator.Send(new GetVehiclePrice(request.BasePrice, vehicleType));
                        var response = updatedPrice.MapToVehicleResponse();
                        return Ok(response);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Message = "Can not calculate the price when base braci less equal zero." });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Can not calculate the price." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Can not calculate the price." });
            }
        }
    }
}
