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
    public class PayDao : IPayDao
    {
        private readonly string _connectionString 
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public Pay Add(Pay pay)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pay> GetAll()
        {
            var payTypes = new List<Pay>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllPayTypes";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var payType = new Pay(reader["tittle"] as string);
                        payType.Id = (int)reader["id"];
                        payTypes.Add(payType);
                    }

                }
            }
            return payTypes;
        }

        public Pay GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetPayTypeById";
                Pay payType = null;
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
                        payType = new Pay(reader["tittle"] as string);
                        payType.Id = (int)reader["id"];
                    }

                }

                return payType;
            }
        }
    }
}
