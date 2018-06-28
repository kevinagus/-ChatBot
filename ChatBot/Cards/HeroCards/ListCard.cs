using LuisBot.Interfaces;
using LuisBot.Messages;
using LuisBot.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;
using ChatBot.DTOs;

namespace LuisBot.Cards.HeroCards
{
    public class ListCard : ICard
    {
        private readonly IList<ProductDto> _products;

        public ListCard(IList<ProductDto> products)
        {
            _products = products;
        }

        public IList<Attachment> Draw()
        {
            var attachment = new List<Attachment>();

            foreach (var product in _products)
            {
                var plCard = new HeroCard()
                {
                    Title = product.Description,
                    Images = new List<CardImage>
                    {
                        new CardImage()
                        {
                            Url = $"C:/Users/f.marcia/Desktop/ChatBot/Images/image.jpeg"
                        }
                    },

                    Buttons = new List<CardAction>()
                            {
                                new CardAction()
                                {
                                    Type =  ActionTypes.PostBack,
                                    Title = CardMessagesResource.ListCardButtonTitle,
                                    Value = JsonConvert.SerializeObject(new ProductSelectionPayload(){Action="add", Product = product})
                                }
                            }
                };
                var plAttachment = plCard.ToAttachment();
                attachment.Add(plAttachment);
            }

            var button = new HeroCard()
            {
                Buttons = new List<CardAction>()
                            {
                                new CardAction()
                                {
                                    Type =  ActionTypes.PostBack,
                                    Title = CardMessagesResource.ListCardFinalButtonTitle,
                                    Value = JsonConvert.SerializeObject(new ProductSelectionPayload(){Action="cancel"})

                                }
                            }
            };

            var buttonAttachment = button.ToAttachment();
            attachment.Add(buttonAttachment);

            return attachment;
        }
    }
}