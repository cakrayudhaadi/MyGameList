using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Features.Characters.Services;
using MyGameList.Src.Features.GameMakers.Services;
using MyGameList.Src.Features.Games.Dtos;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Games.Repositories;
using MyGameList.Src.Shared.Commons;
using MyGameList.Src.Shared.Utils;
using System.Net;

namespace MyGameList.Src.Features.Games.Services
{
    public interface IGameService
    {
        Task<Response> AddGameAsync(GameDto gameDto);
        Task<Response<List<GameCoverResponseDto>>> GetAllGamesAsync();
        Task<Response<GameResponseDto>> GetGameByIdAsync(int id);
        Task<Response> UpdateGameAsync(int id, GameDto gameDto);
        Task<Response> DeleteGameAsync(int id);
    }

    public class GameService(IGameRepository gameRepo,
        IAgeRatingService ageRatingService,
        IDeveloperService developerService,
        IPublisherService publisherService,
        IPersonService personService,
        IGenreService genreService,
        IModeService modeService,
        IPlatformService platformService,
        IGameCharacterService gameCharacterService) : IGameService
    {
        public async Task AddOrUpdateGameFromDto(Game game, GameDto gameDto, int? id)
        {
            await Util.AddCollectionProperties(game.AgeRatings, gameDto.AgeRatingIds, ageRatingService.GetAgeRatingListByIds);
            await Util.AddCollectionProperties(game.Developers, gameDto.DeveloperIds, developerService.GetDeveloperListByIds);
            await Util.AddCollectionProperties(game.Publishers, gameDto.PublisherIds, publisherService.GetPublisherListByIds);
            await Util.AddCollectionProperties(game.Producers, gameDto.ProducerIds, personService.GetPersonListByIds);
            await Util.AddCollectionProperties(game.Genres, gameDto.GenreIds, genreService.GetGenreListByIds);
            await Util.AddCollectionProperties(game.Modes, gameDto.ModeIds, modeService.GetModeListByIds);
            await Util.AddCollectionProperties(game.Platforms, gameDto.PlatformIds, platformService.GetPlatformListByIds);
            await Util.AddCollectionProperties(game.Characters, gameDto.CharacterIds, gameCharacterService.GetCharacterListByIds);

            if (!id.HasValue)
            {
                await gameRepo.AddAsync(game);
            }
            else
            {
                await gameRepo.UpdateDataAsync(game);
            }
        }

        public async Task<Response> AddGameAsync(GameDto gameDto)
        {
            ArgumentNullException.ThrowIfNull(gameDto);

            string? errValidation = gameDto.GameValidation();
            if (errValidation is not null)
                return new Response(HttpStatusCode.BadRequest, errValidation);

            Game game = gameDto.GameDtoToModel(null, null);
            Game? duplicate = await gameRepo.IsDataDuplicateAsync(null, game);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Game Title must be unique.");

            await AddOrUpdateGameFromDto(game, gameDto, null);

            return new Response(HttpStatusCode.OK, "Game created successfully.");
        }

        public async Task<Response<List<GameCoverResponseDto>>> GetAllGamesAsync()
        {
            List<Game> games = await gameRepo.GetAllDatasAsync();
            List<GameCoverResponseDto> gameResponseDtos = [.. games.Select(game => GameCoverResponseDto.GameModelToCoverResponseDto(game))];

            return new Response<List<GameCoverResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), gameResponseDtos);
        }

        public async Task<Response<GameResponseDto>> GetGameByIdAsync(int id)
        {
            Game? game = await gameRepo.GetDataByIdAsync(id);
            if (game is null)
                return new Response<GameResponseDto>(HttpStatusCode.NotFound, "Game not found.", null);

            GameResponseDto gameResponseDto = GameResponseDto.GameModelToResponseDto(game);

            return new Response<GameResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), gameResponseDto);
        }

        public async Task<Response> UpdateGameAsync(int id, GameDto gameDto)
        {
            Game? existingGame = await gameRepo.GetDataByIdAsync(id);
            if (existingGame is null)
                return new Response(HttpStatusCode.NotFound, "Game not found.");

            Game game = gameDto.GameDtoToModel(existingGame, id);
            Game? duplicate = await gameRepo.IsDataDuplicateAsync(id, game);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Game Title must be unique.");

            await AddOrUpdateGameFromDto(game, gameDto, id);

            return new Response(HttpStatusCode.OK, "Game updated successfully.");
        }

        public async Task<Response> DeleteGameAsync(int id)
        {
            Game? existingGame = await gameRepo.GetDataByIdAsync(id);
            if (existingGame is null)
                return new Response(HttpStatusCode.NotFound, "Game not found.");

            await gameRepo.DeleteDataAsync(existingGame);

            return new Response(HttpStatusCode.OK, "Game deleted successfully.");
        }
    }
}
