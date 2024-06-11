using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;


namespace PlantCare.Web.Services
{
    public class ApiConnectionService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiConnectionService<T>> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiConnectionService(HttpClient httpClient, ILogger<ApiConnectionService<T>> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true
            };
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

        public async Task<T> GetByIdAsync(string address, int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>($"{address}/{id}");
            }
            catch (Exception ex)
            {
                var responseContent = string.Empty;
                try
                {
                    var response = await _httpClient.GetAsync($"{address}/{id}");
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                catch (Exception innerEx)
                {
                    _logger.LogError(innerEx, "Error occurred while fetching response content for {Address}/{Id}", address, id);
                }

                _logger.LogError(ex, "Error occurred while fetching data by id from {Address}/{Id}. Response content: {ResponseContent}", address, id, responseContent);
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
                throw;
            }
        }

        public async Task PostAsync(string address, T resource)
        {
            try
            {
                var jsonResource = System.Text.Json.JsonSerializer.Serialize(resource, _jsonOptions);
                var content = new StringContent(jsonResource, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync(address, content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while posting data to {Address}", address);
                throw;
            }
        }


        public async Task PutAsync(string address, T resource)
        {
            try
            {
                var jsonResource = System.Text.Json.JsonSerializer.Serialize(resource, _jsonOptions);
                var content = new StringContent(jsonResource, Encoding.UTF8, "application/json");
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

        public async Task<List<T>> GetByPlantIdAsync(string endpoint, int plantId)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?plantId={plantId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<T>>(content);
            return result;
        }
    }
}
