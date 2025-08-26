namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IDeveloperRepository
    {
    }

    public class DeveloperRepository(MyGameListDbContext context) : IDeveloperRepository
    {
    }
}
