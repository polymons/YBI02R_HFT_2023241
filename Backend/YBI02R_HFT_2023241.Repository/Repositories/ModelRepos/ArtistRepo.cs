using System.Linq;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Database;
using YBI02R_HFT_2023241.Repository.Interfaces;
using YBI02R_HFT_2023241.Repository.Repositories.Repo;


namespace YBI02R_HFT_2023241.Repository.Repositories.ModelRepos
{
    public class ArtistRepo : Repo<Artist>, IRepository<Artist>
    {
        public ArtistRepo(MusicDbContext databaseContext) : base(databaseContext)
        {
        }

        public override Artist Read(int id)
        {
            return _musicDbContext.Artists.FirstOrDefault(x => x.ArtistID == id);
        }

        public override void Update(Artist item)
        {
            var entity = Read(item.ArtistID);
            _musicDbContext.Entry(entity).CurrentValues.SetValues(item);
            _musicDbContext.SaveChanges();
        }
    }
}
