using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using moo_server.Core.DAL.Interfaces;
using moo_server.Core.Entities;

namespace moo_server.Core.DAL
{
    public class UsersRepository : IUsersRepository
    {
        private string connectionString;

        public UsersRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public User GetOrCreateUserByTgId(UserModel userModel)
        {
            try
            {
                User user;
                using (var con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var parameter = new DynamicParameters();
                    parameter.Add("@TgId", userModel.TgId);
                    parameter.Add("@TgUsername", userModel.TgUsername);
                    parameter.Add("@TgFirstName", userModel.TgFirstName);
                    parameter.Add("@TgLastName", userModel.TgLastName);
                    parameter.Add("@TgLanguageCode", userModel.TgLanguageCode);
                    user = con.Query<User>("spGetOrCreateUserByTgId", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetUser(long tgId)
        {
            try
            {
                User user;
                using (var con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var parameter = new DynamicParameters();
                    parameter.Add("@TgId", tgId);
                    user = con.Query<User>("spGetUser", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var parameter = new DynamicParameters();
                    parameter.Add("@Id", user.Id);
                    parameter.Add("@TgId", user.TgId);
                    parameter.Add("@TgUsername", user.TgUsername);
                    parameter.Add("@TgFirstName", user.TgFirstName);
                    parameter.Add("@TgLastName", user.TgLastName);
                    parameter.Add("@TgLanguageCode", user.TgLanguageCode);
                    parameter.Add("@LastMooDate", user.LastMooDate);
                    parameter.Add("@MooCount", user.MooCount);
                    con.Query("spUpdateUser", parameter, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateUserMooCountAndLastMooDateById(User user)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var parameter = new DynamicParameters();
                    parameter.Add("@Id", user.Id);
                    parameter.Add("@LastMooDate", user.LastMooDate);
                    parameter.Add("@MooCount", user.MooCount);
                    con.Query("spUpdateUserMooCountAndLastMooDateById", parameter, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
