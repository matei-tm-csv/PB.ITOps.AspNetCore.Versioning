using System;
using System.Collections.Generic;
using System.Linq;
using ApiVersioningExample.Models.v2_0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PB.ITOps.AspNetCore.Versioning;

namespace ApiVersioningExample.Controllers.v2
{
    /// <summary>
    /// WeatherForecast class
    /// </summary>
    [ApiController]
    [IntroducedInApiVersion(2)]
    [RemovedAsOfApiVersion(3)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class WeatherForecastController : BaseWeatherForecastController
    {
        /// <summary>
        /// WeatherForecast constructor
        /// </summary>
        /// <param name="logger">logger</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get the weather forecast (Expand to see remarks)
        /// </summary>
        /// <remarks>This version is changing the </remarks>
        /// <param name="version">The version parameter</param>
        /// <returns>A collection of WeatherForecast v2.0</returns>
        [HttpGet]
        [Obsolete("This version will be removed as soon as v3 will be released")]
        public ActionResult<IEnumerable<WeatherForecast>> Get(ApiVersion version)
        {
            Log(version);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                ExtraInfo = "some extra info " + Summaries[rng.Next(Summaries.Length)].Length
            })
                .ToArray();
        }


    }
}
