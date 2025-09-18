using System.ComponentModel.DataAnnotations.Schema;

namespace Coopera.Models
{
    public class MiniJuego
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        [ForeignKey("Recursos")]
        public int IdRecurso { get; set; }
        public Recurso? Recursos { get; set; }

        [ForeignKey("Jugador")]
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }
        public DateTime Tiempo { get; set; }

        [ForeignKey("Partida")]
        public int IdPartida { get; set; }
        public Partida? Partida { get; set; }
    }
}
//PARA COMENTAR CARDINALIDAD ENTRE RECURSO Y MINIJUEGO?????


//metodo factory para instanciar minijuegos
//fachada
//testing
//factory
//stratergy{
//servicio valida preguntas, si el parametro es x, 