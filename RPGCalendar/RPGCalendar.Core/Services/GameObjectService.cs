namespace RPGCalendar.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.GameObjects;
    using Exceptions;
    using Repositories;

    public interface IGameObjectService<TDto, TInputDto> : IEntityService<TDto, TInputDto>
        where TInputDto : class
        where TDto : class, TInputDto
    {
    }
    public abstract class GameObjectService<TDto, TInputDto, TGameEntity, TEntityRepository> 
        : EntityService<TDto, TInputDto, TGameEntity, TEntityRepository>, IGameObjectService<TDto, TInputDto>
        where TEntityRepository : IEntityRepository<TGameEntity>
        where TGameEntity : GameObject
        where TDto : class, TInputDto
        where TInputDto : class
    {
        private readonly ISessionService _sessionService;
        private readonly IGameService _gameService;

        protected GameObjectService(IMapper mapper, 
            ISessionService sessionService, 
            IGameService gameService,
            TEntityRepository entityRepository)
            : base(mapper, entityRepository)
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
            var filteredObjects = (await EntityRepository.FetchAllAsync())
                                                    .Where(obj => obj.GameId == gameId);
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
            entity.Game = await _gameService.GetById(gameId) ?? throw new ArgumentNullException(nameof(gameId));
            entity.GameId = gameId;
            await EntityRepository.InsertAsync(entity);
            return Mapper.Map<TGameEntity, TDto>(entity);
        }

        private async Task<bool> UserIsInGame()
        {
            var userId = _sessionService.GetCurrentUserId();
            var gameId = _sessionService.GetCurrentGameId();
            var game = await _gameService.GetById(gameId);
            return game?.IsInGame(userId) ?? false;
        }



    }
}
