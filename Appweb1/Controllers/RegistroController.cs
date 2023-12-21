using Dominio;
using Microsoft.AspNetCore.Mvc;
using AppWeb.Filtros;

namespace Appweb1.Controllers
{
    public class RegistroController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {

            return View(new Miembro());
        }

        
        [HttpPost]
    public IActionResult Index(Miembro miembro)
        {
            try
            {
                _sistema.AgregarMiembro(miembro);
                ViewBag.Sucess = "Registro Exitoso";
                return Redirect("/Login/Index");
            }
            catch (Exception e)
            { 
                ViewBag.Error = e.Message;
            }
            return View(miembro);
        }

    }
}
