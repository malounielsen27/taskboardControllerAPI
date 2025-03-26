using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface ICardService
    {
        Task<ICollection<Card>> GetCardsByColumnId(int id);

        Task<Card> CreateCard(CardRequest request); 
        Task<Card> MoveCard(int id, int cardId);

        Task DeleteCard(int id); 
        
    }
}
