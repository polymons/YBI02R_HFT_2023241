using System.Collections.Generic;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Logic.Interfaces
{
    public interface IStatLogic
    {
        int? OldestArtistAge();
        Song LongestSong();
        Artist ArtistWithMostSongs();
        double? AvgSongLengthForArtist(string artistName);
        Artist MostPopularArtist(); //gets the artist with most plays
        Song MostPopularSongOfArtist(string artistName);
        double? MinutesListenedToPublisher(string publisherName); //gets the publisher's artist(s) and returns with the artists songs plays * songlengths /60
    }
}