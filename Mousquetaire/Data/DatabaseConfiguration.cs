using Microsoft.Data.SqlClient;

namespace Mousquetaire.Data;

public static class DatabaseConfiguration
{
    public static string ConnectionString
    {
        get
        {
            var server = Environment.GetEnvironmentVariable("PREPORDER_SQL_SERVER");
            if (string.IsNullOrWhiteSpace(server))
            {
                server = Environment.MachineName;
            }

            return new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = "PrepOrder",
                IntegratedSecurity = true,
                Encrypt = true,
                TrustServerCertificate = true
            }.ConnectionString;
        }
    }
}
