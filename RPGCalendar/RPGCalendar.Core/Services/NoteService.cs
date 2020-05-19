namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data.GameObjects;
    using Repositories;

    public interface INoteService : IGameObjectService<Dto.Note, Dto.NoteInput>
    {
    }

    public class NoteService : GameObjectService<Dto.Note, Dto.NoteInput, Note, INoteRepository>, INoteService
    {
        public NoteService(IMapper mapper,  ISessionService sessionService, IGameService gameService, INoteRepository noteRepository) 
            : base(mapper, sessionService, gameService, noteRepository)
        {
        }
    }
}
