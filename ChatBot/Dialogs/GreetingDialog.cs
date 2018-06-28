using LuisBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using LuisBot.Messages;
using LuisBot.Logic.CardSender;
using LuisBot.Cards.FacebookTemplates;
using LuisBot.Cards.HeroCards;
using Microsoft.Bot.Connector;
using LuisBot.LuisCustom;
using LuisBot.Factories;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public GreetingDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var channel = context.Activity.ChannelId;

            switch (channel)
            {
                case "facebook":
                    {
                        var name = "FirstName SecondName";
                        var message = string.Format(MessagesResource.Greeting, name);

                        var facebooksender = new SendFacebookTemplate(context);
                        var greetingFacebookTemplate = new GreetingFacebookTemplate(message).GetTemplate();
                        await facebooksender.Send(greetingFacebookTemplate);
                        break;
                    }
                default:
                    {
                        var name = context.Activity.From.Name;
                        var message = string.Format(MessagesResource.Greeting, name);

                        var sender = new SendCardToConversation(context);
                        var greatingDefaultTemplate = new GreetingCard(message);
                        await sender.SendCard(greatingDefaultTemplate);
                        break;
                    }
            }

            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            var luidData = await LuisHelper.GetIntentAndEntitiesFromLUIS(message.Text);

            var dialog = _dialogFactory.Create<RootDialogFactory>().GetDialog(context, luidData);

            if ((luidData.TopScoringIntent.Intent != LuisIntent.Greetings) && (dialog.GetType().ToString() == "GreetingDialog"))
            {
                await context.PostAsync(MessagesResource.CourtesyError);
            }

            context.Call(dialog, Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
        }

    }
}