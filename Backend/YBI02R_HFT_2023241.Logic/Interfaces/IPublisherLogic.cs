using System.Linq;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Logic.Interfaces
{
    public interface IPublisherLogic
    {
        void Create(Publisher item);
        void Delete(int id);
        Publisher Read(int id);
        IQueryable<Publisher> ReadAll();
        void Update(Publisher item);
    }
}