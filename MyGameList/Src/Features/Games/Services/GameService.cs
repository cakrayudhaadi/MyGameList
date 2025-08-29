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
        Task<Response<GameResponseDto>> AddGameAsync(GameDto gameDto);
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
        public async Task<Response<GameResponseDto>> AddGameAsync(GameDto gameDto)
        {
            ArgumentNullException.ThrowIfNull(gameDto);

            string? errValidation = gameDto.GameValidation();
            if (errValidation is not null)
                return new Response<GameResponseDto>(HttpStatusCode.BadRequest, errValidation, null);

            Game? duplicate = await gameRepo.GetGameByTitleAsync(gameDto.Title);
            if (duplicate is not null)
                return new Response<GameResponseDto>(HttpStatusCode.BadRequest, "Game Title must be unique.", null);

            AgeRating? ageRating = await ageRatingService.GetAgeRatingById(gameDto.AgeRatingId.Value);
            if (ageRating is null)
                return new Response<GameResponseDto>(HttpStatusCode.BadRequest, "Age Rating not found.", null);

            List<Developer> developers = await developerService.GetDeveloperListByIds(gameDto.DeveloperIds);
            List<Publisher> publishers = await publisherService.GetPublisherListByIds(gameDto.PublisherIds);
            List<Person> persons = await personService.GetPersonListByIds(gameDto.ProducerIds);
            List<Genre> genres = await genreService.GetGenreListByIds(gameDto.GenreIds);
            List<Mode> modes = await modeService.GetModeListByIds(gameDto.ModeIds);
            List<Platform> platforms = await platformService.GetPlatformListByIds(gameDto.PlatformIds);

            Game game = gameDto.GameDtoToModel(null, null);
            game.Developers = developers;
            game.Publishers = publishers;
            game.Producers = persons;
            game.Genres = genres;
            game.Modes = modes;
            game.Platforms = platforms;

            Game newGame = await gameRepo.AddAsync(game);
            GameResponseDto gameResponseDto = GameResponseDto.GameModelToResponseDto(newGame);

            return new Response<GameResponseDto>(HttpStatusCode.OK, "Game created successfully.", gameResponseDto);
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

            List<Developer> developers = await developerService.GetDeveloperListByIds(gameDto.DeveloperIds);
            List<Publisher> publishers = await publisherService.GetPublisherListByIds(gameDto.PublisherIds);
            List<Person> persons = await personService.GetPersonListByIds(gameDto.ProducerIds);
            List<Genre> genres = await genreService.GetGenreListByIds(gameDto.GenreIds);
            List<Mode> modes = await modeService.GetModeListByIds(gameDto.ModeIds);
            List<Platform> platforms = await platformService.GetPlatformListByIds(gameDto.PlatformIds);

            Game updatedGame = gameDto.GameDtoToModel(existingGame, id);
            updatedGame.Developers = developers;
            updatedGame.Publishers = publishers;
            updatedGame.Producers = persons;
            updatedGame.Genres = genres;
            updatedGame.Modes = modes;
            updatedGame.Platforms = platforms;

            await gameRepo.UpdateGameAsync(updatedGame);

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
