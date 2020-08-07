using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class Narudzba
    {
        public int Id { get; set; }

        public int KlijentId { get; set; }
        public Klijent Klijent { get; set; }

        public DateTime Datum { get; set; }

        public double? Popust { get; set; }
        public double? Prevoz { get; set; }
    }
}