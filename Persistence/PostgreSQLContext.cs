using Npgsql;
using Serilog.Core;

// ReSharper disable InconsistentlySynchronizedField

namespace GtfsDtlk_ToDtwh.Persistence;

/// <summary>
///     Context for the database
/// </summary>
public class PostgreSqlContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PostgreSqlContext" /> class.
    /// </summary>
    /// <param name="dbSettings"></param>
    public PostgreSqlContext(DbSettings dbSettings)
    {
        DbSettings = dbSettings;
        ConnectionString = $"Host={DbSettings.Server}; " +
                           $"Port={DbSettings.Port}; " +
                           $"Database={DbSettings.Database}; " +
                           $"User Id={DbSettings.Username}; " +
                           $"Password={DbSettings.Password};";
    }

    public DbSettings DbSettings { get; }
    public string ConnectionString { get; }

    /// <summary>
    ///     Connect to context DB
    /// </summary>
    /// <returns></returns>
    public NpgsqlConnection DbConnection()
    {
        try
        {
            var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async void CommandQuery(string query, Logger log)
    {
        var dbConnect = DbConnection();
        try
        {
            var cmd = new NpgsqlCommand(query, dbConnect);
            await cmd.ExecuteNonQueryAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
            log.Error($"QUERY : {query}");
        }

        await dbConnect.CloseAsync().ConfigureAwait(false);
    }

    public async Task<List<string?[]>> SelectQuery(string query, Logger log)
    {
        var result = new List<string?[]>();
        var dbConnect = DbConnection();
        try
        {
            var cmd = new NpgsqlCommand(query, dbConnect);
            var reader = await cmd.ExecuteReaderAsync(CancellationToken.None);

            while (reader.Read())
            {
                var row = new string?[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++) row[i] = Convert.ToString(reader[i]);
                result.Add(row);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.Message);
            log.Error($"QUERY : {query}");
        }

        await dbConnect.CloseAsync().ConfigureAwait(false);
        return result;
    }
}