using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Endpoint.Services;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        //Dependency injection for the ArtistController to access the IArtistLogic and IHubContext implementations.
        private readonly IArtistLogic logic;
        private readonly IHubContext<SignalRHub> hub;

        public ArtistController(IArtistLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        #region CRUD
        // GET: api/<ArtistController>
        [HttpGet]
        public IEnumerable<Artist> ReadAll()
        {
            return logic.ReadAll();
        }
        // GET api/<ArtistController>/5
        [HttpGet("{id}")]
        public Artist Read(int id)
        {
            return logic.Read(id);
        }
        // POST api/<ArtistController>
        [HttpPost]
        public void Create([FromBody] Artist value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("ArtistCreated", value);
        }
        // PUT api/<ArtistController>/5
        [HttpPut("{id}")]
        //[HttpPut]
        public void Update([FromBody] Artist id)
        {
            logic.Update(id);
            this.hub.Clients.All.SendAsync("ArtistUpdated", id);
        }
        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var artistToDelete = logic.Read(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("ArtistDeleted", artistToDelete);
        }
        #endregion
    }
}