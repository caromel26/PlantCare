using Microsoft.Extensions.Logging;
using PlantCare.Mobile.Models;
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

        public async Task<List<Reminder>> GetRemindersAsync(string address)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{address}/generate-reminders", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var reminders = await JsonSerializer.DeserializeAsync<List<Reminder>>(responseStream, _jsonOptions);
                    _logger.LogInformation("Successfully retrieved reminders");
                    return reminders;
                }
                else
                {
                    _logger.LogError("Failed to retrieve reminders. Status Code: {StatusCode}", response.StatusCode);
                    throw new HttpRequestException($"Failed to retrieve reminders. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving reminders");
                throw;
            }
        }

    }
}
