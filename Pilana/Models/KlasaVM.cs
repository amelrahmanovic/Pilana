using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class KlasaVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; } 
        public bool Izbrisano { get; set; }
        public double M3 { get; set; }
        public double BalvanaNaSkladistu { get; set; }
        public double BalvanaIskoristeno { get; set; }
    }
}