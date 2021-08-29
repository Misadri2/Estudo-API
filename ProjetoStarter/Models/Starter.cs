using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoStarter.Models
{
    public class Starter
    {
        [Key]
        public int StarterId { get; set; }
        public string NomeStarter { get; set; }
        public string Linguagem { get; set; }
        public ICollection<Avaliacao> Avaliacoes {get; set;} = new List<Avaliacao>();
    }
}