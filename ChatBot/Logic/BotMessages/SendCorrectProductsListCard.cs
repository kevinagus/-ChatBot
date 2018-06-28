using ChatBot.DTOs;
using LuisBot.Cards.FacebookTemplates;
using LuisBot.Cards.HeroCards;
using LuisBot.Logic.CardSender;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuisBot.Logic.BotMessages
{
    [Serializable]
    public class SendCorrectProductsListCard
    {
        private readonly IDialogContext _context;

        public SendCorrectProductsListCard(IDialogContext context)
        {
            _context = context;
        }

        public async Task Send(IList<ProductDto> products)
        {
            var channel = _context.Activity.ChannelId;

            switch (channel)
            {
                case "facebook":
                    {
                        var facebookSender = new SendFacebookTemplate(_context);
                        var listFacebookTemplate = new ListCardFacebookTemplate(products).GetTemplate();
                        await facebookSender.Send(listFacebookTemplate);
                        break;
                    }
                default:
                    {
                        var defaultSender = new SendCardToConversation(_context);
                        var listHeroCard = new ListCard(products);
                        await defaultSender.SendCard(listHeroCard);
                        break;
                    }
            }
        }
    }
}