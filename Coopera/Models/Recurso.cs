using System.ComponentModel.DataAnnotations.Schema;

namespace Coopera.Models
{
    public class Recurso
    {
        public int IdRecursos { get; set; }

        public string? Nombre { get; set; }
        
        public int Cantidad { get; set; }
        
        [ForeignKey("Partida")]
        public int PartidaId { get; set; }

        public List<MiniJuego>? MiniJuegos { get; set; }

        public Partida? Partida { get; set; }
    }
}
