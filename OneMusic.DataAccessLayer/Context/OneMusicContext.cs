using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Context
{
    public class OneMusicContext : IdentityDbContext<AppUser, AppRole, int> // IdentityDbContext'ten miras alacak. O Anda  Microsoft.AspNetCore.Identity.EntityFrameworkCore'u dahil etti.
    {
        //DbContext 'ten miras alması demek OneMusicContext'tin DbContext içerisindeki metotlara erişebilmesi demektir.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  // Protected dememizdeki sebep bulunduğu sınıf içerisinden o sınıftan miras alan (alt sınıflar) sınıflardan erişilebilinmesidir.
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-O1DRK0LF;database=OneMusicDb;user=sa;Password=kaya96SUM;trustServerCertificate=true"); //Sql bağlantı adresi
        }

        // Db'Setler
        public DbSet<About> Abouts { get; set; }  //About class(entity) adıdır, Abouts ise sql tarafına yansıyan tablonun adıdır.
        public DbSet<Album> Albums { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}