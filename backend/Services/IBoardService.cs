using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IBoardService
    {
        Task<Board?> GetBoardByIdAsync(int id, int userId);
        Task<Board?> CreateBoardAsync(BoardRequest request, int userId);
        Task<Board?> UpdateBoardAsync(string title, int id);

        Task<Board> GetFirstBoardId(int userId);

        Task<ICollection<Board>?> GetAllBoards(int userId);

        Task DeleteBoard(int id); 
    }
}