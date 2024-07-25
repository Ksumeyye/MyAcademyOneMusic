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
    public class AboutManager : IAboutService  
    {
        private readonly IAboutDal _aboutDal; 

        public AboutManager(IAboutDal aboutDal) 
        {
            _aboutDal = aboutDal; 
        }

        public void TCreate(About entity) 
        {
            _aboutDal.Create(entity);  
        }

        public void TDelete(int id) 
        {
            _aboutDal.Delete(id); 
        }

        public About TGetById(int id) 
        {
            return _aboutDal.GetById(id);
        }

        public List<About> TGetList()
        {
            return _aboutDal.GetList();
        }

        public void TUpdate(About entity)
        {
            _aboutDal.Update(entity);
        }
    }
}
