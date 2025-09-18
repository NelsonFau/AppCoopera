using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coopera.Models
{
    public class Jugador
    {
        [Key]
        public int Id{ get; set; }
        public string? Nombre { get; set; }
        [ForeignKey("Partida")]
        public int PartidaId { get; set; }
        public Partida? Partida { get; set; }
        public List<MiniJuego>? MiniJuegos { get; set; }
    }
}
