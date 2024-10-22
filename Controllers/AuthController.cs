using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;

    public AuthController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7013/"); // URL base da sua API
    }

    public IActionResult Login()
    {
        return View("~/Views/Home/Login.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> Login(string login, string senha)
    {
        var userData = new
        {
            Login = login,
            Senha = senha
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(userData), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/usuario/Colaboradores/Login", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            // Store authentication token or relevant data
            return RedirectToAction("~/ Views / Home / Index.cshtml");
        }

        ViewBag.Error = "Login falhou, verifique suas credenciais.";
        return View("~/Views/Home/Login.cshtml");
    }
}
