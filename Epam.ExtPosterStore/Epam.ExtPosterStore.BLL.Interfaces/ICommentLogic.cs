using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface ICommentLogic
    {
        int Add(string text, string userEmail,int productId);
        int Update(string text, int targetId);
        int Remove(int id);
        IEnumerable<Comment> GetCommentsForProduct(int productId);
        Comment GetById(int id);
    }
}
