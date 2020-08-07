using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public bool Izbrisan { get; set; }

    }
}