using LuisBot.Interfaces;
using LuisBot.Messages;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace LuisBot.Cards.HeroCards
{
    public class GreetingCard : ICard
    {
        private readonly string _message;

        public GreetingCard(string message)
        {
            _message = message;
        }

        public IList<Attachment> Draw()
        {
            var heroCard = new HeroCard
            {
                Title = _message,
                Buttons = new List<CardAction> {
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = CardMessagesResource.GreetingCardNewListTitle,
                        Value = CardMessagesResource.GreetingCardNewListPayload
                    },
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = CardMessagesResource.GreetingCardOldListTitle,
                        Value = CardMessagesResource.GreetingCardOldListPayload
                    },
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = CardMessagesResource.GreetingCardViewPromoTitle,
                        Value = CardMessagesResource.GreetingCardViewPromoPayload
                    },
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = CardMessagesResource.GreetingCardTimeTableTitle,
                        Value = CardMessagesResource.GreetingCardTimeTablePayload
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