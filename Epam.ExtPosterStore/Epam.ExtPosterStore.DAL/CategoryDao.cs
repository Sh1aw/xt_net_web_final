using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.DAL
{
    public class CategoryDao : ICategoryDao
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;

        public Category Add(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddCategory";

                var tittleParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Tittle",
                    Value = category.Tittle,
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
                    category.Id = (int) idParameter.Value;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    category = null;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
                return category;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllCategories";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var category = new Category(reader["tittle"] as string);
                        category.Id = (int) reader["id"];
                        categories.Add(category);
                    }

                }
            }

            return categories;
        }

        public Category GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetCategoryById";
                Category category = null;
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
                        category = new Category(reader["tittle"] as string);
                        category.Id = (int) reader["id"];
                    }

                }

                return category;
            }
        }

        public int Remove(int id)
        {
            int response = 200;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveCategoryById";
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
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    response = 500;
                }
            }
            return response;
        }

        public Category Update(Category category, int targetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateCategory";
                var tittleParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Tittle",
                    Value = category.Tittle,
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
                category.Id = targetId;
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    category = null;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
                return category;
            }
        }
    }
}
