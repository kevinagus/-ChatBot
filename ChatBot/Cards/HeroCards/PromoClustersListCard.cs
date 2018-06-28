using ChatBot.DTOs;
using LuisBot.Interfaces;
using LuisBot.Messages;
using LuisBot.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuisBot.Cards.HeroCards
{
    public class PromoClustersListCard : ICard
    {
        private readonly IList<ClusterDto> _clusters;

        public PromoClustersListCard(IList<ClusterDto> clusters, string message)
        {
            _clusters = clusters;
        }

        public IList<Attachment> Draw()
        {
            var attachment = new List<Attachment>();

            var buttons = new List<CardAction>();

            foreach (var cluster in _clusters)
            {
                var categoryButton = new CardAction()
                {
                    Type = ActionTypes.PostBack,
                    Title = cluster.Description,
                    Value = JsonConvert.SerializeObject(new OptionSelectionPayload() { Action = "add", Option = cluster.Id })
                };

                buttons.Add(categoryButton);
            }

            var plCard = new HeroCard()
            {
                Buttons = buttons
            };

            var plAttachment = plCard.ToAttachment();
            attachment.Add(plAttachment);

            var button = new HeroCard()
            {
                Buttons = new List<CardAction>()
                            {
                                new CardAction()
                                {
                                    Type =  ActionTypes.PostBack,
                                    Title = CardMessagesResource.ListCardFinalButtonTitle,
                                    Value = JsonConvert.SerializeObject(new OptionSelectionPayload()
                                    {
                                        Action="cancel"
                                    })
                                }
                            }
            };

            var buttonAttachment = button.ToAttachment();
            attachment.Add(buttonAttachment);

            return attachment;
        }
    }
}