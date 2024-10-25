using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Pim_da_Web_001.Controllers
{
    public class ColaboradoresController : Controller
    {
        private readonly HttpClient _httpClient;

        public ColaboradoresController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7013/api/"); // Corrected base address
        }

        // ... other code ...

        // GET: Lista todos os colaboradores
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var resposta = await _httpClient.GetAsync("usuario/Colaboradores/Listar");
                resposta.EnsureSuccessStatusCode(); // Lança exceção se o status code não for sucesso

                var colaboradores = await resposta.Content.ReadFromJsonAsync<List<ColaboradorModel>>();

                if (colaboradores != null)
                {
                    return View(colaboradores);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao carregar colaboradores.  Resposta da API não contém dados válidos.");
                    return View();
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
                return View();
            }
            catch (JsonReaderException ex)
            {
                // Específico para problemas com o JSON
                ModelState.AddModelError(string.Empty, $"Erro ao processar a resposta JSON: {ex.Message}. Verifique se a resposta da API está no formato JSON esperado.");
                return View();
            }
            catch (Exception ex)
            {
                // Tratamento genérico de exceções
                ModelState.AddModelError(string.Empty, $"Erro inesperado: {ex.Message}");
                return View();
            }
        }


        // GET: Obtém detalhes de um colaborador específico
        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var resposta = await _httpClient.GetAsync($"usuario/Colaboradores/Unico?id={id}");
                resposta.EnsureSuccessStatusCode(); // Lança exceção se o status code não for sucesso

                var colaborador = await resposta.Content.ReadFromJsonAsync<ColaboradorModel>();

                if (colaborador != null)
                {
                    return View(colaborador);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao carregar colaborador.  Resposta da API não contém dados válidos.");
                    return View();
                }

            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
                return View();
            }
            catch (JsonReaderException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao processar a resposta JSON: {ex.Message}. Verifique se a resposta da API está no formato JSON esperado.");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro inesperado: {ex.Message}");
                return View();
            }
        }

        // ... other code ...

        // POST: Cria um novo colaborador
        [HttpPost]
        public async Task<IActionResult> Criar(ColaboradorModel colaborador)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("usuario/Colaboradores", colaborador);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar"); // Redirect after successful creation
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao criar colaborador: {response.StatusCode}");
                    return View(colaborador); // Return View with errors
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
                return View(colaborador); // Keep the view data
            }
        }
        // PUT: Atualiza um colaborador específico
        [HttpPut]
        public async Task<IActionResult> Atualizar(int id, ColaboradorModel colaboradorAtualizado)
        {
            try
            {
                if (id != colaboradorAtualizado.Id)
                {
                    ModelState.AddModelError(string.Empty, "O ID do colaborador na solicitação não corresponde ao ID do colaborador a ser atualizado.");
                    return View(colaboradorAtualizado);
                }

                var resposta = await _httpClient.PutAsJsonAsync($"usuario/Colaboradores/{id}", colaboradorAtualizado);
                resposta.EnsureSuccessStatusCode(); // Verifica se a requisição teve sucesso

                return RedirectToAction("Listar"); //Redireciona para a página de lista após a atualização

            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
                return View(colaboradorAtualizado); // Retorna a view com os dados e erros

            }
            catch (JsonException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao processar a resposta JSON: {ex.Message}.");
                return View(colaboradorAtualizado);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro inesperado: {ex.Message}");
                return View(colaboradorAtualizado);
            }

        }


        // DELETE: Exclui um colaborador
        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var resposta = await _httpClient.DeleteAsync($"usuario/Colaboradores/{id}");
                resposta.EnsureSuccessStatusCode(); // Lança exceção em caso de erro
                return RedirectToAction("Listar");
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
                return RedirectToAction("Listar"); // Retorna a página de lista em caso de erro
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro inesperado: {ex.Message}");
                return RedirectToAction("Listar");
            }
        }
    }
}