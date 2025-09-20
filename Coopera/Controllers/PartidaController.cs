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
        [HttpGet]
¿        public ActionResult Index()
        {
            Partida? partida = _context.Partidas
                              .Include(p => p.Recursos)
                              .Where(p => p.Estado == Partida.EstadoPartida.jugando)
                              .OrderByDescending(p => p.Id) 
                              .FirstOrDefault();


            if (partida == null)
            {
                Partida nuevaPartida = new Partida();
                List<Recurso>? recurso = new List<Recurso>
                {
                    new Recurso { Nombre = Recurso.RecursosPartida.Madera, PartidaId = nuevaPartida.Id, Partida = nuevaPartida },
                    new Recurso { Nombre = Recurso.RecursosPartida.Comida, PartidaId = nuevaPartida.Id, Partida = nuevaPartida },
                    new Recurso { Nombre = Recurso.RecursosPartida.Piedra, PartidaId = nuevaPartida.Id, Partida = nuevaPartida }
                };

                return View(recurso); // ✅ Siempre envías una lista
            }

            List<Recurso>? recursos = partida.Recursos;

            return View(recursos); // ✅ Siempre envías una lista
        }


        //List<Recurso> recursos = _context.Regursos.ToList();
        //return View(recursos);

    }
}
