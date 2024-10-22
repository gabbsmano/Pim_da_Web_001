public class UsuarioInput
{
    public int ID { get; set; }
    public int EmpresaID { get; set; }
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public int Funcao { get; set; }
    public Guid? Acesso { get; set; }
    public bool Ativo { get; set; }
}
