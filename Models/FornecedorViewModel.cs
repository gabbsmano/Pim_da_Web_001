namespace Pim_da_Web_001.Models
{
    public class FornecedorViewModel
    {
        public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
        public List<Empresa> Empresas { get; set; } = new List<Empresa>();
        public List<Municipio> Municipios { get; set; } = new List<Municipio>();

    }
}
