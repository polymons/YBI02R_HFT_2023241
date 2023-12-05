using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {

        readonly ISongLogic logic;

        public SongController(ISongLogic logic)
        {
            this.logic = logic;
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
        }
        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        //[HttpPut]
        public void Update([FromBody] Song id)
        {
            logic.Update(id);
        }
        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
        #endregion
    }
}