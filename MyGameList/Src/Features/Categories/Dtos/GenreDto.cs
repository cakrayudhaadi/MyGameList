using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class GenreDto
    {
        public GenreDto()
        {
            Genre = string.Empty;
        }

        public GenreDto(string genre)
        {
            Genre = genre;
        }

        public string Genre { get; set; }

        public Genre GenreDtoToModel(Genre? genre, int? id)
        {
            genre ??= new Genre();
            DateTime timeNow = DateTime.UtcNow;

            genre.Option = Genre is not null ? Genre : genre.Option;
            if (!id.HasValue)
            {
                genre.CreatedAt = timeNow;
                genre.UpdatedAt = timeNow;
            }
            else
            {
                genre.Id = id.Value;
                genre.UpdatedAt = timeNow;
            }

            return genre;
        }
    }
}
