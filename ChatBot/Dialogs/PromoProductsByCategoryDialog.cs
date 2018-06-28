using ChatBot.DTOs;
using LuisBot.DialogTasks;
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

namespace LuisBot.Dialogs
{
    [Serializable]
    public class PromoProductsByCategoryDialog : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        public PromoProductsByCategoryDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var clusters = new List<ClusterDto>()
            {
                new ClusterDto()
                {
                    Description = "prodotto 1",
                    Id = 1
                },
                new ClusterDto()
                {
                    Description = "prodotto 2",
                    Id = 2
                },
                new ClusterDto()
                {
                    Description = "prodotto 3",
                    Id = 3
                },
                new ClusterDto()
                {
                    Description = "prodotto 4",
                    Id = 4
                }
            };

            var sender = new SendCorrectCategoriesListCard(context);
            await sender.Send(clusters);

            new HandleUserIncorrectInput(context).ResetCounter();

            context.Wait(GetTask);
        }

        public async Task GetTask(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            try
            {
                var userResponse = await result;

                var categorySelected = JsonConvert.DeserializeObject<OptionSelectionPayload>(userResponse.Text, new JsonSerializerSettings { Error = delegate (object sender, ErrorEventArgs args) { args.ErrorContext.Handled = true; } });

                if (categorySelected != null)
                {
                    switch (categorySelected.Action)
                    {
                        case "add":
                            {
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

                                context.UserData.SetValue("CategoryPromoProducts", products);

                                if (products.Count == 0)
                                {
                                    await context.PostAsync(MessagesResource.NoPromoProductForCategorySelected);
                                    await context.PostAsync(MessagesResource.SelectAnotherCategory);

                                    var clusters = new List<ClusterDto>()
                                    {
                                        new ClusterDto()
                                        {
                                            Description = "prodotto 1",
                                            Id = 1
                                        },
                                        new ClusterDto()
                                        {
                                            Description = "prodotto 2",
                                            Id = 2
                                        },
                                        new ClusterDto()
                                        {
                                            Description = "prodotto 3",
                                            Id = 3
                                        },
                                        new ClusterDto()
                                        {
                                            Description = "prodotto 4",
                                            Id = 4
                                        }
                                    };

                                    var sender = new SendCorrectCategoriesListCard(context);
                                    await sender.Send(clusters);

                                    context.Wait(GetTask);
                                }
                                else
                                {
                                    context.Call(_dialogFactory.Create<GetCategoryPromoProductByCardTask>(), Callback);
                                }
                                break;
                            }
                        case "cancel":
                            {
                                var sender = new SendCorrectGreetingCard(context);
                                await sender.Send();

                                context.Call(_dialogFactory.Create<GreetingDialog>(), Callback);
                            }
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
                        await context.PostAsync(MessagesResource.CourtesyCategoryError);

                        var clusters = new List<ClusterDto>()
                        {
                            new ClusterDto()
                            {
                                Description = "prodotto 1",
                                Id = 1
                            },
                            new ClusterDto()
                            {
                                Description = "prodotto 2",
                                Id = 2
                            },
                            new ClusterDto()
                            {
                                Description = "prodotto 3",
                                Id = 3
                            },
                            new ClusterDto()
                            {
                                Description = "prodotto 4",
                                Id = 4
                            }
                        };

                        var sender = new SendCorrectCategoriesListCard(context);
                        await sender.Send(clusters);

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