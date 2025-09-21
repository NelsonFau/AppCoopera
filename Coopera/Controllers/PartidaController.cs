using Coopera.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Coopera.Data;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Controllers
{
    public class PartidaController : Controller
    {
        private readonly AppDbContext _context;
        public PartidaController(AppDbContext context)
        {
            _context = context;
        }


        private int CalcularMeta(bool esV1)
        {
            Random rnd = new Random();
            double aleatorio = rnd.NextDouble(); // 0..1
            double factor = esV1 ? 100 : 10;

            int meta = (int)Math.Round(aleatorio * factor);

            return esV1 ? Math.Clamp(meta, 10, 100) : Math.Clamp(meta, 1, 10);
        }

        [HttpGet]
        public ActionResult Index(string version = "V1")
        {
            Partida? partida = _context.Partidas
                                  .Include(p => p.Recursos)
                                  .Where(p => p.Estado == Partida.EstadoPartida.jugando)
                                  .OrderByDescending(p => p.Id)
                                  .FirstOrDefault();

            if (partida == null)
            {
                bool esV1 = version == "V1";

                Partida nuevaPartida = new Partida
                {
                    Estado = Partida.EstadoPartida.jugando,
                    MetaMadera = CalcularMeta(esV1),
                    MetaPiedra = CalcularMeta(esV1),
                    MetaComida = CalcularMeta(esV1)
                };

                _context.Partidas.Add(nuevaPartida);
                _context.SaveChanges();

                nuevaPartida.Recursos = new List<Recurso>
                {
                    new Recurso { Nombre = Recurso.RecursosPartida.Madera, PartidaId = nuevaPartida.Id },
                    new Recurso { Nombre = Recurso.RecursosPartida.Piedra, PartidaId = nuevaPartida.Id},
                    new Recurso { Nombre = Recurso.RecursosPartida.Comida, PartidaId = nuevaPartida.Id}
                };
                _context.Recursos.AddRange(nuevaPartida.Recursos);
                _context.SaveChanges();

                return View(nuevaPartida);
            }

            return View(partida);
        }

    }
}
