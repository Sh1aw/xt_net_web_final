using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class OrderState
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
                    throw new ArgumentException("Tittle parameter can`t be empty, or more 50 characters");
                }

                _tittle = value;
            }
        }

        public OrderState(string tittle)
        {
            Tittle = tittle;
        }
    }
}
