namespace RPGCalendar.Api.Controllers.GameObjectControllers
{
    using System.Collections.Generic;
    using Core.Dto;
    using Core.Services;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ItemController : BaseApiController<Item, ItemInput>
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService service) :
            base(service)
        {
            _itemService = service;
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<List<Item>> GetPlayerInvetory(int id) => await _itemService.FetchItemsByUser(id);
    }
}
