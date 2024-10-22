
public class UsuarioModel
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public bool Ativo { get; set; }
    public string Telefone { get; set; }
}

public class ColaboradorModel : UsuarioModel
{
    public int Funcao { get; set; }
    public Guid? Acesso { get; set; }
}


