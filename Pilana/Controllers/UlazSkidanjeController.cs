using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pilana.Models;

namespace Pilana.Controllers
{
    public class UlazSkidanjeController : Controller
    {
        mssql s = new mssql();
        // GET: UlazSkidanje
        public ActionResult Index(int id, int? UlazSkidanjeId)
        { 
            Ulaz u = s.Ulaz.Include("Klasa").Single(x => x.Id == id);
            ViewData["u"] = u;
            List<UlazSkidanje> us = s.UlazSkidanje.Where(x => x.UlazId == id).ToList();
            ViewData["us"] = us;
            int BrojSkinutihBalvana = 0;
            foreach (UlazSkidanje x in us)
            {
                BrojSkinutihBalvana += x.SkinutoBalvana;
            }
            ViewData["BrojSkinutihBalvana"] = BrojSkinutihBalvana;

            if (UlazSkidanjeId!=null)
            {
                UlazSkidanje use = s.UlazSkidanje.Single(x => x.Id == UlazSkidanjeId);
                ViewData["use"] = use;
            }
            else
            {
                UlazSkidanje use = new UlazSkidanje();
                ViewData["use"] = use;
            }

            Valuta v = s.Valuta.SingleOrDefault();
            ViewData["v"] = v;

            return View();
        }

        public ActionResult Snimi(int UlazId, int ulazSkidanjeId, int BrojBalvana, string DatumKreiranja, string Napomena)
        {
            UlazSkidanje us;

            if (ulazSkidanjeId==0)
                us = new UlazSkidanje();
            else
                us = s.UlazSkidanje.Single(x => x.Id == ulazSkidanjeId);

            us.DatumKreiranja = DateTime.Parse(DatumKreiranja);
            us.SkinutoBalvana = BrojBalvana;
            us.Napomena = Napomena;
            us.UlazId = UlazId;


            if (us.Id == 0)
                s.UlazSkidanje.Add(us);

            s.SaveChanges();

            return RedirectToAction("Index", new {id = UlazId});
        }
        public ActionResult RadniNalozi(string Naloga)
        {
            List<UlazSkidanje> u;
            if (Naloga==null )
            {
                u = s.UlazSkidanje.Where(x => x.Zavrseno == false).OrderByDescending(x => x.DatumKreiranja).ToList();
            }
            else
            {
                int NalogId = Int32.Parse(Naloga);
                u = s.UlazSkidanje.Where(x => x.Zavrseno == true).OrderByDescending(x=>x.DatumKreiranja).Take(NalogId).ToList();
            }
            
            ViewData["u"] = u;

            if (Naloga != null)
                ViewData["br"] = Int32.Parse(Naloga);


            return View("RadniNalozi");
        }
        public ActionResult Zavrsi(int Id)
        {
            UlazSkidanje u = s.UlazSkidanje.SingleOrDefault(x => x.Id == Id);
            u.Zavrseno = true;
            s.SaveChanges();
            return RedirectToAction("RadniNalozi", "UlazSkidanje");
        }
    }
}