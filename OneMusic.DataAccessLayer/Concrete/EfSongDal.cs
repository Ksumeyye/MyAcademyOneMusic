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
    public class EfSongDal : GenericRepository<Song>, ISongDal
    {
        private readonly OneMusicContext _context;
        public EfSongDal(OneMusicContext context) : base(context)
        {
        }

        public List<Song> GetSongsWithAlbumAndArtist()
        {
            throw new NotImplementedException();
        }
    }
}
