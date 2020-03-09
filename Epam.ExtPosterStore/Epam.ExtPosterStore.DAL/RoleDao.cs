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
    public class RoleDao : IRoleDao
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public Role Add(Role role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            var roles = new List<Role>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllRoles";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var role = new Role(reader["name"] as string);
                        role.Id = (int)reader["id"];
                        roles.Add(role);
                    }

                }
            }

            return roles;
        }

        public Role GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetRoleById";
                Role role = null;
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
                        role = new Role(reader["name"] as string);
                        role.Id = (int)reader["id"];
                    }

                }
                return role;
            }
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Role Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
