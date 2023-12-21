using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppWeb.Filtros;

namespace Appweb1.Controllers
{
    public class LoginController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
                }

        [HttpPost]
            public IActionResult Index(string email, string contrasena)
        {

            try{
                Usuario usuario = _sistema.BuscarUsuarioPorEmail(email);
                if (usuario == null || contrasena != usuario.Contrasena)
                {
                    ViewBag.Error = "Usuario y/o contraseña incorrecta";
                    return View("Index");
                }


                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("tipo", usuario.GetType().Name);

                if(HttpContext.Session.GetString("tipo") == "Administrador")
                { return Redirect("/Administrador/Index");
                }
                else
                {
                    return Redirect("/Miembro/Index");
                }

              

            }
            catch {
                return View("Index");  
            }
           


        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
