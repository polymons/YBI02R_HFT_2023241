using System.Linq;
using YBI02R_HFT_2023241.Repository.Database;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Repository.Repositories.Repo
{

    public abstract class Repo<T> : IRepository<T> where T : class
    {
        protected MusicDbContext _musicDbContext; //protected!

        public Repo(MusicDbContext databaseContext)
        {
            _musicDbContext = databaseContext;
        }

        public void Create(T item)
        {
            _musicDbContext.Set<T>().Add(item);
            _musicDbContext.SaveChanges();
        }

        #region These will be implemented in the ModelRepos
        public abstract T Read(int id);

        public abstract void Update(T item);
        #endregion

        public void Delete(int id)
        {
            _musicDbContext.Set<T>().Remove(Read(id));
            _musicDbContext.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            IQueryable<T> all = _musicDbContext.Set<T>();
            return all;
        }

    }
}
