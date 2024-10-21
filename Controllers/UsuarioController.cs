// Controllers/UsuarioController.cs
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pim_da_Web_001.Models;

public class UsuarioController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _client;

    public UsuarioController(IHttpClientFactory clientFactory, HttpClient httpClient)
    {
        _client = clientFactory.CreateClient();
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7013/");
    }

    // GET: Listar Clientes
    public async Task<IActionResult> ListarClientes()
    {
        var response = await _client.GetAsync("https://localhost:7013/api/usuario/Cliente/Listar");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(json);
            return View(clientes);
        }
        return View(new List<ClienteModel>());
    }

    // GET: Listar Colaboradores
    public async Task<IActionResult> ListarColaboradores()
    {
        var response = await _client.GetAsync("https://localhost:7013/api/usuario/Colaboradores/Listar");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var colaboradores = JsonConvert.DeserializeObject<List<ColaboradorModel>>(json);
            return View(colaboradores);
        }
        return View(new List<ColaboradorModel>());
    }

    // POST: Criar Cliente
    [HttpPost]
    public async Task<IActionResult> CriarCliente(ClienteModel cliente)
    {
        var json = JsonConvert.SerializeObject(cliente);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://localhost:7013/api/usuario/Cliente", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ListarClientes");

        ViewBag.Erro = "Erro ao criar cliente";
        return View(cliente);
    }

    // POST: Criar Colaborador
    [HttpPost]
    public async Task<IActionResult> CriarColaborador(ColaboradorModel colaborador)
    {
        var json = JsonConvert.SerializeObject(colaborador);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://localhost:7013/api/usuario/Colaboradores", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ListarColaboradores");

        ViewBag.Erro = "Erro ao criar colaborador";
        return View(colaborador);
    }

    // PUT: Editar Cliente
    [HttpPost]
    public async Task<IActionResult> EditarCliente(ClienteModel cliente)
    {
        var json = JsonConvert.SerializeObject(cliente);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"https://localhost:7013/api/usuario/Cliente/{cliente.ID}", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ListarClientes");

        ViewBag.Erro = "Erro ao editar cliente";
        return View(cliente);
    }

    // PUT: Editar Colaborador
    [HttpPost]
    public async Task<IActionResult> EditarColaborador(ColaboradorModel colaborador)
    {
        var json = JsonConvert.SerializeObject(colaborador);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"https://localhost:7013/api/usuario/Colaboradores/{colaborador.ID}", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ListarColaboradores");

        ViewBag.Erro = "Erro ao editar colaborador";
        return View(colaborador);
    }

    // DELETE: Apagar Cliente
    public async Task<IActionResult> ApagarCliente(int id)
    {
        var response = await _client.DeleteAsync($"https://localhost:7013/api/usuario/Cliente/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ListarClientes");

        ViewBag.Erro = "Erro ao apagar cliente";
        return RedirectToAction("ListarClientes");
    }

    // DELETE: Apagar Colaborador
    public async Task<IActionResult> ApagarColaborador(int id)
    {
        var response = await _client.DeleteAsync($"https://localhost:7013/api/usuario/Colaboradores/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ListarColaboradores");

        ViewBag.Erro = "Erro ao apagar colaborador";
        return RedirectToAction("ListarColaboradores");
    }

    [HttpPost]
    public IActionResult Autenticar([FromBody] UsuarioInput usuarioInput)
    {
        if (string.IsNullOrWhiteSpace(usuarioInput.Login) || string.IsNullOrWhiteSpace(usuarioInput.Senha))
        {
            return BadRequest("Login ou senha não podem ser vazios.");
        }

        // Chamada à API para autenticar
        var response = _httpClient.PostAsync("/api/usuario/autenticar", new StringContent(JsonConvert.SerializeObject(usuarioInput), Encoding.UTF8, "application/json")).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var usuarioAutenticado = JsonConvert.DeserializeObject<UsuarioAutenticadoDto>(responseBody);

            // Verifica o tipo de usuário e retorna a resposta correta
            if (usuarioAutenticado.SerColaborador)
            {
                return Ok(new { tipoUsuario = "colaborador" });
            }
            else
            {
                return Ok(new { tipoUsuario = "cliente" });
            }
        }

        return BadRequest("Login ou senha incorretos.");
    }

}
