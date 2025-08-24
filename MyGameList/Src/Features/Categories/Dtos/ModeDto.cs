using MyGameList.Src.Features.Categories.Models;

namespace MyGameList.Src.Features.Categories.Dtos
{
    public class ModeDto
    {
        public ModeDto()
        {
            Mode = string.Empty;
        }

        public ModeDto(string mode)
        {
            Mode = mode;
        }

        public string Mode { get; set; }

        public Mode ModeDtoToModel(Mode? mode, int? id)
        {
            mode ??= new Mode();
            DateTime timeNow = DateTime.UtcNow;

            mode.Option = Mode is not null ? Mode : mode.Option;
            if (!id.HasValue)
            {
                mode.CreatedAt = timeNow;
                mode.UpdatedAt = timeNow;
            }
            else
            {
                mode.Id = id.Value;
                mode.UpdatedAt = timeNow;
            }

            return mode;
        }

        public string? ModeValidation()
        {
            if (string.IsNullOrEmpty(Mode))
                return "Mode are required";

            return null;
        }
    }
}
