﻿using Microsoft.AspNetCore.Mvc;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;

namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase, IStatLogic
    {
        IStatLogic stats;

        public StatController(IStatLogic statLogic)
        {
            stats = statLogic;
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

        

    }
}
