using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class TypeOfProduct
    {
        private string _tittle;
        public int Id { get; set; }

        public string Tittle
        {
            get => _tittle;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length>50)
                {
                    throw  new ArgumentException("Tittle cant be null or empty!");
                }

                _tittle = value;
            }
        }

        public TypeOfProduct(string tittle)
        {
            Tittle = tittle;
        }
    }
}
