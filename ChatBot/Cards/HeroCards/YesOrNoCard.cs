using LuisBot.Interfaces;
using LuisBot.Messages;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace LuisBot.Cards.HeroCards
{
    public class YesOrNoCard : ICard
    {
        private readonly string _message;

        public YesOrNoCard(string message)
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
                        Title = CardMessagesResource.YesOrNoCardYesTitle,
                        Value = CardMessagesResource.YesOrNoCardYesPayload
                    },
                    new CardAction()
                    {
                        Type = ActionTypes.PostBack,
                        Title = CardMessagesResource.YesOrNoCardNoTitle,
                        Value = CardMessagesResource.YesOrNoCardNoPayload
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