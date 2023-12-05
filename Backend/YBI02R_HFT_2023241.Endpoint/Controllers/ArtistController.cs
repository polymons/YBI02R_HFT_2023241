using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {

        readonly IArtistLogic logic;

        public ArtistController(IArtistLogic logic)
        {
            this.logic = logic;
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
        }
        // PUT api/<ArtistController>/5
        [HttpPut("{id}")]
        //[HttpPut]
        public void Update([FromBody] Artist id)
        {
            logic.Update(id);
        }
        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
        #endregion
    }
}