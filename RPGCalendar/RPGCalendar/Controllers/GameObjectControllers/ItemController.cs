﻿namespace RPGCalendar.Controllers.GameObjectControllers
{
    using Core.Dto;
    using Core.Services;

    public class ItemController : BaseApiController<Item, ItemInput>
    {
        public ItemController(IItemService service) :
            base(service)
        {

        }
    }
}
