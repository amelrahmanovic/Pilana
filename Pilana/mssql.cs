using Pilana.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pilana
{
    public class mssql : DbContext
    {
        public mssql() : base("conn") { }

        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Klasa> Klasa { get; set; }
        public DbSet<Klijent> Klijent { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<NarudzbaProizvodi> NarudzbaProizvodi { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Ulaz> Ulaz { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<UlazSkidanje> UlazSkidanje { get; set; }
        public DbSet<UlazSkidanjeProizvod> UlazSkidanjeProizvod { get; set; }
        public DbSet<Valuta> Valuta { get; set; }
    }
}