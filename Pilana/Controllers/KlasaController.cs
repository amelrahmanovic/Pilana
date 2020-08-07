using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class KlasaController : Controller
    {
        mssql s = new mssql();
        // GET: Klasa
        public ActionResult Index()
        {
            List<Klasa> k = s.Klasa.Where(x=>x.Izbrisano==false).ToList();
            ViewData["Klase"] = k;
            return View();
        }
        public ActionResult Snimi(string Naziv)
        {
            Klasa k = new Klasa();
            k.Naziv = Naziv;

            s.Klasa.Add(k);
            s.SaveChanges();
            return RedirectToAction("");
        }

        public ActionResult Izbrisi(int id)
        {
            s.Klasa.Single(x => x.Id == id).Izbrisano = true;
            s.SaveChanges();
            return RedirectToAction("");
        }
    }
}