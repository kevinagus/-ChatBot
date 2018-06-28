using LuisBot.Logic.BotMessages;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace LuisBot.Logic.LoopTaskHandler
{
    [Serializable]
    public class HandleUserIncorrectInput
    {

        private readonly IDialogContext _context;
        private readonly int errorLimit = 0;
        private const int initialCounterValue = 0;

        public HandleUserIncorrectInput(IDialogContext context)
        {
            _context = context;
            errorLimit = int.Parse(ConfigurationManager.AppSettings["WrongInputCounterLimit"]);
        }

        public bool CheckCounterErrors()
        {
            var count = Int32.Parse(_context.UserData.GetValue<string>("UserIncorrectInput"));
            count++;

            _context.UserData.SetValue("UserIncorrectInput", count);

            return count > errorLimit;
        }

        public async Task UserErrorLimitExceeded()
        {
            await _context.PostAsync(MessagesResource.ErrorLimitExceeded);

            var sender = new SendCorrectGreetingCard(_context);
            await sender.Send();
        }

        public void ResetCounter()
        {
            _context.UserData.SetValue("UserIncorrectInput", initialCounterValue);
        }
    }
}