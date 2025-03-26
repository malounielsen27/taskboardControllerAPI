using backend.Dtos;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class CardService: ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository; 
        }

        public async Task<Card> CreateCard(CardRequest request)
        {
            var card = new Card
            {
                Title = request.Title,
                Description = request.Description,
                ColumnId = request.ColumnId
            };
            var newCard = await _cardRepository.CreateCard(card);
            if (newCard == null)
            {
                throw new Exception("Could not create card"); 
            }
            return newCard; 

        }

        public async Task DeleteCard(int id)
        {
            await _cardRepository.DeleteCard(id); 
        }

        public async Task<ICollection<Card>> GetCardsByColumnId(int id)
        {
            var cards=await _cardRepository.GetCardsByColumnIdAsync(id);
            return cards; 
        }

        public async Task<Card> MoveCard(int id, int cardId)
        {
            var card= await _cardRepository.MoveCard(id, cardId);
            if (card == null)
            {
                throw new BadHttpRequestException("Card couldn't move"); 
            }
            return card; 
        }
    }
}
