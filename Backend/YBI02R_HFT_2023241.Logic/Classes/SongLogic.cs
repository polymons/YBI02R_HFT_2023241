using System.Linq;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Logic.Classes
{
    public class SongLogic : ISongLogic
    {
        IRepository<Song> songRepo;
        public SongLogic(IRepository<Song> songRepo)
        {
            this.songRepo = songRepo;
        }


        #region CRUD methods
        public void Create(Song item)
        {
            this.songRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.songRepo.Delete(id);
        }

        public Song Read(int id)
        {
            return this.songRepo.Read(id);
        }

        public IQueryable<Song> ReadAll()
        {
            return this.songRepo.ReadAll();
        }

        public void Update(Song item)
        {
            this.songRepo.Update(item);
        }
        #endregion
    }
}
