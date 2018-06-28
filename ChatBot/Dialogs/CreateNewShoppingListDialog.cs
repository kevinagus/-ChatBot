using ChatBot.DTOs;
using LuisBot.DialogTasks;
using LuisBot.Interfaces;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class CreateNewShoppingListDialog : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public CreateNewShoppingListDialog(IDialogFactory dialogFactory)
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
                };

                context.UserData.SetValue("CurrentShoppingListId", shoppingList.Id);

                await context.PostAsync(MessagesResource.CreatedList);

                await context.PostAsync(MessagesResource.AskWhatToAdd);

                context.Call(_dialogFactory.Create<GetProductsByUserTask>(), Callback);
            }
            catch (Exception ex)
            {
                await context.PostAsync(MessagesResource.CourtesyError);

                context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
            }
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
        }
    }
}