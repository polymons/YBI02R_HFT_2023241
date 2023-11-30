using System.Linq;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Logic.Interfaces
{
    public interface ISongLogic
    {
        void Create(Song item);
        void Delete(int id);
        Song Read(int id);
        IQueryable<Song> ReadAll();
        void Update(Song item);
    }
}