using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class KlijentController : Controller
    {
        mssql s = new mssql();
        // GET: Klijent
        public ActionResult Index(int ? id)
        {
            List<Klijent> kli = s.Klijent.Where(x=>x.Izbrisan==false).Include(x=>x.Drzava).ToList();
            ViewData["Klijenti"] = kli;
            List<Drzava> d = s.Drzava.OrderBy(x=>x.Naziv).ToList();
            ViewData["d"] = d;

            if (id!=null)
            {
                Klijent k = s.Klijent.SingleOrDefault(x => x.Id == id);
                ViewData["k"] = k;
            }
            else
            {
                Klijent k = new Klijent();
                ViewData["k"] = k;
            }
            

            return View();
        }
        public ActionResult Snimi(int? id, string Ime,string Prezime, int Drzava, string Adresa, string KontaktBroj)
        {
            Klijent kli;

            if (id==0 || id==null)
                kli = new Klijent();
            else
                kli = s.Klijent.SingleOrDefault(x => x.Id == id);

            
            kli.Ime = Ime;
            kli.Prezime = Prezime;
            kli.DrzavaId = Drzava;
            kli.Adresa = Adresa;
            kli.KontaktBroj = KontaktBroj;


            if (id == 0 || id == null)
                s.Klijent.Add(kli);


            s.SaveChanges();
            return RedirectToAction("");
        }
        public ActionResult Delete(int id)
        {
            s.Klijent.Single(x => x.Id == id).Izbrisan = true;
            s.SaveChanges();
            return RedirectToAction("");
        }
    }
}