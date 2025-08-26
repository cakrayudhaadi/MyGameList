using MyGameList.Src.Features.GameMakers.Repositories;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IPeopleService
    {
    }

    public class PeopleService(IPeopleRepository peopleRepo) : IPeopleService
    {
    }
}
