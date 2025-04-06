using Azure.Core;
using backend.Dtos;
using backend.Exceptions;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService; 
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody]CardRequest request)
        {
            try
            {
                var card = await _cardService.CreateCard(request);
                return Ok(card);
            } 
            catch(Exception e)
            {
                return StatusCode(500, e.Message); 
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> MoveCard(int id, [FromBody] MoveCardRequest request)
        {
            try
            {
                if (id < 1 || request.CardId < 1 || request.NewColumnId < 1)
                {
                    throw new FormatException("Not a valid id");
                }
                var card = await _cardService.MoveCard(request.NewColumnId, request.CardId);
                return Ok(card);
            }
            catch(FormatException e)
            {
                return BadRequest(e.Message);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message); 
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); 
            }
            

            
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                if (id < 1)
                {
                    throw new FormatException("Not a valid id");
                }
                await _cardService.DeleteCard(id);
                return Ok(); 
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
        
    }
}
