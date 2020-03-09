using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class Pay
    {
        private string _tittle;
        public int Id { get; set; }

        public String Tittle
        {
            get => _tittle;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>50)
                {
                    throw new ArgumentException("Incorrect Pay Tittle value");
                }

                _tittle = value;
            }
        }

        public Pay(string tittle)
        {
            Tittle = tittle;
        }
    }
}
