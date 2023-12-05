using System.Linq;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Logic.Classes
{
    public class ArtistLogic : IArtistLogic
    {
        readonly IRepository<Artist> artistRepo;
        public ArtistLogic(IRepository<Artist> artistRepo)
        {
            this.artistRepo = artistRepo;
        }


        #region CRUD methods
        public void Create(Artist item)
        {
            artistRepo.Create(item);
        }

        public void Delete(int id)
        {
            artistRepo.Delete(id);
        }

        public Artist Read(int id)
        {
            return artistRepo.Read(id);
        }

        public IQueryable<Artist> ReadAll()
        {
            return artistRepo.ReadAll();
        }

        public void Update(Artist item)
        {
            artistRepo.Update(item);
        }
        #endregion



    }
}
