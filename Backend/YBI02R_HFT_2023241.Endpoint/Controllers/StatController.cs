using Microsoft.AspNetCore.Mvc;
using YBI02R_HFT_2023241.Logic.Interfaces;

namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController :ControllerBase
    {
        ISongLogic logic;

        public StatController(ISongLogic logic)
        {
            this.logic = logic;
        }
    }
}
