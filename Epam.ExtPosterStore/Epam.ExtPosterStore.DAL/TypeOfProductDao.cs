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
    public class TypeOfProductDao : ITypeOfProductDao
    {
        private readonly string _connectionString 
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public TypeOfProduct Add(TypeOfProduct type)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddType";

                var tittleParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Tittle",
                    Value = type.Tittle,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(tittleParameter);

                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Direction = ParameterDirection.Output,
                };
                command.Parameters.Add(idParameter);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    type.Id = (int) idParameter.Value;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    type = null;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
                return type;
            }
        }

        public IEnumerable<TypeOfProduct> GetAll()
        {
            var types= new List<TypeOfProduct>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllTypes";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = new TypeOfProduct(reader["tittle"] as string);
                        type.Id = (int)reader["id"];
                        types.Add(type);
                    }

                }
            }
            return types;
        }

        public TypeOfProduct GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetTypeById";
                TypeOfProduct type = null;
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
                        type = new TypeOfProduct(reader["tittle"] as string);
                        type.Id = (int)reader["id"];
                    }

                }

                return type;
            }
        }

        public int Remove(int id)
        {
            int response = 200;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveTypeById";
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    response = 500;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
            }
            return response;
        }

        public TypeOfProduct Update(TypeOfProduct type, int targetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateType";
                var tittleParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Tittle",
                    Value = type.Tittle,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(tittleParameter);


                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = targetId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                type.Id = targetId;
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    type = null;
                }
                return type;
            }
        }
    }
}
