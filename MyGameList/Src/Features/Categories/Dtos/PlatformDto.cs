using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class PlatformDto
    {
        public PlatformDto()
        {
            Platform = string.Empty;
        }

        public PlatformDto(string platform)
        {
            Platform = platform;
        }

        public string Platform { get; set; }

        public Platform PlatformDtoToModel(Platform? platform, int? id)
        {
            platform ??= new Platform();
            DateTime timeNow = DateTime.UtcNow;

            platform.Option = Platform is not null ? Platform : platform.Option;
            if (!id.HasValue)
            {
                platform.CreatedAt = timeNow;
                platform.UpdatedAt = timeNow;
            }
            else
            {
                platform.Id = id.Value;
                platform.UpdatedAt = timeNow;
            }

            return platform;
        }
    }
}
