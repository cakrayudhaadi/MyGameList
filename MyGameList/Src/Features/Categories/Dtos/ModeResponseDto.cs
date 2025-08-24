using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class ModeResponseDto
    {
        public ModeResponseDto()
        {
            Mode = string.Empty;
        }

        public ModeResponseDto(int id, string mode)
        {
            Id = id;
            Mode = mode;
        }

        public int Id { get; set; }
        public string Mode { get; set; }

        public static ModeResponseDto ModeModelToResponseDto(Mode mode)
        {
            return new()
            {
                Id = mode.Id,
                Mode = mode.Option
            };
        }
    }
}
