using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoStarter.Data;
using ProjetoStarter.Models;

namespace ProjetoStarter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class StartersController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public StartersController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var starters = database.Starters.ToList();
            return Ok(starters);
        }

        [HttpGet("{id}")]              
        public IActionResult Get(int id)
        {
            try
            {
                var starter = database.Starters.First(p => p.StarterId == id);
                return Ok(starter);
            }
            catch (Exception)
            {
                return BadRequest(new { msg = "Id inválido" });
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] StarterTemp sTemp)
        {
            //validação//
            if (sTemp.NomeStarter.Length <= 1)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Nome precisa ter mais de 1 caracter" });
            }
            if (sTemp.Linguagem.Length <= 1)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Nome precisa ter mais de 1 caracter" });
            }
            /**********/

            Starter s = new Starter();
            s.NomeStarter = sTemp.NomeStarter;
            s.Linguagem = sTemp.Linguagem;
            database.Starters.Add(s);
            database.SaveChanges();
            return Ok(new { msg = "Você criou um novo registro" });
        }

        [HttpPatch]              
        public IActionResult Patch([FromBody] Starter starter)
        {
            try
            {
                var s = database.Starters.First(sTemp => sTemp.StarterId == starter.StarterId);

                s.NomeStarter = starter.NomeStarter != null ? starter.NomeStarter : s.NomeStarter;
                s.Linguagem = starter.Linguagem != null ? starter.Linguagem : s.Linguagem;

                database.SaveChanges();
                return Ok();

            }
            catch
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Id do produto é inválido" });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var starter = database.Starters.First(p => p.StarterId == id);
                database.Starters.Remove(starter);
                database.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { msg = "Id inválido" });
            }
        }

        public class StarterTemp
        {
            public string NomeStarter { get; set; }
            public string Linguagem { get; set; }

        }       
    }
}

