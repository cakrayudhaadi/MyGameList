using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.GameMakers.Dtos;
using MyGameList.Src.Features.GameMakers.Services;
using MyGameList.Src.Features.Games.Dtos;
using MyGameList.Src.Features.Games.Models;
using MyGameList.Src.Features.Games.Services;
using MyGameList.Src.Shared.Commons;
using System.Net;

namespace MyGameList.Src.Features.Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly ResponseHandler res = new();

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddGame(GameDto gameDto)
        {
            try
            {
                return res.Result(await gameService.AddGameAsync(gameDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GameCoverResponseDto>>>> GetAllGames()
        {
            try
            {
                return res.Result<List<GameCoverResponseDto>>(await gameService.GetAllGamesAsync());
            }
            catch (ArgumentException ex)
            {
                return res.Result<List<GameCoverResponseDto>>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GameResponseDto>>> GetGameById(int id)
        {
            try
            {
                return res.Result<GameResponseDto>(await gameService.GetGameByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result<GameResponseDto>(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateGame(int id, GameDto gameDto)
        {
            try
            {
                return res.Result(await gameService.UpdateGameAsync(id, gameDto));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteGame(int id)
        {
            try
            {
                return res.Result(await gameService.DeleteGameAsync(id));
            }
            catch (ArgumentException ex)
            {
                return res.Result(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
