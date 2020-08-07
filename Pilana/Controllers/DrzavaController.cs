using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pilana.Models;

namespace Pilana.Controllers
{
    public class DrzavaController : Controller
    {
        mssql s = new mssql();
        // GET: Drzava
        public ActionResult Index(int? id)
        {
            Drzava dr;
            if (id==null)
                dr = new Drzava();
            else
                dr = s.Drzava.SingleOrDefault(x => x.Id == id);
            ViewData["dr"] = dr;

            List<Drzava> d = s.Drzava.OrderBy(x=>x.Naziv).ToList();
            ViewData["d"] = d;
            return View();
        }

        public ActionResult Snimi(int id, string NazivDrzave)
        {
            Drzava d;
            if (id == 0)
                d = new Drzava();
            else
                d = s.Drzava.SingleOrDefault(x => x.Id == id);

            d.Naziv = NazivDrzave;
            if (id == 0)
                s.Drzava.Add(d);
            s.SaveChanges();
            return RedirectToAction("");
        }
        public ActionResult Edit(int id)
        {

            return RedirectToAction("", "Drzava", new {id=id });
        }
    }
}