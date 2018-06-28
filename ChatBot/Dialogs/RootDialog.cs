using LuisBot.Interfaces;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        private readonly IDialogFactory _dialogFactory;

        private readonly bool _debugMode = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugMode"] ?? "false");

        public RootDialog(ILuisService luisService, IDialogFactory dialogFactory)
            : base(luisService)
        {
            _dialogFactory = dialogFactory;
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(MessagesResource.CourtesyError);
            context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
        }

        [LuisIntent("Greetings")]
        public async Task GreetingsIntent(IDialogContext context, LuisResult result)
        {
            await Task.Delay(0);
            context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
        }

        [LuisIntent("CreateNewList")]
        public async Task CreateNewListIntent(IDialogContext context, LuisResult result)
        {
            await Task.Delay(0);
            context.Call(_dialogFactory.Create<CreateNewShoppingListDialog>(), Callback);
        }

        [LuisIntent("OpenLastList")]
        public async Task OpenLastListIntent(IDialogContext context, LuisResult result)
        {
            await Task.Delay(0);
            context.Call(_dialogFactory.Create<OpenLastShoppingListDialog>(), Callback);
        }

        [LuisIntent("GetTimetables")]
        public async Task GetTimetablesIntent(IDialogContext context, LuisResult result)
        {
            await Task.Delay(0);
            context.Call(_dialogFactory.Create<OpeningHoursDialog>(), Callback);
        }

        [LuisIntent("GetPromo")]
        public async Task GetPromoIntent(IDialogContext context, LuisResult result)
        {
            await Task.Delay(0);
            context.Call(_dialogFactory.Create<PromoProductsByCategoryDialog>(), Callback);
        }

        [LuisIntent("")]
        public async Task CancelIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(MessagesResource.CourtesyError);
            context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            await result;
            context.Done(true);
        }
    }
}