using MyGameList.Src.Features.GameMakers.Repositories;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IPublisherService
    {
    }

    public class PublisherService(IPublisherRepository publisherRepo) : IPublisherService
    {
    }
}
