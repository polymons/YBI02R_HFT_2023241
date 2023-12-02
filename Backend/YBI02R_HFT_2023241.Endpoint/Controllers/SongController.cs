using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YBI02R_HFT_2023241.Logic;
using YBI02R_HFT_2023241.Models;


namespace YBI02R_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {

        IArtistLogic logic;

        public ArtistController(IArtistLogic logic)
        {
            this.logic = logic;
        }

        #region CRUD
        // GET: api/<ArtistController>
        [HttpGet]
        public IEnumerable<Artist> ReadAll()
        {
            return this.logic.ReadAll();
        }
        // GET api/<ArtistController>/5
        [HttpGet("{id}")]
        public Artist Read(int id)
        {
            return this.logic.Read(id);
        }
        // POST api/<ArtistController>
        [HttpPost]
        public void Create([FromBody] Artist value)
        {
            this.logic.Create(value);
        }
        // PUT api/<ArtistController>/5
        [HttpPut("{id}")]
        //[HttpPut]
        public void Update([FromBody] Artist id)
        {
            this.logic.Update(id);
        }
        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
        #endregion
    }
}