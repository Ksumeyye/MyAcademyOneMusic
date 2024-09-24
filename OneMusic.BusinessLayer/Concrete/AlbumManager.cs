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
    public class AlbumManager : IAlbumService
    {
        private readonly IAlbumDal _albumDal;

        public AlbumManager(IAlbumDal albumDal)
        {
            _albumDal = albumDal;
        }

        public void TCreate(Album entity)
        {
            _albumDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _albumDal.Delete(id);
        }

        public List<Album> TGetAlbumsByArtist(int id)
        {
            return _albumDal.GetAlbumsByArtist(id);
        }

        public List<Album> TGetAlbumswithArtist()
        {
            return _albumDal.GetAlbumswithArtist();
        }

        public string? TGetAlbumsWithArtist()
        {
            throw new NotImplementedException();
        }

        public Album TGetById(int id)
        {
            return _albumDal.GetById(id);
        }

        public List<Album> TGetList()
        {
            return _albumDal.GetList();
        }

        public void TUpdate(Album entity)
        {
            _albumDal.Update(entity);
        }
    }
}