using ChatBot.DTOs;
using ChatBot.DTOs.RecommendationEngine;
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
    public class AskIfWantSuggestionProductsTask : IDialog
    {
        private readonly IDialogFactory _dialogFactory;
        private const int minSuggestionProductNumber = 2;

        public AskIfWantSuggestionProductsTask(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var sender = new SendCorrectYesOrNoCard(context);
            await sender.Send(MessagesResource.AskIfWantSuggestions);

            new HandleUserIncorrectInput(context).ResetCounter();

            context.Wait(GetTask);
        }

        public async Task GetTask(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            try
            {
                var userResponse = await result;

                var luisResult = await LuisHelper.GetIntentAndEntitiesFromLUIS(userResponse.Text);

                var recommendationResult = new RecommendationDto()
                {
                    Matcher = new MatcherResponse()
                    {
                        Results = new List<MatcherItem>()
                        {
                            new MatcherItem()
                            {
                                Id = 3034,
                                Raw = "pasta",
                                Score = 0.6,
                                Type = "Category"
                            },
                             new MatcherItem()
                            {
                                Id = 3034,
                                Raw = "pizza",
                                Score = 0.6,
                                Type = "Category"
                            },
                            new MatcherItem()
                            {
                                Id = 3034,
                                Raw = "vino",
                                Score = 0.6,
                                Type = "Category"
                            }
                        }
                    },
                    Products = new List<ProductDto>()
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
                    }
                };


                context.UserData.SetValue("LastRecommendationProducts", recommendationResult.Products);
                context.UserData.SetValue("LastRecommendationMatcher", recommendationResult.Matcher);

                switch (luisResult.TopScoringIntent.Intent)
                {
                    case LuisIntent.Yes:
                        {
                            if (recommendationResult.Products.Count > minSuggestionProductNumber)
                            {
                                context.Call(_dialogFactory.Create<GetSuggestionProductByCardTask>(), Callback);
                            }
                            else
                            {
                                await context.PostAsync(MessagesResource.NoSuggestionProduct);

                                context.Call(_dialogFactory.Create<GetPromoProductByCardTask>(), Callback);
                            }
                            break;
                        }
                    case LuisIntent.No:
                        {
                            context.Call(_dialogFactory.Create<AskIfWantProductPositionTask>(), Callback);
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
                                await sender.Send(MessagesResource.AskIfWantSuggestions);

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