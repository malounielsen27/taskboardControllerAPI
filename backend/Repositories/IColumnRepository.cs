using backend.Dtos;
using backend.Models;

namespace backend.Repositories
{
    public interface IColumnRepository
    {
        Task<ICollection<Column>> GetColumnsByBoardIdAsync(int id);

        Task<Column> CreateColumnAsync(Column column); 
        Task DeleteColumnAsync(int id);
        Task<Column?> UpdateColumnAsync(int id,Column column); 
    }
}
