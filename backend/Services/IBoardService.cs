using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IBoardService
    {
        Task<Board?> GetBoardByIdAsync(int id);
        Task<Board?> CreateBoardAsync(BoardRequest request);
        Task<Board?> UpdateBoardAsync(string title, int id);

        Task<ICollection<Board>?> GetAllBoards();

        Task DeleteBoard(int id); 
    }
}