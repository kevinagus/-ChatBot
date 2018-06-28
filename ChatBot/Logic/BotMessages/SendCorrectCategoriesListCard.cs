using ChatBot.DTOs;
using LuisBot.Cards.FacebookTemplates;
using LuisBot.Cards.HeroCards;
using LuisBot.Logic.CardSender;
using LuisBot.Messages;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuisBot.Logic.BotMessages
{
    [Serializable]
    public class SendCorrectCategoriesListCard
    {
        private readonly IDialogContext _context;

        public SendCorrectCategoriesListCard(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send(IList<ClusterDto> clusters)
        {
            var channel = _context.Activity.ChannelId;

            switch (channel)
            {
                case "facebook":
                    {
                        var facebookSender = new SendFacebookTemplate(_context);
                        var listFacebookTemplate = new PromoClustersListCardFacebookTemplate(clusters, MessagesResource.CategoriesListCardTitle).GetTemplate();
                        await facebookSender.Send(listFacebookTemplate);
                        break;
                    }
                default:
                    {
                        var defaultSender = new SendCardToConversation(_context);
                        var listHeroCard = new PromoClustersListCard(clusters, MessagesResource.CategoriesListCardTitle);
                        await defaultSender.SendCard(listHeroCard);
                        break;
                    }
            }
        }

    }
}