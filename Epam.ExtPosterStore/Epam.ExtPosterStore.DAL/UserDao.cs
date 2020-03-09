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
    public class UserDao : IUserDao
    {
        private readonly string _connectionString 
            = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public User Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddUser";

                var emailParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Email",
                    Value = user.Email,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(emailParameter);

                var passwordParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Password",
                    Value = user.Password,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(passwordParameter);

                var nameParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Name",
                    Value = user.Name,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(nameParameter);

                var surnameParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Surname",
                    Value = user.Surname,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(surnameParameter);

                var phoneParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Phone",
                    Value = user.PhoneNumber,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(phoneParameter);

                var roleParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@RoleId",
                    Value = user.Role.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(roleParameter);

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
                    user.Id = (int)idParameter.Value;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    user = null;
                }
                return user;
            }
        }

        public int ChangeRole(Role role, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int resonse = 200;
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.ChangeUserRole";

                var roleParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@RoleId",
                    Value = role.Id,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(roleParameter);

                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = userId,
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
                    resonse = 500;
                }
                return resonse;
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllUsers";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new User(reader["email"] as string, reader["password"] as string, reader["name"] as string,
                            reader["surname"] as string,reader["phone"] as string, new Role(reader["role_name"] as string));
                        user.Id = (int)reader["id"];
                        user.Role.Id = (int) reader["id_role"];
                        users.Add(user);
                    }

                }
            }
            return users;
        }

        public User GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUserById";
                User user = null;
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
                        user = new User(reader["email"] as string, reader["password"] as string, reader["name"] as string,
                            reader["surname"] as string, reader["phone"] as string, new Role(reader["role_name"] as string));
                        user.Id = (int)reader["id"];
                        user.Role.Id = (int)reader["id_role"];
                    }

                }
                return user;
            }
        }

        public User GetByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUserByEmail";
                User user = null;
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Email",
                    Value = email,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User(reader["email"] as string, reader["password"] as string, reader["name"] as string,
                            reader["surname"] as string, reader["phone"] as string, new Role(reader["role_name"] as string));
                        user.Id = (int)reader["id"];
                        user.Role.Id = (int)reader["id_role"];
                    }

                }
                return user;
            }
        }
        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User user, int targetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateUser";

                var emailParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Email",
                    Value = user.Email,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(emailParameter);

                var passwordParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Password",
                    Value = user.Password,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(passwordParameter);

                var nameParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Name",
                    Value = user.Name,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(nameParameter);

                var surnameParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Surname",
                    Value = user.Surname,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(surnameParameter);

                var phoneParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Phone",
                    Value = user.PhoneNumber,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(phoneParameter);

                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = targetId,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParameter);
                user.Id = targetId;
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
                    user = null;
                }
                return user;
            }
        }

        public int UpdatePassword(string newPassword, int targetId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int response = 200;
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UpdateUserPassword";
                var passwordParameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@Password",
                    Value = newPassword,
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(passwordParameter);
                var idParameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@Id",
                    Value = targetId,
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
                return response;
            }
        }
    }
}
