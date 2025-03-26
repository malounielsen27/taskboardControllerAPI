using backend.Models;

namespace backend.Repositories
{
    public interface IBoardRepository
    {
        Task<Board?> GetBoardByIdAsync(int id);
        Task<Board?> CreateBoardAsync(Board board);

        Task<Board?> UpdateBoardAsync(string title, int id);

        Task<ICollection<Board>?> GetAllBoardsAsync();
        Task DeleteBoard(int id); 
    }
}
