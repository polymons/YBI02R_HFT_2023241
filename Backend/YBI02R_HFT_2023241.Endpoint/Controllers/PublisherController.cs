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
    public class PublisherController : ControllerBase
    {

        private readonly IPublisherLogic logic;
        private readonly IHubContext<SignalRHub> hub;

        public PublisherController(IPublisherLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        #region CRUD
        // GET: api/<PublisherController>
        [HttpGet]
        public IEnumerable<Publisher> ReadAll()
        {
            return logic.ReadAll();
        }
        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public Publisher Read(int id)
        {
            return logic.Read(id);
        }
        // POST api/<PublisherController>
        [HttpPost]
        public void Create([FromBody] Publisher value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("PublisherCreated", value);
        }
        // PUT api/<PublisherController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public void Update([FromBody] Publisher id)
        {
            logic.Update(id);
            this.hub.Clients.All.SendAsync("PublisherUpdated", id);
        }
        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var publisherToDelete = logic.Read(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("PublisherDeleted", publisherToDelete);
        }
        #endregion
    }
}