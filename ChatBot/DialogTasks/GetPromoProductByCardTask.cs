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
using System.Linq;
using System.Threading.Tasks;

namespace LuisBot.DialogTasks
{
    [Serializable]
    public class GetPromoProductByCardTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public GetPromoProductByCardTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            try
            {
                var currentShoppingList = new ShoppingListDto()
                {
                    Id = new Guid(),
                    Items = new List<ShoppingItemDto>()
                    {
                        new ShoppingItemDto()
                        {
                            CategoryId = 3034,
                            Color = Pin.Gray,
                            Description = "pasta",
                            Id = context.UserData.Get<Guid>("CurrentShoppingListId")
                        },
                         new ShoppingItemDto()
                         {
                            CategoryId = 3034,
                            Color = Pin.Gray,
                            Description = "vino",
                            Id = context.UserData.Get<Guid>("CurrentShoppingListId")
                        },
                        new ShoppingItemDto()
                        {
                            CategoryId = 3034,
                            Color = Pin.Gray,
                            Description = "pizza",
                            Id = context.UserData.Get<Guid>("CurrentShoppingListId")
                        }
                    }
                };

                var categories = currentShoppingList.Items.Select(x => x.CategoryId).ToList();

                var products = new List<ProductDto>()
                {
                    new ProductDto()
                    {
                            CategoryId = 3034,
                            Description = "prodotto 1",
                            Id = 1,
                            SKU = "1"
                    },
                        new ProductDto()
                    {
                            CategoryId = 3034,
                            Description = "prodotto 2",
                            Id = 2,
                            SKU = "2"
                    },
                    new ProductDto()
                    {
                            CategoryId = 3034,
                            Description = "prodotto 3",
                            Id = 3,
                            SKU = "3"
                    },
                    new ProductDto()
                    {
                            CategoryId = 3034,
                            Description = "prodotto 4",
                            Id = 4,
                            SKU = "4"
                    }
                };

                if (products == null || products.Count == 0)
                {
                    context.Call(_dialogFactory.Create<AskIfWantProductPositionTask>(), Callback);
                }
                else
                {

                    context.UserData.SetValue("LastPromoProducts", products);

                    await context.PostAsync(MessagesResource.PromoProducts);

                    var sender = new SendCorrectProductsListCard(context);
                    await sender.Send(products);

                    new HandleUserIncorrectInput(context).ResetCounter();

                    context.Wait(GetTask);
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync(MessagesResource.CourtesyError);

                context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
            }
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

                    context.Call(_dialogFactory.Create<AskIfWantProductPositionTask>(), Callback);
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
                        await context.PostAsync(MessagesResource.SelectPromoProduct);

                        var lastPromoProduct = context.UserData.GetValueOrDefault<List<ProductDto>>("LastPromoProducts");

                        var sender = new SendCorrectProductsListCard(context);
                        await sender.Send(lastPromoProduct);

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