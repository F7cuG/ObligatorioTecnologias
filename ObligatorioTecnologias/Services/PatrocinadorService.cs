using ObligatorioTecnologias.Models;
using SQLite;

namespace ObligatorioTecnologias.Services
{
    public static class PatrocinadorService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "obligatorio.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Patrocinador>();
        }

        public static async Task SavePatrocinadorAsync(Patrocinador patrocinador)
        {
            await Init();
            await db.InsertAsync(patrocinador);
        }

        public static async Task<List<Patrocinador>> GetPatrocinadoresAsync()
        {
            await Init();
            return await db.Table<Patrocinador>().ToListAsync();
        }
    }
}

