using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace Pilana.Controllers
{
    public class NarudzbaController : Controller
    {
        mssql s = new mssql();
        // GET: Narudzba
        public ActionResult Index(int? id)
        {
            if (id!=null)
            {
                Klijent k = s.Klijent.Single(x => x.Id == id);
                ViewData["k"] = k;
                List<Narudzba> n = s.Narudzba.Where(x => x.KlijentId == id).ToList();
                ViewData["n"] = n;
                return View();
            }
            return RedirectToAction("", "Klijent");
        }
        public ActionResult Snimi(int Id)
        {
            Narudzba n = new Narudzba();
            n.KlijentId = Id;
            n.Datum = DateTime.Now;
            n.Popust = 0;
            n.Prevoz = 0;
            s.Narudzba.Add(n);
            s.SaveChanges();
            List<Narudzba> n2 = s.Narudzba.OrderByDescending(x=>x.Id).Where(x => x.KlijentId==n.KlijentId).Take(1).ToList();
            return RedirectToAction("", "NarudzbaProizvodi", new {id= Id, Nid=n2[0].Id});
        }
    }
}