using LuisBot.Cards.FacebookTemplates;
using LuisBot.Cards.HeroCards;
using LuisBot.Logic.CardSender;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace LuisBot.Logic.BotMessages
{
    [Serializable]
    public class SendCorrectYesOrNoCard
    {
        private readonly IDialogContext _context;

        public SendCorrectYesOrNoCard(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send(string message)
        {
            var channel = _context.Activity.ChannelId;

            switch (channel)
            {
                case "facebook":
                    var facebookSender = new SendFacebookTemplate(_context);
                    var yesOrNoFacebookTemplate = new YesOrNoFacebookTemplate(message).GetTemplate();
                    await facebookSender.Send(yesOrNoFacebookTemplate);
                    break;
                default:
                    var defaultSender = new SendCardToConversation(_context);
                    var yesOrNoCard = new YesOrNoCard(message);
                    await defaultSender.SendCard(yesOrNoCard);
                    break;
            }
        }
    }
}