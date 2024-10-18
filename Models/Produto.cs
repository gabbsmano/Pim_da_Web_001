using System.ComponentModel.DataAnnotations;

namespace Pim_da_Web_001.Models;
public class Produto
{
    public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser um número positivo.")]
        public decimal? ValorVendaKG { get; set; }

        [Required(ErrorMessage = "O campo ativo é obrigatório.")]
        public bool Ativo { get; set; }

}
