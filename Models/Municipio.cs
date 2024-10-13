namespace Pim_da_Web_001.Models
{
    public class Municipio
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int UF { get; set; }

        public ICollection<Fornecedor> Fornecedores { get; set; }
    }
}
