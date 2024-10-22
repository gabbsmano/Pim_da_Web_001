using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Pim_da_Web_001 { 

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
        public IActionResult Index()
        {
            List<ProdutoViewModel> list = new List<ProdutoViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/produto/Get").Result;

            if (response.IsSuccessStatusCode) 
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(data);
            }

            return View(list);
        }
    }
}