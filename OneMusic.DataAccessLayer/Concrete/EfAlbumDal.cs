using Microsoft.EntityFrameworkCore;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Concrete
{
    public class EfAlbumDal : GenericRepository<Album>, IAlbumDal
    {
        private readonly OneMusicContext _context;
        public EfAlbumDal(OneMusicContext context) : base(context)
        {
            _context = context;
        }

        public List<Album> GetAlbumsWithSinger()
        {
            return _context.Albums.Include(x=>x.Singer).ToList();
        }
    }
}

