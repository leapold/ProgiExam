using Calculation_Tool.Models;

namespace Calculation_Tool.Service
{
    public interface IApiService
    {
        Task<VehicleModel> PostDataAsync(string url, VehicleModel model);
    }
}