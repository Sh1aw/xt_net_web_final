using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.DAL
{
    public class ProductOrderDao : IProductOrderDao
    {
        private readonly string _connectionString 
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public IEnumerable<ProductOrder> GetAll()
        {
            var ordersProducts = new List<ProductOrder>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllOrderProducts";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var orderProduct = new ProductOrder((int)reader["id_product"],(int)reader["id_order"],(int)reader["count"]);
                        ordersProducts.Add(orderProduct);
                    }

                }
            }
            return ordersProducts;
        }

        public IEnumerable<CartPair> GetAllProductForOrder(int idOrder)
        {
            var ordersProducts = new List<CartPair>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllProductsForOrder";
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = idOrder,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var orderProduct = new CartPair((int)reader["id_product"],(int)reader["count"]);
                        ordersProducts.Add(orderProduct);
                    }

                }
            }
            return ordersProducts;
        }
    }
}
