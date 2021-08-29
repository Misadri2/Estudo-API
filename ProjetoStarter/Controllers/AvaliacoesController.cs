using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoStarter.Data;
using ProjetoStarter.Models;
using ProjetoStarter.HATEOAS;
using System.Collections.Generic;

namespace ProjetoStarter.Controllers
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AvaliacoesController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private HATEOAS.HATEOAS HATEOAS;

        public AvaliacoesController(ApplicationDbContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Avaliacoes");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
            HATEOAS.AddAction("EDIT_PRODUCT","PUT");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var avaliacoes = database.Avaliacoes.ToList();
            List<AvaliacaoContainer> avaliacoesHATEOAS = new List<AvaliacaoContainer>();
            foreach(var aval in avaliacoes){
                AvaliacaoContainer avaliacaoHATEOAS = new AvaliacaoContainer();
                avaliacaoHATEOAS.avaliacao = aval;
                avaliacaoHATEOAS.links = HATEOAS.GetActions(aval.AvaliacaoId.ToString());
                avaliacoesHATEOAS.Add(avaliacaoHATEOAS);     
            }  return Ok(avaliacoesHATEOAS);
        }

        [HttpGet("{id}")] 

        public IActionResult Get(int id)
        {
            try
            {
                var avaliacoes = database.Avaliacoes.First(p => p.AvaliacaoId == id);
                AvaliacaoContainer avaliacaoHATEOAS = new AvaliacaoContainer();
                avaliacaoHATEOAS.avaliacao = avaliacoes;
                avaliacaoHATEOAS.links = HATEOAS.GetActions(avaliacoes.AvaliacaoId.ToString());
                return Ok(avaliacaoHATEOAS);
            }
            catch (Exception)
            {
                return BadRequest(new { msg = "Id inválido" });
            }
        }

        [HttpPost]      
        public ActionResult Post([FromBody]Avaliacao avaliacao)
        {          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            database.Avaliacoes.Add(avaliacao);
            database.SaveChanges();

            return Ok();
        }

        [HttpPatch]              
       public IActionResult Patch([FromBody] Avaliacao avaliacao)
        {
            try
            {
                var s = database.Avaliacoes.First(sTemp => sTemp.AvaliacaoId == avaliacao.AvaliacaoId);

                s.Projeto = avaliacao.Projeto != null ? avaliacao.Projeto : s.Projeto;
                s.Comportamento = avaliacao.Comportamento != null ? avaliacao.Comportamento : s.Comportamento;
                s.Performance = avaliacao.Performance != 0 ? avaliacao.Performance : s.Performance;

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
               var avaliacoes = database.Avaliacoes.First(p => p.AvaliacaoId == id);
                database.Avaliacoes.Remove(avaliacoes);
                database.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { msg = "Id inválido" });
            } 
        }

        public class AvaliacaoTemp  {
            
            public string Projeto { get; set; }
            public float Performance { get; set; }
            public string Comportamento { get; set; }
            
        }

        public class AvaliacaoContainer{
            public Avaliacao avaliacao { get; set;}
            public Link[] links {get; set; }
         }
    }
}

