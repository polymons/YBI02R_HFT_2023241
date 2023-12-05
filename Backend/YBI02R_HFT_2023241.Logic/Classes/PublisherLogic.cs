using System.Linq;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Logic.Classes
{
    public class PublisherLogic : IPublisherLogic
    {
        readonly IRepository<Publisher> publisherRepo;
        public PublisherLogic(IRepository<Publisher> publisherRepo)
        {
            this.publisherRepo = publisherRepo;
        }


        #region CRUD methods
        public void Create(Publisher item)
        {
            publisherRepo.Create(item);
        }

        public void Delete(int id)
        {
            publisherRepo.Delete(id);
        }

        public Publisher Read(int id)
        {
            return publisherRepo.Read(id);
        }

        public IQueryable<Publisher> ReadAll()
        {
            return publisherRepo.ReadAll();
        }

        public void Update(Publisher item)
        {
            publisherRepo.Update(item);
        }
        #endregion
    }
}
