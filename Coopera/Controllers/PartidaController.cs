using Coopera.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Coopera.Data;

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
        public ActionResult Index()
        {
            Partida partida = _context.Partidas.FirstOrDefault();
            if (partida == null)
            {
                partida = new Partida();
                return View();
            }
            if (partida.Estado == Coopera.Models.Partida.EstadoPartida.jugando)
            {

            }

                //List<Recurso> recursos = _context.Regursos.ToList();
                //return View(recursos);
                return View();
        }

         
     
    }
}
