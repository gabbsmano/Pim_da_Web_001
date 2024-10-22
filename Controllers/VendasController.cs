using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

[Route("api/")]
    public class VendasController : Controller { 

    private readonly HttpClient _httpClient;


    public VendasController(HttpClient httpClient)
    {
        
    }


}

