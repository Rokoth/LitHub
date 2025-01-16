using DeployBD.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace DeployBD.Services
{
    public interface IDeployService
    {
        bool DeployBD();
    }

    public class DeployService : IDeployService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger logger;

        public DeployService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            logger = serviceProvider.GetService<ILogger<DeployService>>();
        }

        public bool DeployBD()
        {
            try
            {
                var root = "BDpostgres";
                using var _scope = _serviceProvider.CreateScope();
                var options = _scope.ServiceProvider.GetRequiredService<IOptions<CommonOptions>>();
                var conn = options.Value.Connection;
                using var connection = new Npgsql.NpgsqlConnection(
                    $"Host = {conn.Server}; Port = {conn.Port}; Database = postgres; Username = {conn.User}; Password = {conn.Password}");
                connection.Open();
                //using var transaction = connection.BeginTransaction();
                ExecuteQueriesFromDirectory(root, connection);
                //transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        private void ExecuteQueriesFromDirectory(string root, Npgsql.NpgsqlConnection connection)
        {
            if (Directory.Exists(root))
            {
                foreach (var dir in Directory.GetDirectories(root).OrderBy(s => s.Substring(0, s.IndexOf('.'))))
                {
                    ExecuteQueriesFromDirectory(dir, connection);
                }
                foreach (var file in Directory.GetFiles(root).OrderBy(s => s.Substring(0, s.IndexOf('.'))))
                {
                    using var command = connection.CreateCommand();
                    using var reader = new StreamReader(file);
                    command.CommandText = reader.ReadToEnd();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
