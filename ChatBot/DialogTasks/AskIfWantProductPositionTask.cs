using ChatBot.DTOs;
using LuisBot.Cards.FacebookTemplates;
using LuisBot.Cards.HeroCards;
using LuisBot.Dialogs;
using LuisBot.Interfaces;
using LuisBot.Logic.BotMessages;
using LuisBot.Logic.CardSender;
using LuisBot.Logic.LoopTaskHandler;
using LuisBot.LuisCustom;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace LuisBot.DialogTasks
{
    [Serializable]
    public class AskIfWantProductPositionTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;
        private readonly string _baseEndpointUrl = ConfigurationManager.AppSettings["BaseEndpointUrl"];

        public AskIfWantProductPositionTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            try
            {
                var askIfWantProductPosition = new AskIfWantProductPosition(context);
                await askIfWantProductPosition.Send();

                new HandleUserIncorrectInput(context).ResetCounter();

                context.Wait(GetTask);
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
                var userResponse = await result;

                var channel = context.Activity.ChannelId;

                var shoppingListId = context.UserData.GetValue<Guid>("CurrentShoppingListId");

                var shoppingItems = new List<ShoppingItemDto>()
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
                };

                var luisResult = await LuisHelper.GetIntentAndEntitiesFromLUIS(userResponse.Text);

                var showLabel = false;

                switch (luisResult.TopScoringIntent.Intent)
                {
                    case LuisIntent.Yes:
                    case LuisIntent.No:
                        {
                            if (luisResult.TopScoringIntent.Intent == LuisIntent.Yes)
                            {
                                showLabel = true;

                                switch (channel)
                                {
                                    case "facebook":
                                        var facebookSender = new SendFacebookTemplate(context);
                                        var imageFacebookTemplate = new ImageFacebookTemplate($"image.jpeg", false).GetTemplate();
                                        await facebookSender.Send(imageFacebookTemplate);
                                        break;
                                    default:
                                        var defaultSender = new SendCardToConversation(context);
                                        var imageCard = new ImageCard($"image.jpeg");
                                        await defaultSender.SendCard(imageCard);
                                        break;
                                }
                            }

                            if (shoppingItems.Count == 0)
                            {
                                await context.PostAsync(MessagesResource.EmptyShoppingList);
                            }
                            else
                            {
                                await context.PostAsync(MessagesResource.ShowShoppingListReview);

                                var sb = new StringBuilder();

                                foreach (var item in shoppingItems)
                                {
                                    var label = $"{(!string.IsNullOrEmpty(item.Label) ? "[" + item.Label + "] " : string.Empty)}";

                                    sb.Append($"{(showLabel ? label : string.Empty)}{item.Description}" + "\n\n");
                                }

                                await context.PostAsync(sb.ToString());
                            }

                            context.Call(_dialogFactory.Create<OpeningHoursDialog>(), Callback);

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
                                await sender.Send(MessagesResource.AskIfWantSeePosition);

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