using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PlantCare.Mobile.Services
{
    public class ApiConnectionService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiConnectionService<T>> _logger;

        public ApiConnectionService(HttpClient httpClient, ILogger<ApiConnectionService<T>> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<T> GetAsync(string address)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching data from {Address}", address);
                throw; // Rethrow the exception to handle it in the calling code
            }
        }

        public async Task<List<T>> GetAllAsync(string address)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<T>>(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching data from {Address}", address);
                throw; // Rethrow the exception to handle it in the calling code
            }
        }

        public async Task PostAsync(string address, T resource)
        {
            try
            {
                await _httpClient.PostAsJsonAsync(address, resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while posting data to {Address}", address);
                throw; // Rethrow the exception to handle it in the calling code
            }
        }

        public async Task PutAsync(string address, T resource)
        {
            try
            {
                await _httpClient.PutAsJsonAsync(address, resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while putting data to {Address}", address);
                throw; // Rethrow the exception to handle it in the calling code
            }
        }

        public async Task DeleteAsync(string address)
        {
            try
            {
                await _httpClient.DeleteAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting data from {Address}", address);
                throw; // Rethrow the exception to handle it in the calling code
            }
        }
    }
}
