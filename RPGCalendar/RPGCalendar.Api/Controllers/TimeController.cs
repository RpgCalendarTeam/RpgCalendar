namespace RPGCalendar.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/Calendar/Time")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        protected ITimeService TimeService { get; }
        public TimeController(ITimeService time)
        {
            TimeService = time ?? throw new ArgumentNullException(nameof(TimeService));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Calendar>> Put([FromBody] TimeChange timeChange)
        {
            var result = await TimeService.ProceedTime(timeChange);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
    }
}
