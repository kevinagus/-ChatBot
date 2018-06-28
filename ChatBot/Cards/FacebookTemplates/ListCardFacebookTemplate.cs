using ChatBot.DTOs;
using LuisBot.Interfaces;
using LuisBot.Messages;
using LuisBot.Models;
using LuisBot.Models.FacebookModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LuisBot.Cards.FacebookTemplates
{
    public class ListCardFacebookTemplate : IFacebookTemplate
    {
        private readonly IList<ProductDto> _products;
        private readonly int maxElements = 4;

        public ListCardFacebookTemplate(IList<ProductDto> products)
        {
            _products = products.Take(maxElements).ToList();
        }

        public FacebookMessage GetTemplate()
        {
            var facebookElements = new List<FacebookElement>();

            foreach (var product in _products)
            {
                var facebookElement = new FacebookElement()
                {
                    Title = product.Description,
                    Image_url = $"C:/Users/f.marcia/source/repos/LuisBot/LuisBot/Images/image.jpeg",
                    Buttons = new FacebookButton[]
                    {
                        new FacebookButton()
                        {
                            Title = CardMessagesResource.ListCardButtonTitle,
                            Type = "postback",
                            Payload = JsonConvert.SerializeObject(new ProductSelectionPayload(){Action="add", Product = product})
                        }
                    }
                };

                facebookElements.Add(facebookElement);
            }

            var facebookMessage = new FacebookMessage()
            {
                Attachment = new FacebookAttachment()
                {
                    Type = "template",
                    Payload = new FacebookPayload()
                    {
                        Template_type = "list",
                        TopElementStyle = "compact",
                        Elements = facebookElements.ToArray(),
                        Buttons = new FacebookButton[]
                        {
                            new FacebookButton()
                            {
                                Title = CardMessagesResource.ListCardFinalButtonTitle,
                                Type = "postback",
                                Payload = JsonConvert.SerializeObject(new ProductSelectionPayload(){Action="cancel"})
                            }
                        }
                    }

                }
            };

            return facebookMessage;
        }
    }
}