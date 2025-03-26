using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IColumnService
    {
        Task<Column> CreateColumnAsync(ColumnRequest request); 
        Task<ICollection<Column>> GetColumnsByBoardIdAsync(int id);
        Task<Column?> UpdateColumn(int id, ColumnRequest column);
        Task DeleteColumn(int id); 
    }
}
