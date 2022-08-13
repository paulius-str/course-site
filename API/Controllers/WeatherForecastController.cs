using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly MySqlConnection _connection;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, MySqlConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    [HttpGet("courses")]
    public async Task<ActionResult<string>> GetCourses()
    {
        await _connection.OpenAsync();

        using var command = new MySqlCommand("SELECT * FROM published_course INNER JOIN course ON published_course.course_id = course.id", _connection);
        using var reader = await command.ExecuteReaderAsync();

        string result ="";

        while (await reader.ReadAsync())
        {
            result += reader.GetString("name") + " ";
            // do something with 'value'  
        }

        return Ok(result.ToString());
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
