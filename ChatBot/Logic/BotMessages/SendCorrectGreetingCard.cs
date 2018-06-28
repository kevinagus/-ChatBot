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
    public class SendCorrectGreetingCard
    {
        private readonly IDialogContext _context;

        public SendCorrectGreetingCard(IDialogContext context)
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
                        var facebooksender = new SendFacebookTemplate(_context);
                        var greetingFacebookTemplate = new GreetingFacebookTemplate(MessagesResource.GreetingVariant).GetTemplate();
                        await facebooksender.Send(greetingFacebookTemplate);
                        break;
                    }
                default:
                    {
                        var sender = new SendCardToConversation(_context);
                        var greatingDefaultTemplate = new GreetingCard(MessagesResource.GreetingVariant);
                        await sender.SendCard(greatingDefaultTemplate);
                        break;
                    }
            }
        }
    }
}