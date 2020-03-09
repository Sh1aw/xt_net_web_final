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
    public class CommentDao : ICommentDao
    {
        private readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public Comment Add(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddComment";

                var textParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Text",
                    Value = comment.Text,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(textParameter);

                var userIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@UserId",
                    Value = comment.UserId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(userIdParameter);

                var productIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@ProductId",
                    Value = comment.ProductId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(productIdParameter);

                var creationTimeParameter = new SqlParameter()
                {
                    DbType = DbType.DateTime,
                    ParameterName = "@CreationTime",
                    Value = comment.CreationTime,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(creationTimeParameter);

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
                    comment.Id = (int)idParameter.Value;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    comment = null;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
                return comment;
            }
        }

        public Comment GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetCommentById";
                Comment comment = null;
                var IdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(IdParameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comment = new Comment(reader["text"] as string, (int)reader["id_user"],
                                (int)reader["id_product"], (DateTime)reader["date_time"]);
                        comment.Id = (int)reader["id"];
                    }

                }
                return comment;
            }
        }

        public IEnumerable<Comment> GetCommentsForProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetCommentsForProduct";
                var comments = new List<Comment>();
                var productIdParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@ProductId",
                    Value = productId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(productIdParameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var comment = new Comment(reader["text"] as string,(int)reader["id_user"],
                            (int)reader["id_product"],(DateTime)reader["date_time"]);
                        comment.Id = (int)reader["id"];
                        comments.Add(comment);
                    }

                }
                return comments;
            }
        }

        public int Remove(int id)
        {
            int response = 200;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveCommentById";
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

        public Comment Update(Comment comment, int targetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateComment";

                var textParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Text",
                    Value = comment.Text,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(textParameter);

                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = targetId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                comment.Id = targetId;
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    comment = null;
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                }
                return comment;
            }
        }
    }
}
