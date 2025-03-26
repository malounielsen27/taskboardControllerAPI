using Azure.Core;
using backend.Exceptions;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationContext _context; 
        public CardRepository(ApplicationContext context)
        {
            _context = context; 
        }

        public async Task<Card> CreateCard(Card card)
        {
            var columnExists = await _context.Columns.AnyAsync(c => c.Id == card.ColumnId);
            if (!columnExists)
            {
                throw new Exception("Invalid ColumnId.");
            }
            var e=await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync(); 
            return e.Entity; 
        }

        public async Task DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                throw new NotFoundException("Could not find ", "card-id: " + id);
            }
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Card>> GetCardsByColumnIdAsync(int id)
        {
            var cards = await _context.Cards.Where(c => c.ColumnId == id).ToListAsync();
            return cards; 
        }

        public async Task<Card> MoveCard(int id, int cardId)
        {
            var moved = await _context.Cards.FindAsync(cardId);
            if (moved == null)
            {
                return null;
            }
            moved.ColumnId = id;
            await _context.SaveChangesAsync();
            return moved; 

        }

        public async Task<Card?> UpdateCardAsync(Card card)
        {
            var existingCard=await _context.Cards.FindAsync(card.Id);
            if (existingCard == null)
            {
                return null; 
            }
            _context.Entry(existingCard).CurrentValues.SetValues(card);
            await _context.SaveChangesAsync();
            return existingCard; 
            
        }
    }
}
