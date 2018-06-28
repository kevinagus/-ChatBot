using LuisBot.Interfaces;
using LuisBot.Models.FacebookModels;

namespace LuisBot.Cards.FacebookTemplates
{
    public class ImageFacebookTemplate : IFacebookTemplate
    {
        private readonly string _imageUrl;
        private readonly bool _isReusable;

        public ImageFacebookTemplate(string imageUrl, bool isReusable = true)
        {
            _imageUrl = imageUrl;
            _isReusable = isReusable;
        }

        public FacebookMessage GetTemplate()
        {
            var facebookElement = new FacebookElementImage()
            {
                Url = _imageUrl,
                IsReusable = _isReusable
            };

            var facebookMessage = new FacebookMessage()
            {
                Attachment = new FacebookAttachment()
                {
                    Type = "image",
                    Payload = facebookElement
                }
            };

            return facebookMessage;
        }
    }
}