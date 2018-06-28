using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace LuisBot.Logic.BotMessages
{
    [Serializable]
    public class AskIfWantProductPosition
    {
        private readonly IDialogContext _context;

        public AskIfWantProductPosition(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send()
        {
            var message = MessagesResource.AskIfWantSeePosition;
            var sendCorrectYesOrNoCard = new SendCorrectYesOrNoCard(_context);
            await sendCorrectYesOrNoCard.Send(message);
        }
    }
}