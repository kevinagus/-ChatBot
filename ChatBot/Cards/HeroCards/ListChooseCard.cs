using LuisBot.Interfaces;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace LuisBot.Cards.HeroCards
{
    public class ListChooseCard : ICard
    {
        private readonly string _message;

        public ListChooseCard(string message)
        {
            _message = message;
        }
        public IList<Attachment> Draw()
        {
            var heroCard = new HeroCard
            {
                Title = _message,
                Buttons = new List<CardAction>
                {
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = "Nuova lista",
                        Value = "Nuova lista"
                    },
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = "Vecchia lista",
                        Value = "Vecchia lista"
                    }
                }
            };

            return new List<Attachment>()
            {
                heroCard.ToAttachment()
            };
        }
    }
}