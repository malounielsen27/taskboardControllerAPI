using backend.Dtos;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;
        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository; 
        }

        public async Task<Column> CreateColumnAsync(ColumnRequest request)
        {
            var column = new Column
            {
                Title = request.Title,
                BoardId = request.BoardId
            };
            var created= await _columnRepository.CreateColumnAsync(column);
            if (created == null)
            {
                throw new Exception("Couldn't create column"); 
            }
            return created; 
        }

        public async Task DeleteColumn(int id)
        {
             await _columnRepository.DeleteColumnAsync(id); 
        }

        public async Task<ICollection<Column>> GetColumnsByBoardIdAsync(int id)
        {
            return await _columnRepository.GetColumnsByBoardIdAsync(id); 
          
        }

        public async Task<Column?> UpdateColumn(int id, ColumnRequest column)
        {
            var newColumn = new Column { Title = column.Title, BoardId = column.BoardId }; 
            return await _columnRepository.UpdateColumnAsync(id, newColumn); 
        }

    }
}
