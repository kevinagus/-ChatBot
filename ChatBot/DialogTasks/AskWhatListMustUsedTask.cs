using ChatBot.DTOs;
using LuisBot.Dialogs;
using LuisBot.Interfaces;
using LuisBot.Logic.BotMessages;
using LuisBot.Logic.LoopTaskHandler;
using LuisBot.LuisCustom;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuisBot.DialogTasks
{
    [Serializable]
    public class AskWhatListMustUsedTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public AskWhatListMustUsedTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var sender = new SendCorrectListChooseCard(context);
            await sender.Send();

            new HandleUserIncorrectInput(context).ResetCounter();

            context.Wait(GetTask);
        }

        public async Task GetTask(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            try
            {
                var message = await result;

                var luisResult = await LuisHelper.GetIntentAndEntitiesFromLUIS(message.Text);

                var product = context.UserData.GetValue<ProductDto>("SelectedCategoryPromoProduct");

                switch (luisResult.TopScoringIntent.Intent)
                {
                    case LuisIntent.CreateNewList:
                    case LuisIntent.OpenLastList:
                        {
                            if (luisResult.TopScoringIntent.Intent == LuisIntent.CreateNewList)
                            {
                                var shoppingList = new ShoppingListDto()
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

                                context.UserData.SetValue("CurrentShoppingListId", shoppingList.Id);
                            }

                            context.Call(_dialogFactory.Create<AskIfAddAnotherProductTask>(), Callback);

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

                                var sender = new SendCorrectListChooseCard(context);
                                await sender.Send();

                                context.Wait(GetTask);
                            }
                            break;
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