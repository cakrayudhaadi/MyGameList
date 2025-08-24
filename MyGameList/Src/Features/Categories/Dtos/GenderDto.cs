using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class GenderDto
    {
        public GenderDto()
        {
            Gender = string.Empty;
        }

        public GenderDto(string gender)
        {
            Gender = gender;
        }

        public string Gender { get; set; }

        public Gender GenderDtoToModel(Gender? gender, int? id)
        {
            gender ??= new Gender();
            DateTime timeNow = DateTime.UtcNow;

            gender.Option = Gender is not null ? Gender : gender.Option;
            if (!id.HasValue)
            {
                gender.CreatedAt = timeNow;
                gender.UpdatedAt = timeNow;
            }
            else
            {
                gender.Id = id.Value;
                gender.UpdatedAt = timeNow;
            }

            return gender;
        }
    }
}
