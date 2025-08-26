using MyGameList.Src.Features.GameMakers.Repositories;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IDeveloperService
    {
    }

    public class DeveloperService(IDeveloperRepository developerRepo) : IDeveloperService
    {
    }
}
