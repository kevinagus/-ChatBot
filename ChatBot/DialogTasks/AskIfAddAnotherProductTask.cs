using LuisBot.Dialogs;
using LuisBot.Interfaces;
using LuisBot.Logic.BotMessages;
using LuisBot.Logic.LoopTaskHandler;
using LuisBot.LuisCustom;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace LuisBot.DialogTasks
{
    [Serializable]
    public class AskIfAddAnotherProductTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public AskIfAddAnotherProductTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var sender = new SendCorrectYesOrNoCard(context);
            await sender.Send(MessagesResource.AskIfWantAddProduct);

            new HandleUserIncorrectInput(context).ResetCounter();

            context.Wait(GetTask);
        }

        public async Task GetTask(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var userResponse = await result;

            var luisResult = await LuisHelper.GetIntentAndEntitiesFromLUIS(userResponse.Text);

            switch (luisResult.TopScoringIntent.Intent)
            {
                case LuisIntent.Yes:
                    {
                        await context.PostAsync(MessagesResource.AddOtherProducts);
                        context.Call(_dialogFactory.Create<GetProductsByUserTask>(), Callback);
                        break;
                    }
                case LuisIntent.No:
                    {
                        context.Call(_dialogFactory.Create<AskIfWantSuggestionProductsTask>(), Callback);
                        break;
                    }
                default:
                    {
                        var handler = new HandleUserIncorrectInput(context);

                        if (handler.CheckCounterErrors())
                        {
                            await handler.UserErrorLimitExceeded();

                            context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
                        }
                        else
                        {
                            await context.PostAsync(MessagesResource.CourtesyChooseError);

                            var sender = new SendCorrectYesOrNoCard(context);
                            await sender.Send(MessagesResource.AskIfWantAddProduct);

                            context.Wait(GetTask);
                        }
                        break;
                    }
            }
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
        }
    }
}