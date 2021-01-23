namespace Rabobank.Training.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Rabobank.Training.ClassLibrary;


    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        //IFundsOfMandates FundMendates;
        //public WeatherForecastController(IFundsOfMandates fundMendatesObj)
        //{
        //    this.FundMendates = fundMendatesObj;
        //}
        //[HttpPost]
        //[Route("UploadFile")]
        //public void Uploadxmlfile() {
        //    var file = Request.Form.Files[0];
        //    using (var reader = new BinaryReader(file.OpenReadStream()))
        //    {
        //        FundMendates.ReadFundOfMandatesFile(reader.ReadBytes((Int32)file.Length));
        //    }
        //}
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
