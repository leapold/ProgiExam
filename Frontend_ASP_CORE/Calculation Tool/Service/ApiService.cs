using Calculation_Tool.Controllers;
using Calculation_Tool.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Calculation_Tool.Service
{
    public class ApiService : IApiService
    {
     
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public ApiService(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
      
        public async Task<VehicleModel> PostDataAsync(string url, VehicleModel model)
        {
            VehicleModel? responceModel=null;
            var jsonContent = JsonConvert.SerializeObject(model);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    responceModel = JsonConvert.DeserializeObject<VehicleModel>(apiResponse);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return responceModel;
        }
    }
}

