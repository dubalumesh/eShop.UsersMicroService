
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using UsersMicroService.Core.Entities;
using UsersMicroService.Core.IRepositories;
using UsersMicroService.Infrastructure.DbFactory;

namespace UsersMicroService.Infrastructure.Repositories
{
    internal class UserRepository(SQLDbFactory dbFactory, ILogger<UserRepository> logger) : IUserRepository
    {
        public async Task AddUserAsync(ApplicationUser user)
        {
            try
            {
                logger.LogInformation("Executing stored procedure to add a new user with email: {Email}", user.Email);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", user.Email, DbType.String);
                parameters.Add("@FirstName", user.FirstName, DbType.String);
                parameters.Add("@LastName", user.LastName, DbType.String);
                parameters.Add("@PasswordHash", user.PasswordHash, DbType.Binary);
                parameters.Add("@PasswordSalt", user.PasswordSalt, DbType.Binary);
                parameters.Add("@Gender", user.Gender, DbType.String);
                string spName = "Sp_InsertUser";
                await dbFactory.Connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
                logger.LogInformation("Executed stored procedure to add a new user with email: {Email}", user.Email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a new user with email: {Email}", user.Email);
                throw;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);
            string spName = "Sp_DeleteUser";
            await dbFactory.Connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            DynamicParameters parameters = new DynamicParameters();
            string spName = "Sp_GetAll";
            return await dbFactory.Connection.QueryAsync<ApplicationUser>(spName, parameters, commandType: CommandType.StoredProcedure);
        }



        public async Task<IEnumerable<ApplicationUser>> GetUserByEmailAsync(string email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);
            string spName = "Sp_GetUserByEmail";
            return await dbFactory.Connection.QueryAsync<ApplicationUser>(spName, parameters, commandType: CommandType.StoredProcedure)!;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);
            string spName = "Sp_GetUserById";
            return await dbFactory.Connection.QueryFirstOrDefaultAsync<ApplicationUser>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.Int32);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@FirstName", user.FirstName, DbType.String);
            parameters.Add("@LastName", user.LastName, DbType.String);
            parameters.Add("@PasswordHash", user.PasswordHash, DbType.Byte);
            parameters.Add("@PasswordSalt", user.PasswordSalt, DbType.Byte);
            parameters.Add("@Gender", user.Gender, DbType.String);
            string spName = "Sp_UpdateUser";
            await dbFactory.Connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
