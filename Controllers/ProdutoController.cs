using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Net.Http.Headers;

namespace Pim_da_Web_001
{

    public class ProdutoController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7013/api");
        private readonly HttpClient _client;

        public ProdutoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAdress;
        }

        [HttpGet]
        public IActionResult Estoque()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            List<Empresa> empresas = new List<Empresa>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/produto").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(data);
            }

            // Aqui você deve buscar a lista de empresas da API
            HttpResponseMessage empresaResponse = _client.GetAsync(_client.BaseAddress + "/empresa").Result;

            if (empresaResponse.IsSuccessStatusCode)
            {
                string empresaData = empresaResponse.Content.ReadAsStringAsync().Result;
                empresas = JsonConvert.DeserializeObject<List<Empresa>>(empresaData);
            }

            var viewModel = new EstoqueViewModel
            {
                Produtos = produtos,
                Empresas = empresas
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Post(ProdutoViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/produto", content).Result; // Corrigido aqui

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Produto criado.";
                    return RedirectToAction("Estoque");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Estoque"); // Redireciona para a lista de produtos em caso de erro
        }
    }
}
