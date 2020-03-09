using System;
using System.Collections.Generic;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class CommentLogic : ICommentLogic
    {
        private readonly ICommentDao _commentDao;
        private readonly IProductDao _productDao;
        private readonly IUserDao _userDao;

        public CommentLogic(ICommentDao commentDao, IProductDao productDao, IUserDao userDao)
        {
            _commentDao = commentDao;
            _userDao = userDao;
            _productDao = productDao;
        }
        public int Add(string text, string email, int productId)
        {
            var user = _userDao.GetByEmail(email);
            var product = _productDao.GetById(productId);
            if (user!=null && product!=null)
            {
                try
                {
                    var comment = _commentDao.Add(new Comment(text, user.Id, productId, DateTime.Now));
                    if (comment!=null)
                    {
                        return 200;
                    }

                    return 500;
                }
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    return 400;
                }
            }
            return 404;
        }

        public Comment GetById(int id)
        {
            return _commentDao.GetById(id);
        }

        public IEnumerable<Comment> GetCommentsForProduct(int productId)
        {
            return _commentDao.GetCommentsForProduct(productId);
        }

        public int Remove(int id)
        {
            if (_commentDao.GetById(id)!=null)
            {
                return _commentDao.Remove(id);
            }

            return 404;
        }

        public int Update(string text, int targetId)
        { 
            var comment = _commentDao.GetById(targetId);
            if (comment!=null)
            {
                try
                {
                    comment.Text = text;
                    var answer = _commentDao.Update(comment, targetId);
                    if (answer != null)
                    {
                        return 200;
                    }
                    return 500;
                }
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    return 400;
                }
            }
            return 404;
        }
    }
}
