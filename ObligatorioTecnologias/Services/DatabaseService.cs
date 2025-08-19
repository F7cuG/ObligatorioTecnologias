using SQLite;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Usuario>().Wait();
    }

    public Task<List<Usuario>> GetUsuariosAsync()
    {
        return _database.Table<Usuario>().ToListAsync();
    }

    public Task<int> SaveUsuarioAsync(Usuario usuario)
    {
        if (usuario.Id != 0)
            return _database.UpdateAsync(usuario);
        else
            return _database.InsertAsync(usuario);
    }

    public Task<int> DeleteUsuarioAsync(Usuario usuario)
    {
        return _database.DeleteAsync(usuario);
    }
}
