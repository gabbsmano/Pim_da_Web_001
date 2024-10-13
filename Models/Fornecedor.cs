namespace Pim_da_Web_001.Models
{
    public class Fornecedor
    {
        public Fornecedor() { }

        public int ID { get; set; }

        // Empresa relacionada ao fornecedor
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; } // Propriedade de navegação para a Empresa

        public string Nome { get; set; }


        public string CNPJ { get; set; }


        public string Telefone { get; set; }


        public string? Email { get; set; }

        // Município relacionado ao fornecedor
        public int MunicipioID { get; set; }
        public Municipio Municipio { get; set; }

        // Status de ativo
        public bool Ativo { get; set; } = true; // Define como ativo por padrão
    }
}
