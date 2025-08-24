using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class GenreResponseDto
    {
        public GenreResponseDto()
        {
            Genre = string.Empty;
        }

        public GenreResponseDto(int id, string genre)
        {
            Id = id;
            Genre = genre;
        }

        public int Id { get; set; }
        public string Genre { get; set; }

        public static GenreResponseDto GenreModelToResponseDto(Genre genre)
        {
            return new()
            {
                Id = genre.Id,
                Genre = genre.Option
            };
        }
    }
}
