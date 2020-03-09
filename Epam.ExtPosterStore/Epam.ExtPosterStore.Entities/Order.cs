using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class Order
    {
        private decimal _finalCost;
        private Pay _typeOfPay;
        private OrderState _orderState;
        private Address _address;
        public int Id { get; set; }
        public DateTime CreationTime { get;}
        public int UserId { get;}
        public OrderState OrderState
        {
            get => _orderState;
            set => _orderState = value ?? 
                                 throw new ArgumentNullException(nameof(value));
        }
        public decimal FinalCost
        {
            get => _finalCost;
            set
            {
                if (value<=0)
                {
                    throw new ArgumentOutOfRangeException("incorrect cost param");
                }

                _finalCost = value;
            }
        }
        public Pay TypeOfPay
        {
            get => _typeOfPay;
            set => _typeOfPay = value ?? 
                                throw new ArgumentNullException("Type of Pay parameter can`t be null");
        }
        public Address Address
        {
            get => _address;
            set => _address = value ??
                              throw new ArgumentNullException("Adress parameter can`t be null!");
        }

        public Order(DateTime creationTime,int userId,OrderState orderState, decimal finalCost, Pay typeOfPay, Address address)
        {
            CreationTime = creationTime;
            UserId = userId;
            OrderState = orderState;
            FinalCost = finalCost;
            TypeOfPay = typeOfPay;
            Address = address;
        }
    }
}
