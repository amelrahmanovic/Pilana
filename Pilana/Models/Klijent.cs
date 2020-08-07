using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class Klijent
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }
        public string KontaktBroj { get; set; }
        public bool Izbrisan { get; set; }
    }
}