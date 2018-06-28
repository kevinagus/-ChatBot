using ChatBot.DTOs;
using LuisBot.Dialogs;
using LuisBot.Interfaces;
using LuisBot.Logic.BotMessages;
using LuisBot.Logic.LoopTaskHandler;
using LuisBot.Messages;
using LuisBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuisBot.DialogTasks
{
    [Serializable]
    public class GetSuggestionProductByCardTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public GetSuggestionProductByCardTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var products = context.UserData.GetValue<IList<ProductDto>>("LastRecommendationProducts");

            await context.PostAsync(MessagesResource.SelectProduct);

            var sender = new SendCorrectProductsListCard(context);
            await sender.Send(products);

            new HandleUserIncorrectInput(context).ResetCounter();

            context.Wait(GetTask);
        }

        public async Task GetTask(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            try
            {
                var message = await result;

                var itemSelected = JsonConvert.DeserializeObject<ProductSelectionPayload>(message.Text, new JsonSerializerSettings { Error = delegate (object sender, ErrorEventArgs args) { args.ErrorContext.Handled = true; } });

                if (itemSelected != null)
                {
                    if (itemSelected.Action == "add")
                    {
                        var sender = new SendConfirmProductAdded(context);
                        await sender.Send();
                    }

                    context.Call(_dialogFactory.Create<GetPromoProductByCardTask>(), Callback);
                }
                else
                {
                    var handler = new HandleUserIncorrectInput(context);

                    if (handler.CheckCounterErrors())
                    {
                        await handler.UserErrorLimitExceeded();

                        context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
                    }
                    else
                    {
                        await context.PostAsync(MessagesResource.CourtesyProductError);

                        var productsList = context.UserData.GetValueOrDefault<List<ProductDto>>("LastRecommendationProducts");

                        var sender = new SendCorrectProductsListCard(context);
                        await sender.Send(productsList);

                        context.Wait(GetTask);
                    }
                }
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