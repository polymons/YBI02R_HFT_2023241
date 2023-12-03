using Microsoft.AspNetCore.Mvc;
using YBI02R_HFT_2023241.Logic.Interfaces;

namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
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


    }
}
