using System.Linq;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Database;
using YBI02R_HFT_2023241.Repository.Interfaces;
using YBI02R_HFT_2023241.Repository.Repositories.Repo;
using YBI02R_HFT_2023241.Repository.Repositories.ModelRepos;


namespace YBI02R_HFT_2023241.Repository.Repositories.ModelRepos
{
    public class PublisherRepo : Repo<Publisher>, IRepository<Publisher>
    {
        public PublisherRepo(MusicDbContext databaseContext) : base(databaseContext)
        {
        }

        public override Publisher Read(int id)
        {
            return _musicDbContext.Publishers.FirstOrDefault(x => x.StudioID == id);
        }

        public override void Update(Publisher item)
        {
            var entity = Read(item.StudioID);
            _musicDbContext.Entry(entity).CurrentValues.SetValues(item);
            _musicDbContext.SaveChanges();
        }
    }
}
