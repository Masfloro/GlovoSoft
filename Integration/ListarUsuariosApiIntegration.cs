using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlovoSoft.Integration.DTO;

namespace GlovoSoft.Integration;

public class ListarUsuariosApiIntegration
{
    private readonly ILogger<ListarUsuariosApiIntegration> _logger;

    private const string API_URL = "https://reqres.in/api/users";

    private readonly HttpClient httpClient;

    public ListarUsuariosApiIntegration(ILogger<ListarUsuariosApiIntegration> logger)
    {
        _logger = logger;
        httpClient = new HttpClient();
    }

    public async Task<List<Usuario>> ListarUsuarios()
    {

        string requestUrl = API_URL;
        List<Usuario> listado = new List<Usuario>();

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (apiResponse != null)
                {
                    listado = apiResponse.Data ?? new List<Usuario>();
                }
            }
        }

        catch (Exception ex)
        {
            _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
        }

        Console.Write("asdasd");

        return listado;

    }


    class ApiResponse
    {
        public List<Usuario> Data { get; set; }
    }
}

