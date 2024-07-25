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
        private readonly ISongDal _sondDal;

        public SongManager(ISongDal sondDal)
        {
            _sondDal = sondDal;
        }

        public void TCreate(Song entity)
        {
            _sondDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _sondDal.Delete(id);
        }

        public Song TGetById(int id)
        {
            return _sondDal.GetById(id);
        }

        public List<Song> TGetList()
        {
            return _sondDal.GetList();
        }

        public void TUpdate(Song entity)
        {
           _sondDal.Update(entity);
        }
    }
}
