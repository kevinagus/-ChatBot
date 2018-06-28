using LuisBot.Models.FacebookModels;

namespace LuisBot.Interfaces
{
    public interface IQuickReply
    {
        FacebookMessageQuickReply GetTemplate();
    }
}