using ChatBot.DTOs;
using LuisBot.Factories;
using LuisBot.Interfaces;
using LuisBot.Logic.BotMessages;
using LuisBot.LuisCustom;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class OpeningHoursDialog : IDialog
    {
        private const string defaultNewLine = "\n";
        private const string facebookNewLine = "\n\n";
        private readonly IDialogFactory _dialogFactory;

        public OpeningHoursDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public async Task StartAsync(IDialogContext context)
        {
            try
            {
                var openingHours = new OpeningHoursDto()
                {
                    RegularOpening = new List<Tuple<string, string>>()
                    {
                        Tuple.Create("lun","10:00 - 11:00"),
                        Tuple.Create("mar","10:00 - 11:00")
                    },
                    SpecialOpenings = new List<Tuple<string, string>>()
                    {
                        Tuple.Create("sab","10:00 - 11:00"),
                        Tuple.Create("dom","10:00 - 11:00")
                    }
                };

                var channel = context.Activity.ChannelId;

                if (openingHours != null)
                {
                    var sb = new StringBuilder();

                    var newLine = defaultNewLine;

                    if (channel == "facebook")
                    {
                        newLine = facebookNewLine;
                    }

                    sb.Append(MessagesResource.ShowOpeningHours + newLine);

                    foreach (var regularOpening in openingHours.RegularOpening)
                    {
                        sb.Append($"{regularOpening.Item1}  {regularOpening.Item2}" + newLine);
                    }

                    if (openingHours.SpecialOpenings != null && openingHours.SpecialOpenings.Any())
                    {
                        sb.Append(MessagesResource.ShowExtraOpeningHours + newLine);

                        foreach (var regularOpening in openingHours.SpecialOpenings)
                        {
                            sb.Append($"{regularOpening.Item1}  {regularOpening.Item2}" + newLine);
                        }
                    }

                    var message = sb.ToString();

                    await context.PostAsync(message);
                }
                else
                {
                    await context.PostAsync(MessagesResource.NoOpeningHours);
                }

                var sender = new SendCorrectGreetingCard(context);
                await sender.Send();

                context.Wait(MessageReceivedAsync);
            }
            catch (Exception ex)
            {
                await context.PostAsync(MessagesResource.CourtesyError);

                context.Wait(MessageReceivedAsync);
            }
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