using ChatBot.DTOs;
using LuisBot.DialogTasks;
using LuisBot.Interfaces;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class OpenLastShoppingListDialog : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public OpenLastShoppingListDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            try
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

                if (shoppingList == null)
                {
                    shoppingList = new ShoppingListDto()
                    {
                        Id = new Guid(),
                        Items = new List<ShoppingItemDto>()
                    };

                    await context.PostAsync(MessagesResource.EmptyShoppingList);

                    await context.PostAsync(MessagesResource.AskWhatToAdd);

                    context.Call(_dialogFactory.Create<GetProductsByUserTask>(), Callback);
                }
                else
                {
                    if (shoppingList.Items.Count == 0)
                    {
                        await context.PostAsync(MessagesResource.EmptyShoppingList);
                    }
                    else
                    {
                        context.UserData.SetValue("CurrentShoppingListId", shoppingList.Id);

                        await context.PostAsync(MessagesResource.ShowShoppingListReview);

                        var sb = new StringBuilder();

                        foreach (var item in shoppingList.Items)
                        {
                            sb.Append(item.Description + "\n\n");
                        }

                        await context.PostAsync(sb.ToString());
                    }

                    context.Call(_dialogFactory.Create<AskIfAddAnotherProductTask>(), Callback);
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