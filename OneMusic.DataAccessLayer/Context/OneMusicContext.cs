using Microsoft.EntityFrameworkCore;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Context
{
    public class OneMusicContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-O1DRK0LF;database=OneMusicDb; user=sa;Password=kaya96SUM;trustServerCertificate=true") ;
        }
        public DbSet<About>Abouts { get; set; }
        public DbSet<Album>Albums { get; set; }
        public DbSet<Banner>Banners { get; set; }
        public DbSet<Contact>Contacts { get; set; }
        public DbSet<Message>Messages { get; set; }
        public DbSet<Singer>Singers { get; set; }
        public DbSet<Song>Songs { get; set; }

    }
}
