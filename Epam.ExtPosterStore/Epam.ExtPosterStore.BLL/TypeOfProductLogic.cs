using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class TypeOfProductLogic : ITypeOfProductLogic
    {
        private readonly ITypeOfProductDao _typeOfProductDao;
        private readonly IProductDao _productDao;
        public TypeOfProductLogic(ITypeOfProductDao typeOfProductDao, IProductDao productDao)
        {
            _typeOfProductDao = typeOfProductDao;
            _productDao = productDao;
        }
        public int Add(string tittle)
        {
            int response = 400;
            try
            {
                var current =_typeOfProductDao.Add(new TypeOfProduct(tittle));
                if (current!=null)
                {
                    response = 200;
                }
            }
            catch (ArgumentException e)
            {
                Logger.Logger.InitLogger();
                Logger.Logger.Log.Error(e);
                response = 400;
            }
            return response;
        }

        public IEnumerable<TypeOfProduct> GetAll()
        {
            return _typeOfProductDao.GetAll();
        }

        public TypeOfProduct GetById(int id)
        {
            return _typeOfProductDao.GetById(id);
        }

        public int Remove(int id)
        {
            int response = 404;
            if (_typeOfProductDao.GetById(id) != null)
            {
                if (_productDao.GetAll().Any(p => p.TypeOfProduct.Id == id))
                {
                    response = 302;
                }
                else
                {
                    response = _typeOfProductDao.Remove(id);
                }
            }
            return response;
        }

        public int Update(string tittle, int targetId)
        {
            int response = 404;
            if (_typeOfProductDao.GetById(targetId) != null)
            {
                try
                {
                    var category = _typeOfProductDao.Update(new TypeOfProduct(tittle), targetId);
                    if (category != null)
                    {
                        response = 200;
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    response = 400;
                }

            }
            return response;
        }
    }
}
