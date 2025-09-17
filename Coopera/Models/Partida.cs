namespace Coopera.Models
{
    public class Partida
    {
        public int IdPartida { get; set; }
        public string Nombre { get; set; }
        public EstadoPartida Estado { get; set; }
        Dictionary<Recurso,int> Recursos { get; set; }
        List<Jugador> Jugadores { get; set; }

        List<MiniJuego> MiniJuegos { get; set; }



        public enum EstadoPartida
        {
            presentadoResultado ,
            jugando
        }

    }
}
