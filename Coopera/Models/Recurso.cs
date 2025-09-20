using System.ComponentModel.DataAnnotations.Schema;

namespace Coopera.Models
{
    public class Recurso
    {
        public int Id { get; set; }

        public RecursosPartida Nombre { get; set; }
        public int Cantidad { get; set; } = 0;

        [ForeignKey("Partida")]
        public int PartidaId { get; set; }

        public List<MiniJuego>? MiniJuegos { get; set; }

        public Partida? Partida { get; set; }

        public enum RecursosPartida
        {
            Madera,
            Piedra,
            Comida
        }
    }
}
