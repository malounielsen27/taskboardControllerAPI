using backend.Exceptions;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationContext _context; 

        public BoardRepository(ApplicationContext context)
        {
            _context = context; 
        }

        public async Task<Board?> CreateBoardAsync(Board board)
        {
            await _context.AddAsync(board);
            await _context.SaveChangesAsync();
            return board; 
        }

        public async Task DeleteBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                throw new NotFoundException("Couldn't delete board", "with id: "+id); 
            }
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Board>?> GetAllBoardsAsync()
        {
            return await _context.Boards.ToListAsync(); 
        }

        public async Task<Board?> GetBoardByIdAsync(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            return board; 
        }

        public async Task<Board?> UpdateBoardAsync(string Title, int id)
        {
                var updateBoard = await _context.Boards.FindAsync(id);
                if (updateBoard == null)
                {
                    throw new NotFoundException("Couldn't update board", "With id: " + id); 
                }

                updateBoard.Title = Title;
                await _context.SaveChangesAsync();
                return updateBoard; 
        }
    }
}
