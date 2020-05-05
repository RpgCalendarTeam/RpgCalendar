namespace RPGCalendar.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Data.GameObjects;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;

    public interface IGameObjectService<TDto, TInputDto> : IEntityService<TDto, TInputDto>
        where TInputDto : class
        where TDto : class, TInputDto
    {
    }
    public abstract class GameObjectService<TDto, TInputDto, TGameEntity> : EntityService<TDto, TInputDto, TGameEntity>, IGameObjectService<TDto, TInputDto>
        where TGameEntity : GameObject
        where TDto : class, TInputDto
        where TInputDto : class
    {
        private readonly ISessionService _sessionService;
        private readonly IGameService _gameService;

        protected GameObjectService(ApplicationDbContext dbContext, 
            IMapper mapper, 
            ISessionService sessionService, 
            IGameService gameService) 
            : base(dbContext, mapper)
        {
            _sessionService = sessionService;
            _gameService = gameService;
        }

        public override async Task<TDto?> UpdateAsync(int id, TInputDto entity)
        {
            
            if (!await UserIsInGame())
                throw new UserPermissionException("Update permission denied");
            return await base.UpdateAsync(id, entity);
        }

        public override async Task<List<TDto>> FetchAllAsync()
        {
            if (!await UserIsInGame())
                throw new UserPermissionException("Read permission denied");
            var gameId = _sessionService.GetCurrentGameId();
            var filteredObjects =
                (await Query.Where(o => o.GameId == gameId).ToListAsync());
            return Mapper.Map<List<TGameEntity>, List<TDto>>(filteredObjects.ToList());
        }

        public override async Task<TDto?> FetchByIdAsync(int id)
        {
            if (! await UserIsInGame())
                throw new UserPermissionException("Read permission denied");

            return await base.FetchByIdAsync(id);
        }

        public override async Task<TDto?> InsertAsync(TInputDto dto)
        {
            if (!(await UserIsInGame()))
                throw new UserPermissionException("Update permission denied");
            TGameEntity entity = Mapper.Map<TInputDto, TGameEntity>(dto);
            int gameId = _sessionService.GetCurrentGameId();
            entity.Game = await _gameService.GetGameById(gameId) ?? throw new ArgumentNullException(nameof(gameId));
            entity.GameId = gameId;
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<TGameEntity, TDto>(entity);
        }

        private async Task<bool> UserIsInGame()
        {
            var userId = _sessionService.GetCurrentUserId();
            var gameId = _sessionService.GetCurrentGameId();
            return (await _gameService.GetGameById(gameId)).IsInGame(userId);
        }



    }
}
