namespace Pim_da_Web_001.Models
{
    public class EstoqueViewModel
    {
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public IEnumerable<Empresa> Empresas { get; set; }
    }

}
