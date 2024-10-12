namespace Pim_da_Web_001.Models
{
    public class Fornecedor
    {
        public int ID { get; set; }
        public int EmpresaID { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string? Email { get; set; }
        public int MunicipioID { get; set; }
    }
}
