using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class UlazSkidanjeProizvodVM
    {
        public int Id { get; set; }

        public string Duzina { get; set; }
        public string Sirina { get; set; }
        public string Debljina { get; set; }
        public string Klasa { get; set; }

        public int PlaniranaKolicina { get; set; }
        public int Kolicina { get; set; }
        public DateTime Datum { get; set; }

        public string Naziv { get; set; }
    }
}