namespace Coopera.Models
{
    public class Partida
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public EstadoPartida Estado { get; set; }
        public List<Recurso>? Recursos { get; set; }
        public List<Jugador>? Jugadores { get; set; }

        public List<MiniJuego>? MiniJuegos { get; set; }

        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }
        
        public enum EstadoPartida
        {
            presentadoResultado ,
            jugando
        }

    }
}
