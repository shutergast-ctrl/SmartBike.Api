namespace SmartBike.Api.Models
{
	public class TelemetryDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
