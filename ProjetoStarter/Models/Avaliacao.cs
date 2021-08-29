namespace ProjetoStarter.Models
{
    public class Avaliacao
    {
        public int AvaliacaoId { get; set; }
        public string Projeto { get; set; }
        public float Performance { get; set; }
        public string Comportamento { get; set; }
        public Starter Starter {get; set; }
        public int StarterId { get; set; }

    }
}