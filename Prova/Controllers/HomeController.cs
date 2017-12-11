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
        private Prova.Models.ProjetoModeloDBEntities db = new ProjetoModeloDBEntities();

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

    }
}
