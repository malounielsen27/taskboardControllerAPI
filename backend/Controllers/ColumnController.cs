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
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService _columnService; 

        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService; 
        }
        [HttpPost]
        public async Task<IActionResult> CreateColumn(ColumnRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new BadHttpRequestException("Request can't be null"); 
                }
                var created = await _columnService.CreateColumnAsync(request);
                return Ok(created);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message); 
            }
           
  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColumn(int id, ColumnRequest request)
        {
            try
            {
                var updated = await _columnService.UpdateColumn(id, request);
                return Ok(updated);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            try
            {
                await _columnService.DeleteColumn(id);
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
