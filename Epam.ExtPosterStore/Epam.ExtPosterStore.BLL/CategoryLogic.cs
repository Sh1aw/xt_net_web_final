using System;
using System.Collections.Generic;
using System.Linq;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;
using Epam.ExtPosterStore.Logger;
using log4net;
using log4net.Config;

namespace Epam.ExtPosterStore.BLL
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryDao _categoryDao;
        private readonly IProductDao _productDao;

        public CategoryLogic(ICategoryDao categoryDao, IProductDao productDao)
        {
            _categoryDao = categoryDao;
            _productDao = productDao;
        }

        public int Add(string tittle)
        {
            int response = 400;
            try
            {
                var current =_categoryDao.Add(new Category(tittle));
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

        public IEnumerable<Category> GetAll()
        {
            return _categoryDao.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryDao.GetById(id);
        }

        public int Remove(int id)
        {
            int response = 404;
            if (_categoryDao.GetById(id)!=null)
            {
                if (_productDao.GetAll().Any(p=>p.ProductCategory.Id==id))
                {
                    response = 302;
                }
                else
                {
                    response = _categoryDao.Remove(id);
                }
            }
            return response;
        }

        public int Update(string tittle, int targetId)
        {
            int response = 404;
            if ( _categoryDao.GetById(targetId)!=null)
            {
                try
                {
                    var category = _categoryDao.Update(new Category(tittle), targetId);
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
