using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prova.Models;

namespace Prova.Controllers
{
    public class HomeController : Controller
    {
        private ProjetoModeloDBEntities db = new ProjetoModeloDBEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Cliente()
        {
            var clientes = db.Clientes.ToList();

            return View(clientes);
        }

        public ActionResult Details(int id)
        {
            var cliente = db.Clientes.Find(id);
            if(cliente == null)
            {
                cliente = new Clientes();
            }

            return View(cliente);
        }


        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Edit(int id)
        {
            var cliente = db.Clientes.Find(id);

            if (cliente == null)
            {
                cliente = new Clientes();
            }

            return View(cliente);
        }

        public ActionResult Delete(int id)
        {
            var cliente = db.Clientes.Find(id);

            if (cliente == null)
            {
                cliente = new Clientes();
            }
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Remove(int ClienteId)
        {
            var cliente = db.Clientes.Find(ClienteId);
            var clientes = db.Clientes.ToList();

            if (cliente == null)
            {
                ViewData["mensagem"] = "Não foi possível excluir o registro!";
                return Content("<script>$('.alert-error').fadeIn().html('Operação não realizada!')</script>");
            }
            else
            {
                ViewData["mensagem"] = "Registro excluído com sucesso!";
                db.Clientes.Remove(cliente);
                db.SaveChanges();
            }
            return RedirectToAction("Cliente");
        }
    }
}
