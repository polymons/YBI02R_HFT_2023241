using System.Linq;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Logic
{
    public interface IArtistLogic
    {
        void Create(Artist item);
        void Delete(int id);
        Artist Read(int id);
        IQueryable<Artist> ReadAll();
        void Update(Artist item);
    }
}