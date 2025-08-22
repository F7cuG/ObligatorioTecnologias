using SQLite;

namespace ObligatorioTecnologias.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, NotNull]
        public string NombreUsuario { get; set; } = string.Empty;

        [NotNull]
        public string Contraseña { get; set; } = string.Empty;

        [NotNull]
        public string NombreCompleto { get; set; } = string.Empty;

        [NotNull]
        public string Direccion { get; set; } = string.Empty;

        [NotNull]
        public string Telefono { get; set; } = string.Empty;

        [NotNull]
        public string Email { get; set; } = string.Empty;

        
        public string FotoPerfil { get; set; } = string.Empty;
    }
}
