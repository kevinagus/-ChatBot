using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace LuisBot.Logic.BotMessages
{
    [Serializable]
    public class SendConfirmProductAdded
    {
        private readonly IDialogContext _context;

        public SendConfirmProductAdded(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send()
        {
            await _context.PostAsync("Prodotto aggiunto con successo.");
        }
    }
}