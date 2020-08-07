using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class UlazSkidanjeProizvod
    {
        public int Id { get; set; }

        public int UlazSkidanjeId { get; set; }
        public UlazSkidanje UlazSkidanje { get; set; }

        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }

        public int PlaniranaKolicina { get; set; }
        public int Kolicina { get; set; }
        public DateTime Datum { get; set; }

        public double CijenaKomad { get; set; }
    }
}