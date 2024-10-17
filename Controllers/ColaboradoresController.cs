using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ColaboradoresController : Controller
{
    private readonly HttpClient _httpClient;

    public ColaboradoresController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7013/swagger/index.html"); // URL base da sua API
    }

    // GET: Lista todos os colaboradores
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var response = await _httpClient.GetAsync("/api/usuario/Colaboradores/Listar");

        if (response.IsSuccessStatusCode)
        {
            var colaboradores = await response.Content.ReadFromJsonAsync<List<ColaboradorModel>>();
            return View(colaboradores);  // Exibe a lista de colaboradores
        }

        ModelState.AddModelError(string.Empty, "Erro ao carregar colaboradores.");
        return View();
    }

    // GET: Obtém detalhes de um colaborador específico
    [HttpGet]
    public async Task<IActionResult> Detalhes(int id)
    {
        var response = await _httpClient.GetAsync($"/api/usuario/Colaboradores/Unico?id={id}");

        if (response.IsSuccessStatusCode)
        {
            var colaborador = await response.Content.ReadFromJsonAsync<ColaboradorModel>();
            return View(colaborador);
        }

        ModelState.AddModelError(string.Empty, "Erro ao carregar detalhes do colaborador."); 
        return View();
    }

    // POST: Cria um novo colaborador
    [HttpPost]
    public async Task<IActionResult> Criar(ColaboradorModel colaborador)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/usuario/Colaboradores", colaborador);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Listar");

        ModelState.AddModelError(string.Empty, "Erro ao criar colaborador.");
        return View(colaborador);
    }

    // PUT: Atualiza um colaborador existente
    [HttpPost]  // Em ASP.NET MVC o PUT pode ser feito via POST com um campo oculto "_method" como "PUT"
    public async Task<IActionResult> Atualizar(int id, ColaboradorModel colaborador)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/usuario/Colaboradores/{id}", colaborador);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Listar");

        ModelState.AddModelError(string.Empty, "Erro ao atualizar colaborador.");
        return View(colaborador);
    }

    // DELETE: Exclui um colaborador
    [HttpPost]
    public async Task<IActionResult> Excluir(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/usuario/Colaboradores/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Listar");

        ModelState.AddModelError(string.Empty, "Erro ao excluir colaborador.");
        return RedirectToAction("Listar");
    }
}
