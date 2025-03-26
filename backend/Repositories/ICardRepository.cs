using backend.Models;

namespace backend.Repositories
{
    public interface ICardRepository
    {

        Task<ICollection<Card>> GetCardsByColumnIdAsync(int id);
        Task<Card?> UpdateCardAsync(Card card);
        Task<Card> MoveCard(int id, int cardId);
        Task<Card> CreateCard(Card card);

        Task DeleteCard(int id); 
    }
}
