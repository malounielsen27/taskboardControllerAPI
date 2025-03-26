using backend.Dtos;
using backend.Exceptions;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly ApplicationContext _context; 
        public ColumnRepository(ApplicationContext context)
        {
            _context = context; 
        }

        public async Task<Column> CreateColumnAsync(Column column)
        {

            await _context.AddAsync(column);
            await _context.SaveChangesAsync();
            return column;
        }

        public async Task DeleteColumnAsync(int id)
        {
            var column = await _context.Columns.FindAsync(id);

            if (column == null)
            {
                throw new NotFoundException("Could not find ", "column-id: " + id); 
            }
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<Column>> GetColumnsByBoardIdAsync(int id)
        {
            var columns=await _context.Columns.Where(c => c.BoardId == id).ToListAsync();
            if (columns == null)
            {
                throw new NotFoundException("Can't get columns", "For board id: "+id); 
            }
            return columns; 
        }

        public async Task<Column?> UpdateColumnAsync(int id, Column column)
        {
            var updated = await _context.Columns.FindAsync(id);
            if (updated == null)
            {
                throw new NotFoundException("Couldn't update columns ", "With id: " + id);  
            }
            updated.Title = column.Title;
            updated.BoardId = column.BoardId; 
            await _context.SaveChangesAsync();
            return updated; 
        }
    }
}
