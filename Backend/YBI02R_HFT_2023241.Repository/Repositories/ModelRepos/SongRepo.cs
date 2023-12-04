using System.Linq;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Database;
using YBI02R_HFT_2023241.Repository.Interfaces;
using YBI02R_HFT_2023241.Repository.Repositories.Repo;


namespace YBI02R_HFT_2023241.Repository.Repositories.ModelRepos
{
    public class SongRepo : Repo<Song>, IRepository<Song>
    {
        public SongRepo(MusicDbContext databaseContext) : base(databaseContext)
        {
        }

        public override Song Read(int id)
        {
            return _musicDbContext.Songs.FirstOrDefault(x => x.SongID == id);
        }

        public override void Update(Song item)
        {
            var entity = Read(item.SongID);
            _musicDbContext.Entry(entity).CurrentValues.SetValues(item);
            _musicDbContext.SaveChanges();
        }
    }
}
