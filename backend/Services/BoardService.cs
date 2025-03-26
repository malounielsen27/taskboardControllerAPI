using backend.Dtos;
using backend.Exceptions;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class BoardService : IBoardService
    {

        private readonly IBoardRepository _boardRepository;
        private readonly IColumnService _columnService;
        private readonly ICardService _cardService;
        public BoardService(IBoardRepository boardRepository, IColumnService columnService, ICardService cardService)
        {
            _boardRepository = boardRepository;
            _columnService = columnService;
            _cardService = cardService; 
        }

        public async Task<Board?> GetBoardByIdAsync(int id)
        {
            var board= await _boardRepository.GetBoardByIdAsync(id);
            if (board == null)
            {
                throw new NotFoundException("No board found", "For id: "+id);
            }
                var columns = await _columnService.GetColumnsByBoardIdAsync(id);

                if (columns == null)
                {
                throw new NotFoundException("No column found", "For board-id: " + id);
                }
                    
                    board.Columns = columns;

                    foreach (Column c in board.Columns)
                    {
                        var cards = await _cardService.GetCardsByColumnId(c.Id);
                            if (cards == null)
                            {
                            throw new NotFoundException("Card found", "For column-id: " + c.Id);
                            }
                        c.Cards = cards; 
                    }

            return board;

        }

        public async Task<Board?> CreateBoardAsync(BoardRequest request)
        {
            var board = new Board
            {
                Title = request.Title
            };

            var created= await _boardRepository.CreateBoardAsync(board);
            if (created == null)
            {
                throw new Exception("Board not created");
            }
            return created; 
            
        }

        public async Task<Board?> UpdateBoardAsync(string title, int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id can't be 0 or below"); 
            }
            var updated = await _boardRepository.UpdateBoardAsync(title, id);
            if (updated == null)
            {
                throw new BadHttpRequestException("Could not update board with id: " +id); 
            }
            return updated;
        }

        public async Task<ICollection<Board>?> GetAllBoards()
        {
            var boards = await _boardRepository.GetAllBoardsAsync();
            if (boards == null)
            {
                throw new BadHttpRequestException("No boards"); 
            }
            return boards; 
        }

        public async Task DeleteBoard(int id)
        {
            await _boardRepository.DeleteBoard(id); 
        }
    }
}
