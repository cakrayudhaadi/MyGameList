using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Dtos
{
    public class GameResponseDto
    {
        public GameResponseDto()
        {
            Title = string.Empty;
        }

        public GameResponseDto(int id, string title, string? description, string? plot, int? yearRelease, int? ageRatingId)
        {
            Id = id;
            Title = title;
            Description = description;
            Plot = plot;
            YearRelease = yearRelease;
            AgeRatingId = ageRatingId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Plot { get; set; }
        public int? YearRelease { get; set; }
        public int? AgeRatingId { get; set; }
        public ICollection<DeveloperResponseDto> Developers { get; set; } = [];
        public ICollection<PublisherResponseDto> Publishers { get; set; } = [];
        public ICollection<PersonResponseDto> Producers { get; set; } = [];
        public ICollection<GenreResponseDto> Genres { get; set; } = [];
        public ICollection<ModeResponseDto> Modes { get; set; } = [];
        public ICollection<PlatformResponseDto> Platforms { get; set; } = [];

        public static GameResponseDto GameModelToResponseDto(Game game)
        {
            GameResponseDto gameResponseDto = new()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Plot = game.Plot,
                YearRelease = game.YearRelease,
                AgeRatingId = game.AgeRatingId,
                Developers = [.. game.Developers.Select(developer => DeveloperResponseDto.DeveloperModelToEditGameProperties(developer))],
                Publishers = [.. game.Publishers.Select(publisher => PublisherResponseDto.PublisherModelToEditGameProperties(publisher))],
                Producers = [.. game.Producers.Select(producer => PersonResponseDto.PersonModelToEditGameProperties(producer))],
                Genres = [.. game.Genres.Select(genre => GenreResponseDto.GenreModelToEditGameProperties(genre))],
                Modes = [.. game.Modes.Select(mode => ModeResponseDto.ModeModelToEditGameProperties(mode))],
                Platforms = [.. game.Platforms.Select(platform => PlatformResponseDto.PlatformModelToEditGameProperties(platform))]
            };

            return gameResponseDto;
        }
    }
    public class GameCoverResponseDto
    {
        public GameCoverResponseDto()
        {
            Title = string.Empty;
        }

        public GameCoverResponseDto(int id, string title, int? yearRelease)
        {
            Id = id;
            Title = title;
            YearRelease = yearRelease;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? YearRelease { get; set; }

        public static GameCoverResponseDto GameModelToCoverResponseDto(Game game)
        {
            GameCoverResponseDto gameCoverResponseDto = new()
            {
                Id = game.Id,
                Title = game.Title,
                YearRelease = game.YearRelease,
                //PlayerAmount = playerAmount,
                //Rating = rating
            };

            return gameCoverResponseDto;
        }
    }
}
