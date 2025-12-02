using Microsoft.AspNetCore.Mvc;
using SmartBike.Api.Data;
using SmartBike.Api.Models;
using System.Threading.Tasks;
namespace SmartBike.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TelemetryController : ControllerBase
	{
		private readonly ITelemetryRepository _repository;
		public TelemetryController(ITelemetryRepository repository)
		{
			_repository = repository;
		}
		/// <summary>
		/// Slaat een nieuwe GPS-meting op in de database.
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> PostTelemetry([FromBody] TelemetryDto dto)
		{
			if (dto == null)
				return BadRequest("Body is leeg.");
			if (dto.UserId <= 0)
				return BadRequest("UserId is ongeldig.");
			var newId = await _repository.InsertTelemetryAsync(dto);
			// 201 Created + simpele payload teruggeven
			return CreatedAtAction(
				nameof(GetLastTelemetry),
				new { userId = dto.UserId },
				new
				{
					Id = newId,
					dto.UserId,
					dto.Longitude,
					dto.Latitude,
					dto.Timestamp
				});
		}
		/// <summary>
		/// Haalt de laatste GPS-meting op voor een specifieke gebruiker.
		/// </summary>
		[HttpGet("last/{userId:int}")]
		public async Task<ActionResult<TelemetryDto>> GetLastTelemetry(int userId)
		{
			if (userId <= 0)
				return BadRequest("UserId is ongeldig.");
			var last = await _repository.GetLastTelemetryAsync(userId);
			if (last == null)
				return NotFound();
			return Ok(last);
		}
	}
}