
namespace RPGCalendar.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController<TDto, TInputDto> : ControllerBase
        where TDto : class, TInputDto
        where TInputDto : class
    {
        protected IEntityService<TDto, TInputDto> Service { get; }

        protected BaseApiController(IEntityService<TDto, TInputDto> service)
        {
            this.Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IEnumerable<TDto>> Get() => await Service.FetchAllAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TDto>> Get(int id)
        {
            try
            {
                TDto? entity = await Service.FetchByIdAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }

                return Ok(entity);
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TDto?>> Put(int id, TInputDto value)
        {
            try
            {
                var result = await Service.UpdateAsync(id, value);
                if (result is null)
                    return NotFound();
                return Ok(result);
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TDto?>> Post(TInputDto entity)
        {
            try
            {
                var result = await Service.InsertAsync(entity);
                return Ok(result);
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await Service.DeleteAsync(id))
                {
                    return Ok();
                }
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }

            return NotFound();
        }
    }
}