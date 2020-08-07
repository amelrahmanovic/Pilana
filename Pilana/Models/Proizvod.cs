using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Duzina { get; set; }
        public double Sirina { get; set; }
        public double Visina { get; set; }
        public bool Poluproizvod { get; set; }
        public double CijenaM3 { get; set; }

        public int KlasaId { get; set; }
        public Klasa Klasa { get; set; }

        public int Kolicina { get; set; }
    }
}