namespace Pim_da_Web_001.Models
{
    public class ColaboradorModel
    {
        public int Id { get; set; }
        public int Funcao { get; set; }
        public UsuarioModel Usuario { get; set; }
        public int UsuarioID { get; set; }
    }

    public class UsuarioModel
    {
        public int Id { get; set; }
        public EmpresaModel Empresa { get; set; }
        public int EmpresaID { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public Guid Acesso { get; set; }
        public bool Ativo { get; set; }
        public bool SerColaborador { get; set; }
    }

    public class EmpresaModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int TipoEmpresa { get; set; }
        public bool PossuiFilial { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public int MunicipioID { get; set; }
        public MunicipioModel Municipio { get; set; }
    }

    public class MunicipioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ufid { get; set; }
        public UfModel Uf { get; set; }
    }

    public class UfModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }

}
