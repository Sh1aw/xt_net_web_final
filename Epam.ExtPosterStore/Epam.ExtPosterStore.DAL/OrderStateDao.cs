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
    public class OrderStateDao : IOrderStateDao
    {
        private readonly string _connectionString 
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public IEnumerable<OrderState> GetAll()
        {
            var states = new List<OrderState>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllStates";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var state = new OrderState(reader["tittle"] as string);
                        state.Id = (int)reader["id"];
                        states.Add(state);
                    }

                }
            }
            return states;
        }

        public OrderState GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetStateById";
                OrderState state = null;
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
                        state = new OrderState(reader["tittle"] as string);
                        state.Id = (int)reader["id"];
                    }

                }

                return state;
            }
        }
    }
}
