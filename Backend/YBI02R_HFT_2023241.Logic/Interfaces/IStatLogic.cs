using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Logic.Interfaces
{
    public interface IStatLogic
    {
        int? OldestArtistAge();
        Song LongestSong();

    }
}