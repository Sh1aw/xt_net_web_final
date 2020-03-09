using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class Role
    {
        private string _name;
        public int Id { get; set; }
        public String Name
        {
            get => _name;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>50)
                {
                    throw new ArgumentException("Incorrect Role Name value");
                }
                _name = value;
            }
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}
