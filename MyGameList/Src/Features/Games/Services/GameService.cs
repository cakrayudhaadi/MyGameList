using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Categories.Services;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.GameMakers.Services;
using MyGameList.Src.Features.Games.Dtos;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Games.Repositories;
using MyGameList.Src.Shared.Commons;
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
        IPlatformService platformService) : IGameService
    {
        public async Task AddOrUpdateGameFromDto(GameDto gameDto, Game? existingGame, int? id)
        {
            Game game = gameDto.GameDtoToModel(existingGame, id);

            await AddGameProperties(existingGame, game.Developers, gameDto.DeveloperIds, developerService.GetDeveloperListByIds);
            await AddGameProperties(existingGame, game.Publishers, gameDto.PublisherIds, publisherService.GetPublisherListByIds);
            await AddGameProperties(existingGame, game.Producers, gameDto.ProducerIds, personService.GetPersonListByIds);
            await AddGameProperties(existingGame, game.Genres, gameDto.GenreIds, genreService.GetGenreListByIds);
            await AddGameProperties(existingGame, game.Modes, gameDto.ModeIds, modeService.GetModeListByIds);
            await AddGameProperties(existingGame, game.Platforms, gameDto.PlatformIds, platformService.GetPlatformListByIds);

            if (!id.HasValue)
            {
                await gameRepo.AddAsync(game);
            }
            else
            {
                await gameRepo.UpdateGameAsync(game);
            }
        }

        // Helper function to process and add associations
        public async Task AddGameProperties<T>(
            Game? existingGame,
            ICollection<T> existingCollection,
            List<int> dtoIds,
            Func<List<int>, Task<List<T>>> getByIdsFunc)
        {
            var idsToAdd = existingGame is not null
                ? [.. dtoIds.Except(existingCollection.Select(item => (int)typeof(T).GetProperty("Id")?.GetValue(item)!))]
                : dtoIds;

            var newItems = await getByIdsFunc(idsToAdd);

            foreach (var item in newItems)
            {
                existingCollection.Add(item);
            }
        }


        public async Task<Response> AddGameAsync(GameDto gameDto)
        {
            ArgumentNullException.ThrowIfNull(gameDto);

            string? errValidation = gameDto.GameValidation();
            if (errValidation is not null)
                return new Response(HttpStatusCode.BadRequest, errValidation);

            Game? duplicate = await gameRepo.GetGameByTitleAsync(gameDto.Title);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Game Title must be unique.");

            if (gameDto.AgeRatingId is not null)
            {
                AgeRating? ageRating = await ageRatingService.GetAgeRatingById(gameDto.AgeRatingId.Value);
                if (ageRating is null)
                    return new Response(HttpStatusCode.BadRequest, "Age Rating not found.");
            }

            await AddOrUpdateGameFromDto(gameDto, null, null);

            return new Response(HttpStatusCode.OK, "Game created successfully.");
        }

        public async Task<Response<List<GameCoverResponseDto>>> GetAllGamesAsync()
        {
            List<Game> games = await gameRepo.GetAllGamesAsync();
            List<GameCoverResponseDto> gameResponseDtos = [.. games.Select(game => GameCoverResponseDto.GameModelToCoverResponseDto(game))];

            return new Response<List<GameCoverResponseDto>>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), gameResponseDtos);
        }

        public async Task<Response<GameResponseDto>> GetGameByIdAsync(int id)
        {
            Game? game = await gameRepo.GetGameByIdAsync(id);
            if (game is null)
                return new Response<GameResponseDto>(HttpStatusCode.NotFound, "Game not found.", null);

            GameResponseDto gameResponseDto = GameResponseDto.GameModelToResponseDto(game);

            return new Response<GameResponseDto>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), gameResponseDto);
        }

        public async Task<Response> UpdateGameAsync(int id, GameDto gameDto)
        {
            Game? existingGame = await gameRepo.GetGameByIdAsync(id);
            if (existingGame is null)
                return new Response(HttpStatusCode.NotFound, "Game not found.");

            Game? duplicate = await gameRepo.GetGameByTitleAsync(gameDto.Title);
            if (duplicate is not null)
                return new Response(HttpStatusCode.BadRequest, "Game Title must be unique.");

            if (gameDto.AgeRatingId.HasValue)
            {
                AgeRating? ageRating = await ageRatingService.GetAgeRatingById(gameDto.AgeRatingId.Value);
                if (ageRating is null)
                    return new Response(HttpStatusCode.BadRequest, "Age Rating not found.");
            }

            await AddOrUpdateGameFromDto(gameDto, existingGame, id);

            return new Response(HttpStatusCode.OK, "Game updated successfully.");
        }

        public async Task<Response> DeleteGameAsync(int id)
        {
            Game? existingGame = await gameRepo.GetGameByIdAsync(id);
            if (existingGame is null)
                return new Response(HttpStatusCode.NotFound, "Game not found.");

            await gameRepo.DeleteGameAsync(existingGame);

            return new Response(HttpStatusCode.OK, "Game deleted successfully.");
        }
    }
}
