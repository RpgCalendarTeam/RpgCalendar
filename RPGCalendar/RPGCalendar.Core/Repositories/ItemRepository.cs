namespace RPGCalendar.Core.Repositories
{
    using Data;
    using Data.GameObjects;
    using Services;

    public interface IItemRepository : IEntityRepository<Item>
    {
    }

    public class ItemRepository : EntityRepository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext dbContext, ISessionService sessionService, IGameRepository gameRepository)
            : base(dbContext)
        {
        }
    }
}
