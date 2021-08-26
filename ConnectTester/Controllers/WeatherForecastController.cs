using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using ConnectTester.Helpers;
using System.Net;

namespace ConnectTester.Controllers
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

        static async Task<String> ConnectTest()
        {
            String result = "";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
         
            var client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new Uri("https://login-test.idfy.no/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("/");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await ConnectTest();


            return Content("-----Got the following reply from idfy-----:" + result);
        }
    }
}
