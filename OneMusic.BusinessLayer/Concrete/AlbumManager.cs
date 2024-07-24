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

        public List<Album> TGetList()
        {
            return _albumDal.GetList();
        }

        public void TCreate(Album entity)
        {
            _albumDal.Create(entity);
        }

        public void TDelete(int id)
        {
           _albumDal.Delete(id);
        }

        public Album TGetById(int id)
        {
            return _albumDal.GetById(id);
        }

        public void TUpdate(Album entity)
        {
            _albumDal.Update(entity);
        }
    }
}
