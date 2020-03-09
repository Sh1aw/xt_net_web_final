using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface ICommentDao
    {
        Comment Add(Comment comment);
        Comment Update(Comment comment, int targetId);
        int Remove(int id);
        IEnumerable<Comment> GetCommentsForProduct(int productId);
        Comment GetById(int id);
    }
}
