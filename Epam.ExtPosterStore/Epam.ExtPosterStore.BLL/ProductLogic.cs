using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.ExtPosterStore.BLL.Common;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class ProductLogic : IProductLogic
    {

        private readonly IProductDao _productDao;
        private readonly ITypeOfProductDao _typeOfProductDao;
        private readonly ICategoryDao _categoryDao;
        private readonly IProductOrderDao _productOrderDao;

        public ProductLogic(IProductDao productDao, ITypeOfProductDao typeOfProductDao,
                            ICategoryDao categoryDao, IProductOrderDao productOrderDao)
        {
            _productDao = productDao;
            _typeOfProductDao = typeOfProductDao;
            _categoryDao = categoryDao;
            _productOrderDao = productOrderDao;
        }
        public int Add(string pTittle, int pType, int pCategory, decimal pWidth,
                        decimal pHeight, decimal pPrice, HttpPostedFileBase img)
        {
            if (img.ContentLength == 0|| ImageValidation.Validate(img.ContentType,img.ContentLength))
            {
                var typeObj = _typeOfProductDao.GetById(pType);
                var categoryObj = _categoryDao.GetById(pCategory);
                pTittle = pTittle.Trim();
                byte[] imgBytes = null;
                if (img.ContentLength>0)
                {
                    imgBytes = ImageConverter.ConvertToBinary(img);
                }
                try
                {
                    var current = _productDao.Add(new Product(pTittle, typeObj, categoryObj,
                                        pWidth, pHeight, pPrice, imgBytes, true));
                    if (current != null)
                    {
                        return 200; // successful added to db;
                    }
                }   
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    return 400;
                }
            }
            return 400; //wrong image format;
        }

        public int Delete(int id)
        {
            if (_productOrderDao.GetAll().Any(p=>p.IdProduct==id))
            {
                return 300;
            }
            else
            {
                return _productDao.Delete(id);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _productDao.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDao.GetById(id);
        }

        public int Hide(int id)
        {
            return _productDao.ChangeVisibility(id,false);
        }

        public int Show(int id)
        {
            return _productDao.ChangeVisibility(id, true);
        }

        public int Update(string pTittle, int pType, int pCategory, decimal pWidth,
            decimal pHeight, decimal pPrice, HttpPostedFileBase img, int targetId)
        {
            if (img.ContentLength == 0 || ImageValidation.Validate(img.ContentType, img.ContentLength))
            {
                var currentProduct = _productDao.GetById(targetId);
                if (currentProduct==null)
                {
                    return 404;//product not found
                }
                var typeObj = _typeOfProductDao.GetById(pType);
                var categoryObj = _categoryDao.GetById(pCategory);
                pTittle = pTittle.Trim();
                byte[] imgBytes = null;
                if (img.ContentLength > 0)
                {
                    imgBytes = ImageConverter.ConvertToBinary(img);
                }
                try
                {
                    var current = _productDao.Update(new Product(pTittle, typeObj, categoryObj, pWidth,
                                        pHeight, pPrice, imgBytes, true), targetId);
                    if (current != null)
                    {
                        return 200; // successful added to db;
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    return 400;
                }
            }
            return 400; //wrong image format;
        }
    }
}
