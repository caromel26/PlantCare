using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlantCare.Mobile.Services
{
    public class BusinessLogicService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BusinessLogicService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public BusinessLogicService(HttpClient httpClient, ILogger<BusinessLogicService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task WaterPlant(string address, int id)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{address}/water/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully watered plant with id {Id}", id);
                }
                else
                {
                    _logger.LogError("Failed to water plant with id {Id}. Status Code: {StatusCode}", id, response.StatusCode);
                    throw new HttpRequestException($"Failed to water plant. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while watering plant with id {Id}", id);
                throw;
            }
        }
    }
}
