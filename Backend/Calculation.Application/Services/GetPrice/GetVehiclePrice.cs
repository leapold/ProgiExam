using Calculation.Application.Model;
using MediatR;

using static Calculation.Common.Common;

namespace Calculation.Application.Services.GetPrice
{
    public record GetVehiclePrice(decimal basePrice, VehicleType type) : IRequest<Vehicle>;

}
