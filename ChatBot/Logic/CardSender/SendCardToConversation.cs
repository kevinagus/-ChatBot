using LuisBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace LuisBot.Logic.CardSender
{
    [Serializable]
    public class SendCardToConversation : ISendCardToConversation
    {
        private readonly IDialogContext _context;

        public SendCardToConversation(IDialogContext context)
        {
            _context = context;
        }

        public async Task SendCard(ICard card)
        {
            var message = _context.MakeMessage();
            var connector = new ConnectorClient(new Uri(message.ServiceUrl));
            var replyToConversation = (Activity)_context.MakeMessage();
            replyToConversation.Attachments = card.Draw();

            string cardType = card.GetType().Name;
            if (cardType == "CarouselListCard")
            {
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            }

            await connector.Conversations.SendToConversationAsync(replyToConversation);
        }
    }
}