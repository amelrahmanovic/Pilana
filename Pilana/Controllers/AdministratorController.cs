using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class AdministratorController : Controller
    {
        mssql s = new mssql();
        // GET: Administrator
        public ActionResult Index(int? id)
        {
            List<Administrator> a = s.Administrator.Where(x =>x.Izbrisan==false).ToList();
            Administrator ad;
            ViewData["Administrators"] = a;
            if (id == null)
                ad = new Administrator();
            else
                ad = s.Administrator.Single(x => x.Id == id);
            ViewData["ad"] = ad;
            return View();
        }
        public ActionResult Snimi(int? id, string Ime, string Prezime, string KorisnickoIme, string Lozinka)
        {
            Administrator a;
            if (id == 0)
                a = new Administrator();
            else
                a = s.Administrator.Single(x => x.Id == id);

            a.Ime = Ime;
            a.Prezime = Prezime;
            a.KorisnickoIme = KorisnickoIme;
            a.Lozinka = Lozinka;
            a.Izbrisan = false;

            if (id == 0)
                s.Administrator.Add(a);

            s.SaveChanges();
            return RedirectToAction("");
        }
        public ActionResult Brisi(int id)
        {
            Administrator a = s.Administrator.SingleOrDefault(x => x.Id == id);
            a.Izbrisan = true;
            s.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Password()
        {
            return View("Password");
        }
        public ActionResult PasswordUpdate(string Password, int id)
        {
            mssql s = new mssql();
            Administrator a = s.Administrator.Single(x => x.Id == id);
            a.Lozinka = Password;
            s.SaveChanges();
            return RedirectToAction("");
        }
        public ActionResult Logout()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-32);
            }
            return RedirectToAction("Login", "Home");
        }
    }
}