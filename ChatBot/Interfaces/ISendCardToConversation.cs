using System.Threading.Tasks;

namespace LuisBot.Interfaces
{
    public interface ISendCardToConversation
    {
        Task SendCard(ICard card);
    }
}