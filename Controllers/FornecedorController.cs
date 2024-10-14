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

    // GET: Lista todos os fornecedores
    public async Task<IActionResult> Fornecedor()
    {
        var fornecedoresResponse = await _httpClient.GetAsync("/api/fornecedor");
        var empresasResponse = await _httpClient.GetAsync("/api/empresa");
        var municipiosResponse = await _httpClient.GetAsync("/api/municipio");

        if (fornecedoresResponse.IsSuccessStatusCode && empresasResponse.IsSuccessStatusCode && municipiosResponse.IsSuccessStatusCode)
        {
            var fornecedores = await fornecedoresResponse.Content.ReadFromJsonAsync<List<Fornecedor>>();
            var empresas = await empresasResponse.Content.ReadFromJsonAsync<List<Empresa>>();
            var municipios = await municipiosResponse.Content.ReadFromJsonAsync<List<Municipio>>();

            // Usando ViewModel para passar os dados
            var viewModel = new FornecedorViewModel
            {
                Fornecedores = fornecedores ?? new List<Fornecedor>(), // Evita nulo
                Empresas = empresas ?? new List<Empresa>(),           // Evita nulo
                Municipios = municipios ?? new List<Municipio>()      // Evita nulo
            };

            return View(viewModel);
        }

        ModelState.AddModelError(string.Empty, "Erro ao carregar os dados.");
        return View(new FornecedorViewModel());
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
        return RedirectToAction("Index");
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
        return RedirectToAction("Index"); // Retorna à lista
    }

    // POST: Atualiza um fornecedor existente
    [HttpPut]
    public async Task<IActionResult> AtualizarFornecedor(int id, Fornecedor fornecedor)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/fornecedor/{id}", fornecedor);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Erro ao atualizar fornecedor.");
        return RedirectToAction("Index");
    }

    // DELETE: Exclui um fornecedor
    [HttpDelete]
    public async Task<IActionResult> ExcluirFornecedor(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/fornecedor/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Erro ao excluir fornecedor.");
        return RedirectToAction("Index");
    }
}
