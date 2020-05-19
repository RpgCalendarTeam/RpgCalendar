namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data.GameObjects;
    using Repositories;

    public interface IItemService : IGameObjectService<Dto.Item, Dto.ItemInput>
    {
    }

    public class ItemService : GameObjectService<Dto.Item, Dto.ItemInput, Item, IItemRepository>, IItemService
    {
        public ItemService(IMapper mapper, ISessionService sessionService, IGameService gameService, IItemRepository itemRepository)
            : base(mapper, sessionService, gameService, itemRepository)
        {
        }
    }
}
