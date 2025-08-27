namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPersonRepository
    {
    }

    public class PersonRepository(MyGameListDbContext context) : IPersonRepository
    {
    }
}
