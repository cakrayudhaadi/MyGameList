using MyGameList.Src.Features.Categories.Dtos;
using MyGameList.Src.Features.Characters.Dtos;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features.Games.Dtos
{
    public class GameResponseDto
    {
        public GameResponseDto()
        {
        }

        public GameResponseDto(int id, string? title, string? description, string? plot, int? yearRelease)
        {
            Id = id;
            Title = title;
            Description = description;
            Plot = plot;
            YearRelease = yearRelease;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Plot { get; set; }
        public int? YearRelease { get; set; }
        public ICollection<AgeRatingResponseDto> AgeRatings { get; set; } = [];
        public ICollection<DeveloperResponseDto> Developers { get; set; } = [];
        public ICollection<PublisherResponseDto> Publishers { get; set; } = [];
        public ICollection<PersonResponseDto> Producers { get; set; } = [];
        public ICollection<GenreResponseDto> Genres { get; set; } = [];
        public ICollection<ModeResponseDto> Modes { get; set; } = [];
        public ICollection<PlatformResponseDto> Platforms { get; set; } = [];
        public ICollection<CharacterCoverResponseDto> Characters { get; set; } = [];

        public static GameResponseDto GameModelToResponseDto(Game? game)
        {
            ArgumentNullException.ThrowIfNull(game);

            GameResponseDto gameResponseDto = new()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Plot = game.Plot,
                YearRelease = game.YearRelease,
                AgeRatings = [.. game.AgeRatings.Select(ageRating => AgeRatingResponseDto.AgeRatingModelToEditGameProperties(ageRating))],
                Developers = [.. game.Developers.Select(developer => DeveloperResponseDto.DeveloperModelToEditGameProperties(developer))],
                Publishers = [.. game.Publishers.Select(publisher => PublisherResponseDto.PublisherModelToEditGameProperties(publisher))],
                Producers = [.. game.Producers.Select(producer => PersonResponseDto.PersonModelToEditGameProperties(producer))],
                Genres = [.. game.Genres.Select(genre => GenreResponseDto.GenreModelToEditGameProperties(genre))],
                Modes = [.. game.Modes.Select(mode => ModeResponseDto.ModeModelToEditGameProperties(mode))],
                Platforms = [.. game.Platforms.Select(platform => PlatformResponseDto.PlatformModelToEditGameProperties(platform))],
                Characters = [.. game.Characters.Select(character => CharacterCoverResponseDto.CharacterModelToCoverResponseDto(character))]
            };

            return gameResponseDto;
        }
    }
    public class GameCoverResponseDto
    {
        public GameCoverResponseDto()
        {
        }

        public GameCoverResponseDto(int id, string? title, int? yearRelease)
        {
            Id = id;
            Title = title;
            YearRelease = yearRelease;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
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
