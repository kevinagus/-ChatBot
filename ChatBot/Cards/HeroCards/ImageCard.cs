using LuisBot.Interfaces;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace LuisBot.Cards.HeroCards
{
    public class ImageCard : ICard
    {
        private readonly string _urlImage;
        public ImageCard(string urlImage)
        {
            _urlImage = urlImage;
        }

        public IList<Attachment> Draw()
        {
            var attachment = new List<Attachment>();
            var plCard = new HeroCard()
            {
                Images = new List<CardImage>
                    {
                        new CardImage()
                        {
                            Url = _urlImage
                        }
                    }

            };

            var plAttachment = plCard.ToAttachment();
            attachment.Add(plAttachment);

            return attachment;
        }
    }
}