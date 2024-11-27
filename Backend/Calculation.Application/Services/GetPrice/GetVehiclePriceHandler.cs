using Calculation.Application.Model;
using MediatR;

namespace Calculation.Application.Services.GetPrice
{
    public class GetVehiclePriceHandler : IRequestHandler<GetVehiclePrice, Vehicle>
    {
        private IVehicleService _vehicleService;
        public GetVehiclePriceHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public Task<Vehicle> Handle(GetVehiclePrice request, CancellationToken cancellationToken)
        {
            return _vehicleService.CalculateFees(request.basePrice, request.type);
        }
    }
}
