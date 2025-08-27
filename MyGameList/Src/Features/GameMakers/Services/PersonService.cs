using MyGameList.Src.Features.GameMakers.Repositories;

namespace MyGameList.Src.Features.GameMakers.Services
{
    public interface IPersonService
    {
    }

    public class PersonService(IPersonRepository personRepo) : IPersonService
    {
    }
}
