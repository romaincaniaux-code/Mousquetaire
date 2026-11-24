using Microsoft.Data.SqlClient;

namespace Mousquetaire.Data;

public static class StoredProcedureExecutor
{
    public static async Task<T?> QuerySingleAsync<T>(
        string procedureName,
        Func<SqlDataReader, T> map,
        params SqlParameter[] parameters)
        where T : class
    {
        await using var connection = new SqlConnection(DatabaseConfiguration.ConnectionString);

        await using var command = new SqlCommand(procedureName, connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        var result = map(reader);

        if (await reader.ReadAsync())
        {
            throw new InvalidOperationException(
                $"La procédure stockée {procedureName} a retourné plusieurs lignes.");
        }

        return result;
    }
}
