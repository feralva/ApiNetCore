using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPruebaController : ControllerBase
    {
        private ILogger<LogPruebaController> _logger;

        public LogPruebaController(ILogger<LogPruebaController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Prueba Log
        /// </summary>
        /// <remarks>
        /// No Requiere Parametros
        ///
        /// </remarks>
        [HttpPost]
        [Route("[action]")]
        public ActionResult Log()
        {

            _logger.LogError(new Exception("Mensaje Excepcion"), "Error desde codigo");
            //cli.TrackMetric(new MetricTelemetry() { });
            //System.Diagnostics.Trace.TraceError("If you're seeing this, something bad happened");
            return null;
        }
    }
}