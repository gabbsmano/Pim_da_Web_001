namespace Pim_da_Web_001.Models
{
    public class Produto
    {
        public Produto() { }
        public int ID { get; set; }
        public Empresa Empresa { get; set; }
        public int EmpresaID { get; set; }
        public string Nome { get; set; }
        public int? ValorVendaKG { get; set; }
        public bool Ativo { get; set; }

        public ICollection<ProdutoFornecedor> ProdutoFornecedor { get; set; }
    }
}
