using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pilana.Models
{
    public class UlazSkidanje
    {
        public int Id { get; set; }

        public int UlazId { get; set; }
        public Ulaz Ulaz { get; set; }

        public int SkinutoBalvana { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Napomena { get; set; }
        public bool Zavrseno { get; set; }
    }
}