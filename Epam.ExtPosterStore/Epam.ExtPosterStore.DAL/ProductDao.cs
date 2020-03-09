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
    public class ProductDao : IProductDao
    {
        private readonly string _connectionString 
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public Product Add(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddProduct";

                var tittleParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Tittle",
                    Value = product.Tittle,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(tittleParameter);

                var typeIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@TypeId",
                    Value = product.TypeOfProduct.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(typeIdParameter);

                var categoryIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@CategoryId",
                    Value = product.ProductCategory.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(categoryIdParameter);

                var widthParameter = new SqlParameter()
                {
                    DbType = DbType.Decimal,
                    ParameterName = "@Width",
                    Value = product.Width,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(widthParameter);

                var heightParameter = new SqlParameter()
                {
                    DbType = DbType.Decimal,
                    ParameterName = "@Height",
                    Value = product.Height,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(heightParameter);

                var priceParameter = new SqlParameter()
                {
                    DbType = DbType.Decimal,
                    ParameterName = "@Price",
                    Value = product.Price,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(priceParameter);

                var imgParameter = new SqlParameter()
                {
                    DbType = DbType.Binary,
                    ParameterName = "@Image",
                    Value = product.Image,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(imgParameter);

                var visibilityParameter = new SqlParameter()
                {
                    DbType = DbType.Boolean,
                    ParameterName = "@Visibility",
                    Value = product.Visibility,
                    Direction = ParameterDirection.Input,
                };

                command.Parameters.Add(visibilityParameter);

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
                    product.Id = (int)idParameter.Value;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    product = null;
                }
                
                return product;
            }
        }

        public int Delete(int id)
        {
            int response = 200;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveProductById";
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

        public IEnumerable<Product> GetAll()
        {
            var products = new Dictionary<int,Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllProducts";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var product = new Product(reader["tittle"] as string, new TypeOfProduct(reader["type_tittle"] as string), 
                            new Category(reader["category_tittle"] as string), (decimal)reader["width"], (decimal)reader["height"],
                            (decimal)reader["price"],reader["image"] as byte[],(bool)reader["visibility"]);
                        product.Id = (int)reader["id"];
                        product.TypeOfProduct.Id = (int) reader["id_type"];
                        product.ProductCategory.Id = (int) reader["id_category"];
                        products.Add(product.Id, product);
                    }

                }
            }
            return products.Values;
        }

        public Product GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetProductById";
                Product product = null;
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
                        product = new Product(reader["tittle"] as string, new TypeOfProduct(reader["type_tittle"] as string),
                            new Category(reader["category_tittle"] as string), (decimal)reader["width"], (decimal)reader["height"],
                            (decimal)reader["price"], reader["image"] as byte[],(bool)reader["visibility"]); ;
                        product.Id = (int)reader["id"];
                        product.TypeOfProduct.Id = (int)reader["id_type"];
                        product.ProductCategory.Id = (int)reader["id_category"];
                    }

                }

                return product;
            }
        }

        public int ChangeVisibility(int id, bool visibility)
        {
            int response = 200;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.HideProductById";
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                var visibilityParameter = new SqlParameter()
                {
                    DbType = DbType.Boolean,
                    ParameterName = "@Visibility",
                    Value = visibility,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(visibilityParameter);
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

        public Product Update(Product product, int targetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateProduct";

                var tittleParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Tittle",
                    Value = product.Tittle,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(tittleParameter);

                var typeIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@TypeId",
                    Value = product.TypeOfProduct.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(typeIdParameter);

                var categoryIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@CategoryId",
                    Value = product.ProductCategory.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(categoryIdParameter);

                var widthParameter = new SqlParameter()
                {
                    DbType = DbType.Decimal,
                    ParameterName = "@Width",
                    Value = product.Width,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(widthParameter);

                var heightParameter = new SqlParameter()
                {
                    DbType = DbType.Decimal,
                    ParameterName = "@Height",
                    Value = product.Height,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(heightParameter);

                var priceParameter = new SqlParameter()
                {
                    DbType = DbType.Decimal,
                    ParameterName = "@Price",
                    Value = product.Price,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(priceParameter);

                var imgParameter = new SqlParameter()
                {
                    DbType = DbType.Binary,
                    ParameterName = "@Image",
                    Value = product.Image,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(imgParameter);

                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value =  targetId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                product.Id = targetId;
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    product = null;
                }
                return product;
            }
        }
    }
}
