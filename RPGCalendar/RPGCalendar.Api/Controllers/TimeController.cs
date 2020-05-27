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
        protected ICalendarService CalendarRepository { get; }
        public TimeController(ITimeService time, ICalendarService calendar)
        {
            TimeService = time ?? throw new ArgumentNullException(nameof(TimeService));
            CalendarRepository = calendar ?? throw new ArgumentNullException(nameof(CalendarRepository));
        }

        [HttpPut("{id,sec}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Calendar>> Put(int id, long sec)
        {
            var result = await TimeService.ProceedTime(id,sec);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
    }
}
