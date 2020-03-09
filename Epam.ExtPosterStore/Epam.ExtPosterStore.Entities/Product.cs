using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class Product
    {
        private decimal _width;
        private decimal _height;
        private decimal _price;
        private string _tittle;
        private TypeOfProduct _typeOfProduct;
        private Category _productCategory;

        public int Id { get; set; }
        public string Tittle
        {
            get => _tittle;
            set
            {
                if (String.IsNullOrWhiteSpace(value)||value.Length>50)
                {
                    throw new ArgumentException("Incorrect tittle value");
                }

                _tittle = value;
            }
        }
        public TypeOfProduct TypeOfProduct
        {
            get => _typeOfProduct;
            set => _typeOfProduct = value ??
                                    throw new ArgumentNullException(nameof(value));
        }
        public Category ProductCategory
        {
            get => _productCategory;
            set => _productCategory = value ?? 
                                      throw new ArgumentNullException(nameof(value));
        }
        public decimal Width
        {
            get => _width;
            set
            {
                if (value<=0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"Incorrect Width Value");
                }
                _width = value;
            }
        }
        public decimal Height 
        { 
            get => _height;
            set
            {
                if (value<=0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"Incorrect height value");
                }

                _height = value;
            }

        }
        public byte[] Image { get; set; }
        public decimal Price
        {
            get => _price;
            set
            {
                if (value<=0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"Incorrect Price value");
                }

                _price = value;
            }
        }
        public bool Visibility { get; set; }

        public Product(string tittle, TypeOfProduct type, Category category, decimal width, decimal height, decimal price, byte[] imgBytes, bool visibility)
        {
            Tittle = tittle;
            TypeOfProduct = type;
            ProductCategory = category;
            Width = width;
            Height = height;
            Image = imgBytes;
            Price = price;
            Visibility = visibility;
        }
    }
}
