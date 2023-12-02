using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Logic;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {

        ISongLogic logic;

        public SongController(ISongLogic logic)
        {
            this.logic = logic;
        }

        #region CRUD
        // GET: api/<SongController>
        [HttpGet]
        public IEnumerable<Song> ReadAll()
        {
            return this.logic.ReadAll();
        }
        // GET api/<SongController>/5
        [HttpGet("{id}")]
        public Song Read(int id)
        {
            return this.logic.Read(id);
        }
        // POST api/<SongController>
        [HttpPost]
        public void Create([FromBody] Song value)
        {
            this.logic.Create(value);
        }
        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        //[HttpPut]
        public void Update([FromBody] Song id)
        {
            this.logic.Update(id);
        }
        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
        #endregion
    }
}