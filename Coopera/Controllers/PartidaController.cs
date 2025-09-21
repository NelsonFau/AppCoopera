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


        private (int madera, int piedra, int comida) CalcularMetasTotales(bool esV1, int? semilla = null, int totalMax=100)
        {
            Random rnd = semilla.HasValue ? new Random(semilla.Value) : new Random();

            // Generar proporciones aleatorias
            double r1 = rnd.NextDouble();
            double r2 = rnd.NextDouble();
            double r3 = rnd.NextDouble();

            double suma = r1 + r2 + r3;

            // Calcular valores iniciales proporcionales
            int madera = (int)Math.Floor((r1 / suma) * totalMax);
            int piedra = (int)Math.Floor((r2 / suma) * totalMax);
            int comida = (int)Math.Floor((r3 / suma) * totalMax);

            // Ajuste para que la suma sea exactamente totalMax
            int restante = totalMax - (madera + piedra + comida);
            while (restante > 0)
            {
                // Elegimos aleatoriamente a quién sumar 1 hasta que se termine el restante
                int opcion = rnd.Next(3);
                if (opcion == 0) madera++;
                else if (opcion == 1) piedra++;
                else comida++;
                restante--;
            }

            // Aplicar límites mínimos y máximos
            int min = esV1 ? 10 : 1;
            int max = esV1 ? 100 : 10;

            madera = Math.Clamp(madera, min, max);
            piedra = Math.Clamp(piedra, min, max);
            comida = Math.Clamp(comida, min, max);

            // Ajuste final si los límites rompieron la suma total
            int ajusteFinal = totalMax - (madera + piedra + comida);
            while (ajusteFinal != 0)
            {
                if (ajusteFinal > 0)
                {
                    if (madera < max) { madera++; ajusteFinal--; }
                    else if (piedra < max) { piedra++; ajusteFinal--; }
                    else if (comida < max) { comida++; ajusteFinal--; }
                }
                else // ajusteFinal < 0
                {
                    if (madera > min) { madera--; ajusteFinal++; }
                    else if (piedra > min) { piedra--; ajusteFinal++; }
                    else if (comida > min) { comida--; ajusteFinal++; }
                }
            }

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

                var (metaMadera, metaPiedra, metaComida) = CalcularMetasTotales(esV1, semilla: null, totalMax:100);

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
