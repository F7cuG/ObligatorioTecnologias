using ObligatorioTecnologias.Models;
using System.Threading.Tasks;
using System.IO;
using System;
using ObligatorioTecnologias.Data;

namespace ObligatorioTecnologias.Services
{
    public class UsuarioService
    {
        private static UsuarioDatabase? _database;
        public static UsuarioDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "usuarios.db3");
                    _database = new UsuarioDatabase(dbPath);
                }
                return _database;
            }
        }

        public static Task<List<Usuario>> GetUsuariosAsync() => Database.GetUsuariosAsync();
        public static Task<Usuario?> GetUsuarioByNombreAsync(string nombreUsuario) => Database.GetUsuarioByNombreAsync(nombreUsuario);
        public static Task<int> SaveUsuarioAsync(Usuario usuario) => Database.SaveUsuarioAsync(usuario);
        public static Task<int> UpdateUsuarioAsync(Usuario usuario) => Database.UpdateUsuarioAsync(usuario);
        public static Task<int> DeleteUsuarioAsync(Usuario usuario) => Database.DeleteUsuarioAsync(usuario);
    }
}
