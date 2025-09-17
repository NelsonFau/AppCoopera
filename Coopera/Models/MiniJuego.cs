using System.ComponentModel.DataAnnotations.Schema;

namespace Coopera.Models
{
    public class MiniJuego
    {
        public int IdMiniJuego { get; set }
        public string? Nombre { get; set; }

        [ForeignKey("Recursos")]
        public int Recurso { get; set; }
        public Recurso? Recursos { get; set; }

        [ForeignKey("Jugador")]
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        [ForeignKey("Partida")]
        public int IdPartida { get; set; }
        public Partida? Partida { get; set; }
    }
}
//PARA COMENTAR CARDINALIDAD ENTRE RECURSO Y MINIJUEGO??????