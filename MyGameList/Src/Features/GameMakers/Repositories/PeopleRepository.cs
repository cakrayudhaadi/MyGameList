namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPeopleRepository
    {
    }

    public class PeopleRepository(MyGameListDbContext context) : IPeopleRepository
    {
    }
}
