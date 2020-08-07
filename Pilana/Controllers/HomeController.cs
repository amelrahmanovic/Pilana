using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pilana.Controllers
{
    public class HomeController : Controller
    {
        mssql s = new mssql();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Request.Cookies["userInfo"] == null)
                return View("Login");
            else
                return RedirectToAction("", "Home");
        }
        public ActionResult LoginTest(string name, string password)
        {
            Administrator a = s.Administrator.SingleOrDefault(x => x.KorisnickoIme == name && x.Lozinka == password && x.Izbrisan==false);

            if (a == null)
                return RedirectToAction("Login");
            else
            {
                Response.Cookies["userInfo"]["userName"] = a.Ime + " " + a.Prezime;
                Response.Cookies["userInfo"]["id"] = a.Id.ToString();
                Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
                Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(31);

                HttpCookie aCookie = new HttpCookie("userInfo");
                aCookie.Values["userName"] = a.Ime + " " + a.Prezime; ;
                aCookie.Values["id"] = a.Id.ToString();
                aCookie.Values["lastVisit"] = DateTime.Now.ToString();
                aCookie.Expires = DateTime.Now.AddDays(31);
                Response.Cookies.Add(aCookie);

                return RedirectToAction("", "Home");
            }

        }
    }
}