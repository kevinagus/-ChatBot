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
    public class GetCategoryPromoProductByCardTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public GetCategoryPromoProductByCardTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var products = context.UserData.GetValue<IList<ProductDto>>("CategoryPromoProducts");

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
                    switch (itemSelected.Action)
                    {
                        case "add":
                            {
                                var product = itemSelected.Product;

                                context.UserData.SetValue("SelectedCategoryPromoProduct", product);

                                context.Call(_dialogFactory.Create<AskWhatListMustUsedTask>(), Callback);
                                break;
                            }
                        case "cancel":
                            {
                                var sender = new SendCorrectGreetingCard(context);
                                await sender.Send();

                                context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
                                break;
                            }
                        default:
                            break;
                    }
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
                        await context.PostAsync(MessagesResource.NotUnderstoodRequest);
                        await context.PostAsync(MessagesResource.SelectProduct);

                        var productsList = context.UserData.GetValueOrDefault<List<ProductDto>>("CategoryPromoProducts");

                        var sendProductsListCard = new SendCorrectProductsListCard(context);
                        await sendProductsListCard.Send(productsList);

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