using Microsoft.AspNetCore.Mvc;
using MyGameList.Src.Features.GameMakers.Services;

namespace MyGameList.Src.Features.GameMakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(IPeopleService peopleService) : ControllerBase
    {
    }
}
