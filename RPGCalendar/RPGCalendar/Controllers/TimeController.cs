namespace RPGCalendar.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Core.Dto;
    using Core.Services;

    [Route("api/Calendar/Time")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        protected ITimeService timeService { get; }
        protected ICalendarService calendarService { get; }
        public TimeController(ITimeService time, ICalendarService calendar)
        {
            timeService = time ?? throw new ArgumentNullException(nameof(timeService));
            calendarService = calendar ?? throw new ArgumentNullException(nameof(calendarService));
        }

        [HttpPut("{id,sec}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Calendar>> Put(int id, long sec)
        {
            var result = await timeService.ProceedTime(id,sec);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
    }
}
