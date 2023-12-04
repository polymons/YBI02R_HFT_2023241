using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {

        IPublisherLogic logic;

        public PublisherController(IPublisherLogic logic)
        {
            this.logic = logic;
        }

        #region CRUD
        // GET: api/<PublisherController>
        [HttpGet]
        public IEnumerable<Publisher> ReadAll()
        {
            return this.logic.ReadAll();
        }
        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public Publisher Read(int id)
        {
            return this.logic.Read(id);
        }
        // POST api/<PublisherController>
        [HttpPost]
        public void Create([FromBody] Publisher value)
        {
            this.logic.Create(value);
        }
        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        //[HttpPut]
        public void Update([FromBody] Publisher id)
        {
            this.logic.Update(id);
        }
        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
        #endregion
    }
}