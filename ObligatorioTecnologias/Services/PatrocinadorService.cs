using SQLite;
using ObligatorioTecnologias.Modelos;

namespace ObligatorioTecnologias.Data
{
    public class PatrocinadorService
    {
        private readonly SQLiteAsyncConnection _db;

        public PatrocinadorService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Patrocinador>().Wait();
        }

        public Task<int> AddPatrocinadorAsync(Patrocinador patrocinador)
        {
            return _db.InsertAsync(patrocinador);
        }

        public Task<List<Patrocinador>> GetPatrocinadoresAsync()
        {
            return _db.Table<Patrocinador>().ToListAsync();
        }

        public Task<Patrocinador?> GetPatrocinadorByIdAsync(int id)
        {
            return _db.Table<Patrocinador>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
