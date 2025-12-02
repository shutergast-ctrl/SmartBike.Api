using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SmartBike.Api.Models;


namespace SmartBike.Api.Data
{
	public class TelemetryRepository : ITelemetryRepository
	{
		private readonly string _connectionString;

		public TelemetryRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("SmartBikeDb");

		}

		public async Task<int> InsertTelemetryAsync(TelemetryDto telemetry)
		{
			const string sql = @"
				INSERT INTO Telemetry (UserId, Longitude, Latitude, Timestamp)
				VALUES (@UserId, @Longitude, @Latitude, @Timestamp);
				SELECT SCOPE_IDENTITY();";

			using var connection = new SqlConnection(_connectionString);
			using var command = new SqlCommand(sql, connection);

			command.Parameters.AddWithValue("@UserId", telemetry.UserId);
			command.Parameters.AddWithValue("@Longitude", telemetry.Longitude);
			command.Parameters.AddWithValue("@Latitude", telemetry.Latitude);
			command.Parameters.AddWithValue("@Timestamp", telemetry.Timestamp);

			await connection.OpenAsync();

			var result = await command.ExecuteScalarAsync();
			return Convert.ToInt32(result);
		}

		public async Task<TelemetryDto?> GetLastTelemetryAsync(int userId)
		{
			const string sql = @"
                SELECT TOP 1 Id, UserId, Longitude, Latitude, Timestamp
                FROM Telemetry
                WHERE UserId = @UserId
                ORDER BY Timestamp DESC;
            ";

			using var conn = new SqlConnection(_connectionString);
			using var cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@UserId", userId);

			await conn.OpenAsync();
			using var reader = await cmd.ExecuteReaderAsync();

			if (!reader.Read())
				return null;

			return new TelemetryDto
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				Longitude = reader.GetDouble(2),
				Latitude = reader.GetDouble(3),
				Timestamp = reader.GetDateTime(4)
			};
		}
	}
}
