using backend.Dtos;
using backend.Exceptions;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
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
                var board = await _boardService.GetBoardByIdAsync(id);
                return Ok(board);
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

        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            try
            {
                var boards = await _boardService.GetAllBoards();
                return Ok(boards ?? new List<Board>());
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
                var created = await _boardService.CreateBoardAsync(request);
                return Ok(created); 
                
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, "Error"); 
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
            catch(BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: "+e.Message); 
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
            catch(NotFoundException e)
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



    }
}
