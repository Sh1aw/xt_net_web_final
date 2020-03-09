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
    public class OrderDao : IOrderDao
    {
        private readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public int makeOrder(Order order, IEnumerable<CartPair> products)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int response = 0;

                //Address Part of transaction
                var commandZero = connection.CreateCommand();
                commandZero.CommandType = CommandType.StoredProcedure;
                commandZero.CommandText = "dbo.AddAddress";
                var cityParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@City",
                    Value = order.Address.City,
                    Direction = ParameterDirection.Input,
                };
                commandZero.Parameters.Add(cityParameter);

                var streetParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Street",
                    Value = order.Address.Street,
                    Direction = ParameterDirection.Input,
                };
                commandZero.Parameters.Add(streetParameter);

                var buildingParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Building",
                    Value = order.Address.Building,
                    Direction = ParameterDirection.Input,
                };
                commandZero.Parameters.Add(buildingParameter);

                var postcodeParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Postcode",
                    Value = order.Address.PostCode,
                    Direction = ParameterDirection.Input,
                };
                commandZero.Parameters.Add(postcodeParameter);

                var idAddressParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Direction = ParameterDirection.Output,
                };
                commandZero.Parameters.Add(idAddressParameter);


                //Order Part Of Transaction
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddOrder";
                var dateParameter = new SqlParameter()
                {
                    DbType = DbType.DateTime,
                    ParameterName = "@Date",
                    Value = order.CreationTime,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(dateParameter);
                var userIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@UserId",
                    Value = order.UserId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(userIdParameter);
                var stateIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@StateId",
                    Value = order.OrderState.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(stateIdParameter);

                var costParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@FinalCost",
                    Value = order.FinalCost,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(costParameter);

                var payIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@PayId",
                    Value = order.TypeOfPay.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(payIdParameter);
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Direction = ParameterDirection.Output,
                };
                command.Parameters.Add(idParameter);


                var command2 = connection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "dbo.AddOrderProduct";
                SqlTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    commandZero.Transaction = transaction;
                    commandZero.ExecuteNonQuery();

                    var addressParameter = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@Address",
                        Value = (int)idAddressParameter.Value,
                        Direction = ParameterDirection.Input,
                    };

                    command.Parameters.Add(addressParameter);
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    // ProductOrder Part of Transaction
                    var OrderId = (int) idParameter.Value;
                    foreach (var item in products)
                    {
                        var orderParameter = new SqlParameter()
                        {
                            DbType = DbType.Int32,
                            ParameterName = "@OrderId",
                            Value = OrderId,
                            Direction = ParameterDirection.Input,
                        };
                        command2.Parameters.Add(orderParameter);
                        var productParameter = new SqlParameter()
                        {
                            DbType = DbType.Int32,
                            ParameterName = "@ProductId",
                            Value = item.IdProduct,
                            Direction = ParameterDirection.Input,
                        };
                        command2.Parameters.Add(productParameter);
                        var countParameter = new SqlParameter()
                        {
                            DbType = DbType.Int32,
                            ParameterName = "@Count",
                            Value = item.CountProduct,
                            Direction = ParameterDirection.Input,
                        };
                        command2.Parameters.Add(countParameter);
                        command2.Transaction = transaction;
                        command2.ExecuteNonQuery();
                        command2.Parameters.Clear();
                    }

                    transaction.Commit();
                    response = 200;
                }
                catch (SqlException e)
                {
                    transaction.Rollback();
                    connection.Close();
                    response = 500;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
                return response;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllOrders";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var order = new Order((DateTime) reader["date_time"],
                            (int)reader["id_user"],
                            new OrderState(reader["tittle_state"] as string),
                            (decimal)reader["final_cost"],
                            new Pay(reader["tittle_pay"] as string),
                            new Address(reader["city"] as string,
                                reader["street"] as string, 
                                reader["building"] as string,
                                reader["postcode"] as string));
                        order.Id = (int) reader["id"];
                        order.OrderState.Id = (int)reader["id_state"];
                        order.TypeOfPay.Id = (int)reader["id_pay"];
                        orders.Add(order);
                    }

                }
            }
            return orders;
        }

        public IEnumerable<Order> GetAllOrdersForUser(int userId)
        {
            var orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllOrdersForUser";
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = userId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var order = new Order((DateTime)reader["date_time"],
                            (int)reader["id_user"],
                            new OrderState(reader["tittle_state"] as string),
                            (decimal)reader["final_cost"],
                            new Pay(reader["tittle_pay"] as string),
                            new Address(reader["city"] as string,
                                reader["street"] as string,
                                reader["building"] as string,
                                reader["postcode"] as string));
                        order.Id = (int)reader["id"];
                        order.OrderState.Id = (int)reader["id_state"];
                        order.TypeOfPay.Id = (int)reader["id_pay"];
                        orders.Add(order);
                    }

                }
            }
            return orders;
        }

        public Order GetOrderById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetOrderById";
                Order order = null;
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        order = new Order((DateTime)reader["date_time"],
                            (int)reader["id_user"],
                            new OrderState(reader["tittle_state"] as string),
                            (decimal)reader["final_cost"],
                            new Pay(reader["tittle_pay"] as string),
                            new Address(reader["city"] as string,
                                reader["street"] as string,
                                reader["building"] as string,
                                reader["postcode"] as string));
                        order.Id = (int)reader["id"];
                        order.OrderState.Id = (int)reader["id_state"];
                        order.TypeOfPay.Id = (int)reader["id_pay"];
                    }

                }
                return order;
            }
        }

        public OrderState ChangeOrderState(int idOrder, OrderState orderState)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.ChangeOrderState";
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = idOrder,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);

                var idStateParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@StateId",
                    Value = orderState.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idStateParameter);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    orderState = null;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
            }
            return orderState;
        }
    }
}
