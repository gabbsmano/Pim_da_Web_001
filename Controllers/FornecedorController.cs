using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class FornecedorController : Controller
{
    private readonly HttpClient _httpClient;

    public FornecedorController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7013/"); // URL base da sua API
    }

    // GET: Fornecedores
    public async Task<IActionResult> Index()
    {
        var fornecedores = await ListarFornecedores();
        return View(fornecedores);
    }

    // GET: Lista todos os fornecedores
    [HttpGet]
    public async Task<List<Fornecedor>> ListarFornecedores()
    {
        var response = await _httpClient.GetAsync("api/fornecedor");

        if (response.IsSuccessStatusCode)
        {
            var fornecedores = await response.Content.ReadFromJsonAsync<List<Fornecedor>>();
            return fornecedores;  // Retorna a lista de fornecedores
        }

        return new List<Fornecedor>(); // Retorna uma lista vazia em caso de erro
    }

    // GET: Detalhes de um fornecedor específico
    [HttpGet]
    public async Task<IActionResult> DetalhesFornecedor(int id)
    {
        var response = await _httpClient.GetAsync($"api/fornecedor/{id}");

        if (response.IsSuccessStatusCode)
        {
            var fornecedor = await response.Content.ReadFromJsonAsync<Fornecedor>();
            return View(fornecedor);
        }

        ModelState.AddModelError(string.Empty, "Erro ao carregar detalhes do fornecedor.");
        return View();
    }

    // POST: Cria um novo fornecedor
    [HttpPost]
    public async Task<IActionResult> CriarFornecedor(Fornecedor fornecedor)
    {
        var response = await _httpClient.PostAsJsonAsync("api/fornecedor", fornecedor);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index"); // Redireciona para a listagem
        }

        ModelState.AddModelError(string.Empty, "Erro ao criar fornecedor.");
        return View("Index", await ListarFornecedores()); // Recarrega a lista de fornecedores
    }

    // POST: Atualiza um fornecedor existente
    [HttpPost]
    public async Task<IActionResult> AtualizarFornecedor(int id, Fornecedor fornecedor)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/fornecedor/{id}", fornecedor);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Erro ao atualizar fornecedor.");
        return View(fornecedor);
    }

    // DELETE: Exclui um fornecedor
    [HttpPost]
    public async Task<IActionResult> ExcluirFornecedor(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/fornecedor/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Erro ao excluir fornecedor.");
        return RedirectToAction("Index");
    }
}
