using System;

namespace Epam.ExtPosterStore.Entities
{
    public class Address
    {
        private string _postCode;
        private string _city;
        private string _street;
        private string _building;

        public int Id { get; set; }
        public string PostCode
        {
            get => _postCode;
            set
            {
                if (value.Length!=6)
                {
                    throw new ArgumentException("Incorrect postcode value, expected 6-char string");
                }

                _postCode = value;
            }
        }
        public string City
        {
            get => _city;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>50)
                {
                    throw new ArgumentException("Incorrect city value");
                }
                _city = value;
            }
        }
        public string Street
        {
            get => _street;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>100)
                {
                    throw new ArgumentException("Incorrect street value");
                }
                _street = value;
            }
        }
        public string Building
        {
            get => _building;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length>20)
                {
                    throw new ArgumentException("Incorrect building value");
                }
                _building = value;
            }
        }

        public Address(string city, string street, string building, string postCode)
        {
            City = city;
            Street = street;
            Building = building;
            PostCode = postCode;
        }
    }
}
