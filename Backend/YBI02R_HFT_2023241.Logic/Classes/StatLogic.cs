using System.Linq;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Interfaces;

namespace YBI02R_HFT_2023241.Logic.Classes
{
    public class StatLogic : IStatLogic
    {
        IRepository<Song> songRepo;
        IRepository<Artist> artistRepo;
        IRepository<Publisher> publisherRepo;

        public StatLogic(IRepository<Song> songRepo, IRepository<Artist> artistRepo, IRepository<Publisher> publisherRepo)
        {
            this.songRepo = songRepo;
            this.artistRepo = artistRepo;
            this.publisherRepo = publisherRepo;
        }

        public Song LongestSong()
        {
            return artistRepo.ReadAll().SelectMany(x => x.Songs).OrderByDescending(x => x.Length).FirstOrDefault();
        }

        public int? OldestArtistAge()
        {
            return this.artistRepo.ReadAll().Max(x => x.Age);
        }



    }
}
