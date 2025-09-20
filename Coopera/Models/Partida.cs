namespace Coopera.Models
{
    public class Partida
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public EstadoPartida Estado { get; set; } = EstadoPartida.jugando;
        public List<Recurso>? Recursos { get; set; } = new List<Recurso>();
        public List<Jugador>? Jugadores { get; set; } = new List<Jugador>();

        public List<MiniJuego>? MiniJuegos { get; set; } = new List<MiniJuego>();

        public DateTime HoraInicio { get; set; } = DateTime.Now;
        public DateTime HoraFinal { get; set; }
        
        public enum EstadoPartida
        {
            presentadoResultado ,
            jugando
        }

        public Partida()
        {
            
        }
    }
}
