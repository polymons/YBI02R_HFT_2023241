﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YBI02R_HFT_2023241.Endpoint.Services;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase, IStatLogic
    {
        private readonly IStatLogic stats;
        private readonly IHubContext<SignalRHub> hub;

        public StatController(IStatLogic statLogic, IHubContext<SignalRHub> hub)
        {
            stats = statLogic;
            this.hub = hub;
        }

        [HttpGet]
        public int? OldestArtistAge()
        {
            return stats.OldestArtistAge();
        }

        [HttpGet]
        public Song LongestSong()
        {
            return stats.LongestSong();
        }

        [HttpGet]
        public Artist ArtistWithMostSongs()
        {
            return stats.ArtistWithMostSongs();
        }

        [HttpGet]
        public double? AvgSongLengthForArtist(string artistName)
        {
            return stats.AvgSongLengthForArtist(artistName);
        }

        [HttpGet]
        public Artist MostPopularArtist()
        {
            return stats.MostPopularArtist();
        }

        [HttpGet]
        public Song MostPopularSongOfArtist(string artistName)
        {
            return stats.MostPopularSongOfArtist(artistName);
        }

        [HttpGet]
        public double? MinutesListenedToPublisher(string publisherName)
        {
            return stats.MinutesListenedToPublisher(publisherName);
        }

        [HttpGet]
        public string ArtistHomeCity(string artistName)
        {
            return stats.ArtistHomeCity(artistName);
        }
    }
}
