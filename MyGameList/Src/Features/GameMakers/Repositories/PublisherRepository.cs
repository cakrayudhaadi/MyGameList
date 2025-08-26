namespace MyGameList.Src.Features.GameMakers.Repositories
{
    public interface IPublisherRepository
    {
    }

    public class PublisherRepository(MyGameListDbContext context) : IPublisherRepository
    {
    }
}
