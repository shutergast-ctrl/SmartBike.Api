using System.Threading.Tasks;
using SmartBike.Api.Models;
namespace SmartBike.Api.Data
{
	public interface ITelemetryRepository
	{
		Task<int> InsertTelemetryAsync(TelemetryDto telemetry);
		Task<TelemetryDto?> GetLastTelemetryAsync(int userId);
	}
}