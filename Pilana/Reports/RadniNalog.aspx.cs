using Microsoft.Reporting.WebForms;
using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Pilana.Reports
{
    public partial class RadniNalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["userInfo"] == null)
                Response.Redirect("/Home/Login");

            if (!IsPostBack)
            {
                int idPokupi = Int32.Parse(Request.QueryString["id"]);
                mssql s = new mssql();
                List<UlazSkidanje> u = s.UlazSkidanje.Where(x=>x.Id==idPokupi).ToList();
                List<UlazSkidanjeProizvodVM> usp = s.UlazSkidanjeProizvod.Include("Proizvod").Include("Proizvod.Klasa").Where(x => x.UlazSkidanjeId == idPokupi).Select(x => new UlazSkidanjeProizvodVM() {
                    Datum = x.Datum,
                    Id = x.Id,
                    Kolicina = x.Kolicina,
                    PlaniranaKolicina = x.PlaniranaKolicina,
                    Debljina = x.Proizvod.Visina.ToString(),
                    Sirina = x.Proizvod.Sirina.ToString(),
                    Duzina = x.Proizvod.Duzina.ToString(),
                    Klasa = x.Proizvod.Klasa.Naziv,
                    Naziv=x.Proizvod.Naziv
                }).ToList();

                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet", u));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", usp));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/RadniNalog.rdlc";

                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}