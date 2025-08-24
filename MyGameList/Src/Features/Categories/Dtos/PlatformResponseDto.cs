using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class PlatformResponseDto
    {
        public PlatformResponseDto()
        {
            Platform = string.Empty;
        }

        public PlatformResponseDto(int id, string platform)
        {
            Id = id;
            Platform = platform;
        }

        public int Id { get; set; }
        public string Platform { get; set; }

        public static PlatformResponseDto PlatformModelToResponseDto(Platform platform)
        {
            return new()
            {
                Id = platform.Id,
                Platform = platform.Option
            }; ;
        }
    }
}
