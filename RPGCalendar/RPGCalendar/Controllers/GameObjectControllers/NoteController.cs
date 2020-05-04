
namespace RPGCalendar.Controllers.GameObjectControllers
{
    using Core.Dto;
    using Core.Services;

    public class NoteController : BaseApiController<Note, NoteInput>
    {
        public NoteController(INoteService service) : 
            base(service)
        { }
    }
}