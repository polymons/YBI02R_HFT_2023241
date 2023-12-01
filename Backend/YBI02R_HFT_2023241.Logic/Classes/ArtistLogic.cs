using System.Linq;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Logic
{
    public class ArtistLogic : IArtistLogic
    {
        IRepository<Artist> artistRepo;
        public ArtistLogic(IRepository<Artist> artistRepo)
        {
            this.artistRepo = artistRepo;
        }


        #region CRUD methods
        public void Create(Artist item)
        {
            this.artistRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.artistRepo.Delete(id);
        }

        public Artist Read(int id)
        {
            return this.artistRepo.Read(id);
        }

        public IQueryable<Artist> ReadAll()
        {
            return this.artistRepo.ReadAll();
        }

        public void Update(Artist item)
        {
            this.artistRepo.Update(item);
        }
        #endregion



    }
}
