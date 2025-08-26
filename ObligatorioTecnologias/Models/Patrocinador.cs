using SQLite;

namespace ObligatorioTecnologias.Modelos
{
    public class Patrocinador
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string LogoPath { get; set; } = string.Empty;
    }
}
