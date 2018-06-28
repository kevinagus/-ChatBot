using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace LuisBot.Interfaces
{
    public interface ICard
    {
        IList<Attachment> Draw();
    }
}