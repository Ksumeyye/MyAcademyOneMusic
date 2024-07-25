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
    public class SingerManager : ISingerService
    {
        private readonly ISingerDal _singerDal;

        public SingerManager(ISingerDal singerDal)
        {
            _singerDal = singerDal;
        }

        public void TCreate(Singer entity)
        {
            _singerDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _singerDal.Delete(id);
        }

        public Singer TGetById(int id)
        {
            return _singerDal.GetById(id);
        }

        public List<Singer> TGetList()
        {
            return _singerDal.GetList();
        }

        public void TUpdate(Singer entity)
        {
            _singerDal.Update(entity);
        }
    }
}
