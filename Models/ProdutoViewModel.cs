using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pim_da_Web_001.Models;
public class ProdutoViewModel
{
    public int ID { get; set; }
    [Required]
    [DisplayName("Produto Nome")]
    public Empresa Empresa { get; set; }
    public int EmpresaID { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public int? ValorVendaKG { get; set; }
    [Required]
    public bool Ativo { get; set; }

}
