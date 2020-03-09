using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class Category
    {
        private string _tittle;
        public int Id { get; set; }
        public String Tittle
        {
            get => _tittle;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tittle cant be empty or null");
                }

                _tittle = value;
            }
        }

        public Category(string tittle)
        {
            Tittle = tittle;
        }
    }
}
