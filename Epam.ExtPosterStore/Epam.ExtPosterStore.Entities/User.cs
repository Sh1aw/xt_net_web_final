using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class User
    {
        private string _email;
        private string _name;
        private string _surname;
        private string _phoneNumber;
        public int Id { get; set; }
        public string Email
        {
            get => _email;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>100)
                {
                    throw new ArgumentException("Incorrect Email Value");
                }

                _email = value;
            }
        }
        public string Password { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>50)
                {
                    throw new ArgumentException("Incorrect Name Value");
                }

                _name = value;
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>50)
                {
                    throw new ArgumentException("Incorrect Surname Value");
                }

                _surname = value;
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value.Length>11)
                {
                    throw new ArgumentException("Incorrect PhoneNumber Value");
                }

                _phoneNumber = value;
            }
        }
        public Role Role { get; set; }

        public User(string email, string password, string name, string surname, string phone, Role role)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phone;
            Role = role;
        }
    }
}
