using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class ProizvodController : Controller
    {
        mssql s = new mssql();
        // GET: Proizvod
        public ActionResult Index(int? id, int? Sort)
        {
            Proizvod p;
            if (id == null || id == 0)
                p = new Proizvod();
            else
                p = s.Proizvod.Single(x => x.Id == id);
            ViewData["p"] = p;
            List<Klasa> k = s.Klasa.Where(x=>x.Izbrisano==false).ToList();
            ViewData["k"] = k;

            List<Proizvod> pro;
            if (Sort==2)
            {
                pro = s.Proizvod.OrderByDescending(x=>x.Kolicina).ToList();
                
            }
            else
            {
                if (Sort == 3)
                {
                    pro = s.Proizvod.OrderBy(x => x.Kolicina).ToList();
                }
                else
                {
                    pro = s.Proizvod.ToList();
                }
            }
            ViewData["pro"] = pro;

            Valuta v = s.Valuta.SingleOrDefault();
            ViewData["v"] = v;

            return View();
        }
        public ActionResult Snimi(int id, string Naziv, string Duzina, string Sirina, string Visina, string Poluproizvod, string CijenaM3, string Kolicina, int Klasa)
        {
            Proizvod p;

            if (id == 0)
                p = new Proizvod();
            else
                p = s.Proizvod.Single(x => x.Id == id);

            p.Naziv = Naziv;
            p.Duzina = double.Parse(Duzina);
            p.Sirina = double.Parse(Sirina);
            p.Visina = double.Parse(Visina);
            if (Poluproizvod == "Proizvod")
                p.Poluproizvod = true;
            else
                p.Poluproizvod = false;
            p.CijenaM3 = double.Parse(CijenaM3);
            p.Kolicina = Int32.Parse(Kolicina);
            p.KlasaId = Klasa;

            if (id == 0)
                s.Proizvod.Add(p);
            s.SaveChanges();
            return RedirectToAction("");
        }
    }
}