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
    public class EfAboutDal : GenericRepository<About>, IAboutDal //IAboutDal ıgenericte crud işlemlerinden farklı işlemler yapmaya yarar.
    {
        public EfAboutDal(OneMusicContext context) : base(context)
        {
        }
    }
}
