using System.ComponentModel.DataAnnotations.Schema;

namespace Coopera.Models
{
    public class Jugador
    {
        public int IdJugador { get;set }
        public string Nombre { get; set; }
        [ForeignKey("Partida")]
        public int PartidaId { get; set; }
        public Partida Partida { get; set; }
    }
}
