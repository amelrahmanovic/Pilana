using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class ValutaController : Controller
    {
        mssql s = new mssql();
        // GET: Valuta
        public ActionResult Index()
        {
            Valuta v = s.Valuta.FirstOrDefault();
            if (v==null)
                v = new Valuta();
            ViewData["v"] = v;
            return View();
        }
        public ActionResult Snimi(int id, string val)
        {
            Valuta v;
            if (id == 0)
                v = new Valuta();
            else
                v = s.Valuta.SingleOrDefault(x => x.Id == id);
            
            v.Val = val;
            if (id == 0)
                s.Valuta.Add(v);

            s.SaveChanges();
            return RedirectToAction("");
        }
    }
}