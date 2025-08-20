using SQLite;
using ObligatorioTecnologias.Models;

namespace ObligatorioTecnologias.Data
{
    public class UsuarioDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public UsuarioDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Usuario>().Wait();
        }

        public Task<List<Usuario>> GetUsuariosAsync() => _database.Table<Usuario>().ToListAsync();

        public Task<Usuario?> GetUsuarioByNombreAsync(string nombreUsuario) =>
            _database.Table<Usuario>().FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        public Task<int> SaveUsuarioAsync(Usuario usuario) => _database.InsertAsync(usuario);

        public Task<int> UpdateUsuarioAsync(Usuario usuario) => _database.UpdateAsync(usuario);

        public Task<int> DeleteUsuarioAsync(Usuario usuario) => _database.DeleteAsync(usuario);
    }
}
