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


        private (int madera, int piedra, int comida) CalcularMetasTotales(bool esV1, int? seed = null, int totalMax = 100)
        {
            Random rnd = seed.HasValue ? new Random(seed.Value) : new Random();

            double r1 = rnd.NextDouble();
            double r2 = rnd.NextDouble();
            double r3 = rnd.NextDouble();

            double suma = r1 + r2 + r3;

            int madera = (int)Math.Round((r1 / suma) * totalMax);
            int piedra = (int)Math.Round((r2 / suma) * totalMax);
            int comida = (int)Math.Round((r3 / suma) * totalMax);

            int ajuste = (madera + piedra + comida) - totalMax;
            if (ajuste > 0)
            {
                if (madera >= piedra && madera >= comida) madera -= ajuste;
                else if (piedra >= madera && piedra >= comida) piedra -= ajuste;
                else comida -= ajuste;
            }

            int min = esV1 ? 10 : 1;
            int max = esV1 ? 100 : 10;
            madera = Math.Clamp(madera, min, max);
            piedra = Math.Clamp(piedra, min, max);
            comida = Math.Clamp(comida, min, max);

            return (madera, piedra, comida);
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

                var (metaMadera, metaPiedra, metaComida) = CalcularMetasTotales(esV1, seed: null, totalMax: 100);

                Partida nuevaPartida = new Partida
                {
                    Estado = Partida.EstadoPartida.jugando,
                    MetaMadera = metaMadera,
                    MetaPiedra = metaPiedra,
                    MetaComida = metaComida
                };

                _context.Partidas.Add(nuevaPartida);
                _context.SaveChanges();

                nuevaPartida.Recursos = new List<Recurso>
                {
                    new Recurso { Nombre = Recurso.RecursosPartida.Madera, PartidaId = nuevaPartida.Id },
                    new Recurso { Nombre = Recurso.RecursosPartida.Piedra, PartidaId = nuevaPartida.Id },
                    new Recurso { Nombre = Recurso.RecursosPartida.Comida, PartidaId = nuevaPartida.Id }
                };

                _context.Recursos.AddRange(nuevaPartida.Recursos);
                _context.SaveChanges();

                return View(nuevaPartida);
            }

            return View(partida);
        }


    }
}
