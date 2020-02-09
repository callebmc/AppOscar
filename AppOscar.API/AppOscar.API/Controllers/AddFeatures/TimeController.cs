using AppOscar.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.AddFeatures
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly AppOscarContext context;


        public TimeController(AppOscarContext context)  {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetQuantoFalta()
        {
            try
            {
                DateTime _timeDataOscar = new DateTime(2020, 2, 9, 20, 30, 00);
                DateTime _timeNow = DateTime.Now;
                TimeSpan time = _timeDataOscar.Subtract(_timeNow);
                return new OkObjectResult((int)time.TotalSeconds);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
