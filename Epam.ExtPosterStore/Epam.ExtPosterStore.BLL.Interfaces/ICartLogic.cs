using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface ICartLogic
    {
        bool Add(int id);
        void Remove(int id);
        IEnumerable<CartPair> GetAll();
        int GetAllCountPositions();
        decimal GetFinalCost();
        void Clear();
    }
}
