using Microsoft.Data.SqlClient;

namespace Mousquetaire.Data;

public static class StoredProcedureExecutor
{
    public static Task<IReadOnlyList<T>> QueryListAsync<T>(
        string procedureName,
        Func<SqlDataReader, T> map,
        params SqlParameter[] parameters)
    {
        return ExecuteReaderAsync<IReadOnlyList<T>>(
            procedureName,
            async reader =>
            {
                var results = new List<T>();
                while (await reader.ReadAsync())
                {
                    results.Add(map(reader));
                }

                return results;
            },
            parameters);
    }

    public static Task<T?> QuerySingleAsync<T>(
        string procedureName,
        Func<SqlDataReader, T> map,
        params SqlParameter[] parameters)
        where T : class
    {
        return ExecuteReaderAsync<T?>(
            procedureName,
            async reader =>
            {
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
            },
            parameters);
    }

    private static async Task<TResult> ExecuteReaderAsync<TResult>(
        string procedureName,
        Func<SqlDataReader, Task<TResult>> readResult,
        SqlParameter[] parameters)
    {
        await using var connection = new SqlConnection(DatabaseConfiguration.ConnectionString);
        await using var command = new SqlCommand(procedureName, connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        return await readResult(reader);
    }
}
