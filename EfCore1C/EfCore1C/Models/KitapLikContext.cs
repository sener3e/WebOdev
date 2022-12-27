using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore1C.Models
{
    public class KitapLikContext:DbContext
    {
       public DbSet<Kitap> Kitaplar { get; set; }
       public DbSet<Yazar> Yazarlar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=2022Kitap1C;Trusted_Connection=True;");
        }

    }
}
