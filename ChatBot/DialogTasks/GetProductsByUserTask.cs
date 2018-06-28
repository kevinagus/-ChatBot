using LuisBot.Dialogs;
using LuisBot.Interfaces;
using LuisBot.Logic.LoopTaskHandler;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace LuisBot.DialogTasks
{
    [Serializable]
    public class GetProductsByUserTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public GetProductsByUserTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(GetTask);
        }

        public async Task GetTask(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            try
            {
                var productsSelected = await result;

                await context.PostAsync(MessagesResource.AskIfContinueToAddProduct);

                new HandleUserIncorrectInput(context).ResetCounter();

                context.Call(_dialogFactory.Create<AskIfAddAnotherProductTask>(), Callback);
            }
            catch (Exception ex)
            {
                await context.PostAsync(MessagesResource.CourtesyError);

                context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
            }
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
        }
    }
}