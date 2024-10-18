namespace Pim_da_Web_001.Models
{
    public class Cliente
    {
        public Cliente() { }
        public int ID { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Cep { get; set; }
        public int MunicipioID { get; set; }
        public Municipio Municipio { get; set; }
    }
}
