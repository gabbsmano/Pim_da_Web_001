using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EstoqueController : Controller
{
    private readonly HttpClient _httpClient;

    public EstoqueController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7013/"); // URL base da sua API
    }

    // GET: Exibe a lista de produtos
    public async Task<IActionResult> Estoque()
    {
        var produtos = await ObterProdutos(); // Obtem produtos
        return View("Estoque", produtos); // Renderiza a view Estoque com a lista de produtos
    }

    private async Task<List<Produto>> ObterProdutos()
    {
        var response = await _httpClient.GetAsync("/api/produto");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Produto>>();
        }

        ModelState.AddModelError(string.Empty, "Erro ao carregar produtos.");
        return new List<Produto>(); // Retorna uma lista vazia em caso de erro
    }

    // GET: Obtém detalhes de um produto específico
    [HttpGet]
    public async Task<IActionResult> DetalhesProduto(int id)
    {
        var response = await _httpClient.GetAsync($"/api/produto/Unico?id={id}");

        if (response.IsSuccessStatusCode)
        {
            var produto = await response.Content.ReadFromJsonAsync<Produto>();
            return View(produto);
        }

        ModelState.AddModelError(string.Empty, "Erro ao carregar detalhes do produto.");
        return View(); // Retorna uma view vazia em caso de erro
    }


    
    private async Task<List<Fornecedor>> ObterFornecedores()
    {
        var response = await _httpClient.GetAsync("/api/fornecedor");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Fornecedor>>();
        }
        return new List<Fornecedor>(); // Retorna uma lista vazia em caso de erro
    }


    
    [HttpPost]
    public async Task<IActionResult> CriarProduto(Produto produto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/produto", produto);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Estoque","Home");
        }

        ModelState.AddModelError(string.Empty, "Erro ao criar produto.");
        return RedirectToAction("Estoque","Home"); // Redireciona para Estoque em caso de erro
    }

    
    [HttpPut]
    public async Task<IActionResult> AtualizarProduto(int id, Produto produto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/produto/{id}", produto);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Estoque");
        }

        ModelState.AddModelError(string.Empty, "Erro ao atualizar produto.");
        return RedirectToAction("Estoque"); // Redireciona para Estoque em caso de erro
    }

    // DELETE: Exclui um produto
    [HttpDelete]
    public async Task<IActionResult> ExcluirProduto(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/produto/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Estoque");
        }

        ModelState.AddModelError(string.Empty, "Erro ao excluir produto.");
        return RedirectToAction("Estoque"); // Redireciona para Estoque em caso de erro
    }
}
