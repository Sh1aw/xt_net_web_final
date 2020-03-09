using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;
using System.Web;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface IProductLogic
    {
        //Product Add(Product product);
        //int Add(string pTittle,string pType,string pCategory,string pWidth, string pHeight, string pPrice, HttpPostedFileBase img);
        int Add(string pTittle, int pType, int pCategory, decimal pWidth, decimal pHeight, decimal pPrice, HttpPostedFileBase img);

        int Delete(int id);

        //Product Update(Product product, int targetId);

        int Update(string pTittle, int pType, int pCategory, decimal pWidth, decimal pHeight, decimal pPrice, HttpPostedFileBase img, int targetId);

        IEnumerable<Product> GetAll();

        Product GetById(int id);
        int Hide(int id);
        int Show(int id);
    }
}
