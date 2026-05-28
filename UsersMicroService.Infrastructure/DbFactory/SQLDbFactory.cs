using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Data;


namespace UsersMicroService.Infrastructure.DbFactory
{
    public class SQLDbFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public SQLDbFactory(IConfiguration configuration)
        {
            _configuration = configuration;

            string connectionTemplate = _configuration.GetConnectionString("SQLConnection")!;

            connectionTemplate = connectionTemplate.Replace("$SQL_SERVER_HOST", Environment.GetEnvironmentVariable("SQL_SERVER_HOST") ?? "localhost");
            connectionTemplate = connectionTemplate.Replace("$DB", Environment.GetEnvironmentVariable("SQL_DB") ?? "eShopUsersDB");
            connectionTemplate = connectionTemplate.Replace("$SQL_USER", Environment.GetEnvironmentVariable("SQL_USER") ?? "sa");
            connectionTemplate = connectionTemplate.Replace("$SQL_PASSWORD", Environment.GetEnvironmentVariable("SQL_PASSWORD") ?? "admiN@123");

            string connectionString = connectionTemplate;

            _dbConnection = new SqlConnection(connectionString);

        }

        public IDbConnection Connection => _dbConnection;


    }
}
