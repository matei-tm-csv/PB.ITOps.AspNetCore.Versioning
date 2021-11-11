using System;
using System.Collections.Generic;
using System.Linq;
using ApiVersioningExample.Models.v1_0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PB.ITOps.AspNetCore.Versioning;

namespace ApiVersioningExample.Controllers.v1
{
    /// <summary>
    /// WeatherForecast class
    /// If the ApiVersion is superseeded by another version switch the Deprecated property to true
    /// </summary>
    [ApiController]
    [IntroducedInApiVersion(1)]
    [RemovedAsOfApiVersion(2)]
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
        /// <remarks>The method does not need to be explicitly decorated with version.
        /// <br /> It inherits the attribute from the class level<br />
        /// Notify the behavior to the Obsolete attribute
        /// </remarks>
        /// <param name="version">The version parameter</param>
        /// <returns>A collection of WeatherForecast</returns>
        [HttpGet]
        [Obsolete("This version will be removed as soon as v3 will be released")]
        public ActionResult<IEnumerable<WeatherForecast>> Get(ApiVersion version)
        {
            Log(version);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = 451,
                Summary = BaseWeatherForecastController.Summaries[rng.Next(BaseWeatherForecastController.Summaries.Length)]
            })
            .ToArray();
        }
    }
}
