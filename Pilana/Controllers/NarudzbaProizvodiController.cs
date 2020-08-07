using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class NarudzbaProizvodiController : Controller
    {
        mssql s = new mssql();
        // GET: NarudzbaProizvodi
        public ActionResult Index(int id, int Nid)
        {
            Klijent k = s.Klijent.Include("Drzava").Single(x => x.Id == id);
            Narudzba n = s.Narudzba.OrderByDescending(x=>x.Datum).Where(x=>x.KlijentId==id).FirstOrDefault();
            ViewData["k"] = k;
            ViewData["n"] = n;
            List<Proizvod> p = s.Proizvod.OrderBy(x=>x.Poluproizvod).OrderBy(x=>x.CijenaM3).ToList();
            ViewData["p"] = p;
            List<NarudzbaProizvodi> np = s.NarudzbaProizvodi.Where(x => x.NarudzbaId == Nid).ToList();
            ViewData["np"] = np;

            double Ukupno = 0;
            foreach(NarudzbaProizvodi x in np)
            {
                Ukupno += x.Kolicina * x.CijenaKomad;
            }
            ViewData["Ukupno"] = Ukupno;

            return View();
        }
        public ActionResult Snimi(int NarudzbaId, int KlijentId, int ProizvodId, int Kolicina)
        {
            NarudzbaProizvodi np = new NarudzbaProizvodi();
            np.NarudzbaId = NarudzbaId;
            np.ProizvodId = ProizvodId;
            np.Kolicina = Kolicina;
            np.CijenaKomad=s.Proizvod.SingleOrDefault(x=>x.Id==ProizvodId).CijenaM3;
            s.NarudzbaProizvodi.Add(np);

            Proizvod p = s.Proizvod.Single(x => x.Id == ProizvodId);
            p.Kolicina -= Kolicina;

            s.SaveChanges();
            return RedirectToAction("", "NarudzbaProizvodi", new { id = KlijentId, Nid= NarudzbaId });
        }
        public ActionResult DodatniTrosak(int NarudzbaId, int KlijentId, double Popust, double Prevoz)
        {
            Narudzba n = s.Narudzba.SingleOrDefault(x => x.Id == NarudzbaId);
            n.Prevoz = Prevoz;
            n.Popust = Popust;
            s.SaveChanges();
            return RedirectToAction("", "NarudzbaProizvodi", new { id = KlijentId, Nid = NarudzbaId });
        }
        public ActionResult Skini(int id, int Nid, int Pid)
        {
            NarudzbaProizvodi np = s.NarudzbaProizvodi.SingleOrDefault(x => x.Id == Pid);

            Proizvod p = s.Proizvod.SingleOrDefault(x => x.Id == np.ProizvodId);
            p.Kolicina += np.Kolicina;

            s.NarudzbaProizvodi.Remove(np);

            s.SaveChanges();

            return RedirectToAction("", "NarudzbaProizvodi", new { id = id, Nid = Nid });
        }
    }
}