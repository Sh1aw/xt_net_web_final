using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface ICartDao
    {
        void Add(int id);
        void Remove(int id);
        IEnumerable<CartPair> GetAll();
        int GetAllCountPositions();
        void Clear();
    }
}
