using backend.Dtos;
using backend.Exceptions;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard([FromRoute] int id)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("No user id found in token");
                }

                int userId = int.Parse(userIdClaim);
                var board = await _boardService.GetBoardByIdAsync(id, userId);
                return Ok(board);
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message); 
            }
            catch (NotFoundException e)
            {
                return NotFound(new
                {
                    e.Message,
                    e.DetailInfo
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }

        }

       /* [HttpGet]
        public async Task<IActionResult> GetFirstBoard()
        {

        }*/
        
        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("No user id found in token");
                }

                int userId = int.Parse(userIdClaim);
               
                var boards = await _boardService.GetAllBoards(userId);
                return Ok(boards ?? new List<Board>());
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);  
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBoard(BoardRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new BadHttpRequestException("Request was null");
                }
               
             
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("No user id found in token");
                }

                int userId = int.Parse(userIdClaim);
                var created = await _boardService.CreateBoardAsync(request, userId);
                return Ok(created);
             

            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateBoardName(int id, BoardRequest request)
        {
          try
            {
                var updated = await _boardService.UpdateBoardAsync(request.Title, id);
                return Ok(updated);
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }

        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            try
            {
                await _boardService.DeleteBoard(id);
                return Ok(); 
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }



    }
}
