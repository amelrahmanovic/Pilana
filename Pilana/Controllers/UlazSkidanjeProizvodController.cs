using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class UlazSkidanjeProizvodController : Controller
    {
        mssql s = new mssql();
        // GET: UlazSkidanjeProizvod
        public ActionResult Index(int id, string Block)
        {
            UlazSkidanje us = s.UlazSkidanje.Single(x => x.Id == id);
            Ulaz u = s.Ulaz.Single(x => x.Id == us.UlazId);
            List<Proizvod> p = s.Proizvod.ToList();
            List<UlazSkidanjeProizvod> usp = s.UlazSkidanjeProizvod.Include("Proizvod").Include("Proizvod.Klasa").Where(x=>x.UlazSkidanjeId==id).ToList();

            double PlaniranoM3 = 0, OstvarenoM3 = 0;

            foreach (UlazSkidanjeProizvod x in usp)
            {
                PlaniranoM3 += ((x.Proizvod.Duzina / 100) * (x.Proizvod.Sirina / 100) * (x.Proizvod.Visina / 100) * x.PlaniranaKolicina);
                OstvarenoM3 += ((x.Proizvod.Duzina / 100) * (x.Proizvod.Sirina / 100) * (x.Proizvod.Visina / 100) * x.Kolicina);
            }
            ViewData["PlaniranoM3"] = PlaniranoM3;
            ViewData["OstvarenoM3"] = OstvarenoM3;

            ViewData["us"] = us;
            ViewData["u"] = u;
            ViewData["p"] = p;
            ViewData["usp"] = usp;
            Valuta v = s.Valuta.SingleOrDefault();
            ViewData["v"] = v;
            ViewData["Block"] = Block;
            return View();
        }
        public ActionResult Snimi(int UlazSkidanjeId, int Proizvod, int PlaniranaKolicina, int Kolicina, string Datum)
        {
            UlazSkidanjeProizvod usp = new UlazSkidanjeProizvod();
            usp.UlazSkidanjeId = UlazSkidanjeId;
            usp.ProizvodId = Proizvod;
            usp.Kolicina = Kolicina;
            usp.PlaniranaKolicina = PlaniranaKolicina;
            usp.Datum = DateTime.Parse(Datum);
            s.UlazSkidanjeProizvod.Add(usp);

            if (Kolicina !=0)
            {
                Proizvod p = s.Proizvod.Single(x => x.Id == Proizvod);
                p.Kolicina += Kolicina;
            }
            usp.CijenaKomad = s.Proizvod.SingleOrDefault(x => x.Id == Proizvod).Id;
            s.SaveChanges();
            return RedirectToAction("Index", new { id = UlazSkidanjeId });
        }
        public ActionResult OstvarenaKolicinaSnimi(int id, int OstvarenaKolicina)
        {
            UlazSkidanjeProizvod u = s.UlazSkidanjeProizvod.SingleOrDefault(x => x.Id == id);
            u.Kolicina += OstvarenaKolicina;

            Proizvod p = s.Proizvod.SingleOrDefault(x => x.Id == u.ProizvodId);
            p.Kolicina += OstvarenaKolicina;

            s.SaveChanges();

            return RedirectToAction("Index", "UlazSkidanjeProizvod", new {id= u.UlazSkidanjeId});
        }
    }
}