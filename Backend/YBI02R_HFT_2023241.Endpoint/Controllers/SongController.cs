using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Endpoint.Services;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {

        private readonly ISongLogic logic;
        private readonly IHubContext<SignalRHub> hub;

        public SongController(ISongLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        #region CRUD
        // GET: api/<SongController>
        [HttpGet]
        public IEnumerable<Song> ReadAll()
        {
            return logic.ReadAll();
        }
        // GET api/<SongController>/5
        [HttpGet("{id}")]
        public Song Read(int id)
        {
            return logic.Read(id);
        }
        // POST api/<SongController>
        [HttpPost]
        public void Create([FromBody] Song value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("SongCreated", value);
        }
        // PUT api/<SongController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public void Update([FromBody] Song id)
        {
            logic.Update(id);
            this.hub.Clients.All.SendAsync("SongUpdated", id);
        }
        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var songToDelete = logic.Read(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("SongDeleted", songToDelete);
        }
        #endregion
    }
}