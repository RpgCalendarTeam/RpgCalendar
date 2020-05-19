namespace RPGCalendar.Core.Repositories
{
    using Data.GameObjects;
    using RPGCalendar.Data;

    public interface INoteRepository : IEntityRepository<Note>
    {
    }

    public class NoteRepository : EntityRepository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
