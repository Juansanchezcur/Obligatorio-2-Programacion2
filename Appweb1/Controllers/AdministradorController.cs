using AppWeb.Filtros;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Appweb1.Controllers
{
    [Admin]
    public class AdministradorController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
    
        public IActionResult Index()
        {
            try {
                ViewBag.Miembros = _sistema.ListarMiembrosOrdenados();
                    
            } catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }
 
    public IActionResult BloquearMiembro(string email)
        {
            try {
                _sistema.BloquearMiembro(email);   
            }
            catch(Exception e) { ViewBag.Error = e.Message; }
       
           return RedirectToAction("Index");
    }
        public IActionResult ListarPosts()
        {
            try
            {
                ViewBag.Posts = _sistema.ListarPosts();

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }
        public IActionResult CensurarPost(int id)
        {
            try
            {
                _sistema.CensurarPost(id);
            }
            catch (Exception e) { ViewBag.Error = e.Message; }

            return RedirectToAction("ListarPosts");
        }
    }
}

