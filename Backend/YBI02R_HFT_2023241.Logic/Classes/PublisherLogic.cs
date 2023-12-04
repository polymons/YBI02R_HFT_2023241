using System.Linq;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Logic.Classes
{
    public class PublisherLogic : IPublisherLogic
    {
        IRepository<Publisher> publisherRepo;
        public PublisherLogic(IRepository<Publisher> publisherRepo)
        {
            this.publisherRepo = publisherRepo;
        }


        #region CRUD methods
        public void Create(Publisher item)
        {
            this.publisherRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.publisherRepo.Delete(id);
        }

        public Publisher Read(int id)
        {
            return this.publisherRepo.Read(id);
        }

        public IQueryable<Publisher> ReadAll()
        {
            return this.publisherRepo.ReadAll();
        }

        public void Update(Publisher item)
        {
            this.publisherRepo.Update(item);
        }
        #endregion
    }
}
