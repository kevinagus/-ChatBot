using LuisBot.Cards.FacebookTemplates;
using LuisBot.Cards.HeroCards;
using LuisBot.Logic.CardSender;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace LuisBot.Logic.BotMessages
{
    [Serializable]
    public class SendCorrectListChooseCard
    {
        private readonly IDialogContext _context;

        public SendCorrectListChooseCard(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send()
        {
            var channel = _context.Activity.ChannelId;

            switch (channel)
            {
                case "facebook":
                    {
                        var facebookSender = new SendFacebookTemplate(_context);
                        var listFacebookTemplate = new ListChooseCardFacebookTemplate(MessagesResource.AskWhatListUse).GetTemplate();
                        await facebookSender.Send(listFacebookTemplate);
                        break;
                    }
                default:
                    {
                        var defaultSender = new SendCardToConversation(_context);
                        var listChooseCard = new ListChooseCard(MessagesResource.AskWhatListUse);
                        await defaultSender.SendCard(listChooseCard);
                        break;
                    }
            }
        }
    }
}