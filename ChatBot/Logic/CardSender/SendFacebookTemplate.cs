using LuisBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace LuisBot.Logic.CardSender
{
    [Serializable]
    public class SendFacebookTemplate : ISendFacebookTemplate
    {
        private readonly IDialogContext _context;

        public SendFacebookTemplate(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send(IFacebookMessage facebookMessage)
        {
            var replyMessage = (Activity)_context.MakeMessage();
            replyMessage.ChannelData = facebookMessage;
            await _context.PostAsync(replyMessage);
        }
    }
}