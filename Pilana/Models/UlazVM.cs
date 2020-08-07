using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class UlazVM
    {
        public int Id { get; set; }
        public int BrojBalvana { get; set; }
        public int BrojBalvanaIskoristeno { get; set; }
        public double M3 { get; set; }
        public double CijenaM3 { get; set; }
        public DateTime DatumDostave { get; set; }

        public int AdministratorId { get; set; }
        public Administrator Administrator { get; set; }

        public Boolean Zavrseno { get; set; }
        public string Klasa { get; set; }
    }
}