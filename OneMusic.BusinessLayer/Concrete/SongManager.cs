using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Concrete
{
    public class SongManager : ISongService
    {
        private readonly ISongDal _songDal;

        public SongManager(ISongDal sondDal)
        {
            _songDal = sondDal;
        }

        public void TCreate(Song entity)
        {
            _songDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _songDal.Delete(id);
        }

        public Song TGetById(int id)
        {
            return _songDal.GetById(id);
        }

        public List<Song> TGetList()
        {
            return _songDal.GetList();
        }
        public List<Song> TGetSongsByAlbumId(int id)
        {
            return _songDal.GetSongsByAlbumId(id);
        }
        public List<Song> TGetSongsWithAlbumAndArtist()
        {
            return _songDal.GetSongsWithAlbumAndArtist();
        }

        public List<Song> TGetSongswithAlbumByUserId(int id)
        {
            return _songDal.GetSongswithAlbumByUserId(id);
        }
        public List<Song> TGetSongWithAlbum()
        {
            return _songDal.GetSongWithAlbum();
        }
        public void TUpdate(Song entity)
        {
           _songDal.Update(entity);
        }
    }
}
