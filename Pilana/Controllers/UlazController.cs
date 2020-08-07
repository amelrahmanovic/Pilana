using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pilana.Models;

namespace Pilana.Controllers
{
    public class UlazController : Controller
    {
        mssql s = new mssql();
        // GET: Ulaz
        public ActionResult Index(int ? id)
        {
            List<KlasaVM> KlasaStanje = s.Klasa.Where(x=>x.Izbrisano==false).Select(x => new KlasaVM()
            {
                BalvanaNaSkladistu = 0, Id = x.Id, Izbrisano = x.Izbrisano, M3 = 0, Naziv=x.Naziv, BalvanaIskoristeno=0
            }).ToList();




            List<Klasa> k = s.Klasa.Where(x => x.Izbrisano == false).ToList();
            ViewData["k"] = k;
            List<UlazVM> u = s.Ulaz.Where(x=>x.Zavrseno==false).Select(x=> new UlazVM()
            {
                Id=x.Id,
                AdministratorId=x.AdministratorId,
                BrojBalvana=x.BrojBalvana,
                BrojBalvanaIskoristeno=0,
                CijenaM3=x.CijenaM3,
                DatumDostave=x.DatumDostave,
                M3=x.M3,
                Zavrseno=x.Zavrseno,
                Klasa=x.Klasa.Naziv
            }).ToList();
            foreach (UlazVM x in u)
            {
                if (s.UlazSkidanje.Where(y => y.UlazId == x.Id).Count()==0)
                {
                    x.BrojBalvanaIskoristeno = 0;
                }
                else
                {
                    x.BrojBalvanaIskoristeno = s.UlazSkidanje.Where(y => y.UlazId == x.Id).Sum(z => z.SkinutoBalvana);
                }
            }

            foreach (UlazVM x in u)
            {
                foreach (KlasaVM x2 in KlasaStanje)
                {
                    if (x.Klasa==x2.Naziv)
                    {
                        x2.M3 = x.M3;
                        x2.BalvanaNaSkladistu = x.BrojBalvana;
                        x2.BalvanaIskoristeno = x.BrojBalvanaIskoristeno;
                    }
                }
            }
            ViewData["KlasaVM"] = KlasaStanje;


            ViewData["u"] = u;
            if (id>0)
            {
                Ulaz ul = s.Ulaz.Single(x => x.Id == id);
                ViewData["ul"] = ul;
            }
            else
            {
                Ulaz ul = new Ulaz();
                ViewData["ul"] = ul;
            }

            List<Ulaz> u2 = s.Ulaz.ToList();
            int UlazBalvanaUkupno = 0, SkinutoBalvana=0;
            foreach (Ulaz x in u2)
            {
                UlazBalvanaUkupno += x.BrojBalvana;
            }
            List<UlazSkidanje> us = s.UlazSkidanje.ToList();
            foreach (UlazSkidanje x in us)
            {
                SkinutoBalvana += x.SkinutoBalvana;
            }
            ViewData["Balvani"] = UlazBalvanaUkupno - SkinutoBalvana;

            Valuta v = s.Valuta.SingleOrDefault();
            ViewData["v"] = v;

            return View();
        }

        public ActionResult Snimi(int id, int Klasa, int BrojBalvana, string M3, string CijenaM3, string DatumDostave)
        {
            Ulaz u;
            if (id==0)
                u = new Ulaz();
            else
                u = s.Ulaz.Single(x => x.Id == id);
            
            u.M3 = Convert.ToDouble(M3);
            u.CijenaM3 = Convert.ToDouble(CijenaM3);
            u.BrojBalvana = BrojBalvana;
            u.DatumDostave = DateTime.Parse(DatumDostave);
            u.KlasaId = Klasa;
            u.AdministratorId = 1; //POTREBNO MJENJATI... UZETI IZ COOKIE

            if (id == 0)
                s.Ulaz.Add(u);

            s.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Zavrsi(int id)
        {
            Ulaz u = s.Ulaz.Single(x => x.Id == id);
            u.Zavrseno = true;
            s.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Stanje(string DatumOd, string DatumDo)
        {
            if (DatumOd!=null)
            {
                double Placeno = 0, Skladiste=0, Prodato=0;
                DateTime DatumOdd = DateTime.Parse(DatumOd), DatumDoo = DateTime.Parse(DatumDo);
                


                List<Ulaz> u = s.Ulaz.Where(x => x.DatumDostave >= DatumOdd && x.DatumDostave <= DatumDoo).ToList();
                foreach (Ulaz x in u)
                {
                    Placeno += x.CijenaM3 * x.M3;
                }

                ViewData["Placeno"] = Placeno;

                string v = s.Valuta.FirstOrDefault().Val;
                ViewData["v"] = v;

                List<Proizvod> p = s.Proizvod.ToList();
                foreach (Proizvod x in p)
                {
                    Skladiste += x.CijenaM3 * x.Kolicina;
                }
                ViewData["s"] = Skladiste;

                List<NarudzbaProizvodi> usp = s.NarudzbaProizvodi.Include("Narudzba").Where(x => x.Narudzba.Datum >= DatumOdd && x.Narudzba.Datum <= DatumDoo).ToList();
                foreach (NarudzbaProizvodi x in usp)
                {
                    double Popust = double.Parse(x.Narudzba.Popust.ToString());
                    Prodato += x.CijenaKomad * x.Kolicina - ((x.CijenaKomad * x.Kolicina)* (Popust/100));
                }
                List<Narudzba> nar = s.Narudzba.Where(x => x.Datum >= DatumOdd && x.Datum <= DatumDoo).ToList();
                foreach (Narudzba x in nar)
                {
                    double Prevoz = double.Parse(x.Prevoz.ToString());
                    Prodato += Prevoz;
                }
                ViewData["p"] = Prodato;
            }
            

            ViewData["Od"] = DatumOd;
            ViewData["Do"] = DatumDo;
            return View("Stanje");
        }
    }
}