using Coopera.Data;
using Coopera.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coopera.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }
        // GET: UsuarioController
        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(string nombre)
        {
            if (nombre !=null)
            {
                Jugador jugador = new Jugador(nombre);
                _context.Jugadores.Add(jugador);
                _context.SaveChanges();
                return View();
                
            }

            Console.WriteLine("No se ha introducido un nombre");
            return RedirectToAction("Index", "Home");

        }
    }
}
