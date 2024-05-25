using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace GlovoSoft.Integration;

public class CrearUsuarioApiIntegration
{
    private readonly ILogger<CrearUsuarioApiIntegration> _logger;

    private const string API_URL = "https://reqres.in/api/users";

    private readonly HttpClient httpClient;

    public CrearUsuarioApiIntegration(ILogger<CrearUsuarioApiIntegration> logger)
    {
        _logger = logger;
        httpClient = new HttpClient();
    }

    public async Task<ApiResponse> CreateUser(String name, String job)
    {
        string requestUrl = API_URL;
        ApiResponse apiResponse = null;
        try
        {

            var userData = new { name, job };

            var jsonUserData = JsonSerializer.Serialize(userData);

            var requestContent = new StringContent(jsonUserData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, requestContent);

            if (response.IsSuccessStatusCode)
            {

                var respondeBody = await response.Content.ReadAsStringAsync();

                apiResponse= JsonSerializer.Deserialize<ApiResponse>(respondeBody);

            }

            else{

                _logger.LogDebug($"La solicitud POST a la API no se ha podido establecer \n Código de estado: {response.StatusCode}");
            }


        }

        catch (Exception exception){
            _logger.LogDebug($"Error al comento de conectarse con la API: {exception.Message}");
        }

        return apiResponse;
    }


    public class ApiResponse
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
