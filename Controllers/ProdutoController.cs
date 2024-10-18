using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;


public class ProdutoController : Controller
{
    private readonly HttpClient _httpClient;

    public ProdutoController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7013/"); // URL base da sua API
    }
    // GET: Produto/Listar
    public async Task<IActionResult> Listar()
    {
        var response = await _httpClient.GetAsync("api/produto?EmpresaID=1");
        var produtos = JsonConvert.DeserializeObject<List<Produto>>(await response.Content.ReadAsStringAsync());

        return View(produtos);
    }

    // GET: Produto/Detalhes/5
    public async Task<IActionResult> Detalhes(int id)
    {
        var response = await _httpClient.GetAsync($"api/produto/Unico?ProdutoID={id}");
        var produto = JsonConvert.DeserializeObject<Produto>(await response.Content.ReadAsStringAsync());

        return View(produto);
    }

    // GET: Produto/Criar
    public IActionResult Criar()
    {
        return View();
    }

    // POST: Produto/Criar
    [HttpPost]
    public async Task<IActionResult> Criar(Produto model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Serializando o objeto para JSON
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                // Enviando para a API
                var response = await _httpClient.PostAsync("api/produto", content);

                // Verifique se a resposta foi bem-sucedida
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true });
                else
                    return Json(new { success = false, message = "Erro ao criar produto na API: " + response.StatusCode });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Exceção: " + ex.Message });
            }
        }

        return Json(new { success = false, message = "Dados inválidos" });
    }

    // GET: Produto/Editar/5
    public async Task<IActionResult> Editar(int id)
    {
        var response = await _httpClient.GetAsync($"api/produto/Unico?ProdutoID={id}");
        var produto = JsonConvert.DeserializeObject<Produto>(await response.Content.ReadAsStringAsync());

        return View(produto);
    }

    // POST: Produto/Editar/5
    [HttpPost]
    public async Task<IActionResult> Editar(Produto model)
    {
        if (ModelState.IsValid)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/produto", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Listar));
        }

        return View(model);
    }

    // GET: Produto/Excluir/5
    public async Task<IActionResult> Excluir(int id)
    {
        var response = await _httpClient.GetAsync($"api/produto/Unico?ProdutoID={id}");
        var produto = JsonConvert.DeserializeObject<Produto>(await response.Content.ReadAsStringAsync());

        return View(produto);
    }

    // POST: Produto/Excluir/5
    [HttpPost, ActionName("Excluir")]
    public async Task<IActionResult> ConfirmarExclusao(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/produto?ProdutoID={id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Listar));

        return View();
    }


}
