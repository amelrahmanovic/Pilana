using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class NarudzbaProizvodi
    {
        public int Id { get; set; }

        public int NarudzbaId { get; set; }
        public Narudzba Narudzba { get; set; }

        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }

        public int Kolicina { get; set; }
        public double CijenaKomad { get; set; }
    }
}