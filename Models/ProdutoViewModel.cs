using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pim_da_Web_001.Models;
public class ProdutoViewModel
{
    public string Nome { get; set; }
    public decimal ValorVendaKG { get; set; }
    public bool Ativo { get; set; }
    public bool NovoProduto { get; set; }
    public Empresa Empresa { get; set; } 
}

