using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class GenderResponseDto
    {
        public GenderResponseDto()
        {
            Gender = string.Empty;
        }

        public GenderResponseDto(int id, string gender)
        {
            Id = id;
            Gender = gender;
        }

        public int Id { get; set; }
        public string Gender { get; set; }

        public static GenderResponseDto GenderModelToResponseDto(Gender gender)
        {
            return new()
            {
                Id = gender.Id,
                Gender = gender.Option
            };
        }
    }
}
