using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;

namespace NewsViewer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<News>>  Get()
        {
            using (var sqlConnection = new SqlConnection("Server=db;Database=News;User=sa;Password=Your_password123;"))
            {
                sqlConnection.Open();

                var result = await sqlConnection.QueryAsync<News>("select * from news");
                return result;
            }
        }
    }
}
