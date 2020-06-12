namespace RPGCalendar.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using AutoMapper;
    using Data.GameObjects;
    using Repositories;

    public interface IItemService : IGameObjectService<Dto.Item, Dto.ItemInput>
    {
        public Task<List<Dto.Item>> FetchItemsByUser(int UserId);
    }

    public class ItemService : GameObjectService<Dto.Item, Dto.ItemInput, Item, IItemRepository>, IItemService
    {
        public ItemService(IMapper mapper, ISessionService sessionService, IGameService gameService, IItemRepository itemRepository)
            : base(mapper, sessionService, gameService, itemRepository)
        {
        }
        public async Task<List<Dto.Item>> FetchItemsByUser(int UserId)
        {
            var gameId = _sessionService.GetCurrentGameId();
            var items = (await FetchAllAsync())
                .Where(a => a.UserId == UserId);
            return items.ToList();
        }
    }
}
