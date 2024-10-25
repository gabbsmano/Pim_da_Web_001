using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;

    public AuthController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _httpClient.GetAsync($"https://localhost:7013/api/usuario/Login?login={model.Login}&senha={model.Senha}");

        // Log the raw response for debugging
        var rawResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(rawResponse); // Log the response

        if (response.IsSuccessStatusCode)
        {
            // O token deve ser extraído da resposta, dependendo do formato da sua API
            // Supondo que a resposta contenha apenas o token como texto
            string token = rawResponse; // Modifique se necessário para extrair o token corretamente

            // Armazenar o token em um local apropriado, como Session ou Cookie
            HttpContext.Session.SetString("AccessToken", token);

            // Retornar sucesso
            return Json(new { success = true, token });
        }

        return Json(new { success = false, message = "Invalid login credentials." });
    }
}
