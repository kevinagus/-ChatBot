using LuisBot.Interfaces;
using LuisBot.Messages;
using LuisBot.Models.FacebookModels;

namespace LuisBot.Cards.FacebookTemplates
{
    public class GreetingFacebookTemplate : IQuickReply
    {
        private readonly string _message;
        public GreetingFacebookTemplate(string message)
        {
            _message = message;
        }

        public FacebookMessageQuickReply GetTemplate()
        {
            var facebookMessage = new FacebookMessageQuickReply();

            var timeTableQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = CardMessagesResource.GreetingCardTimeTableTitle,
                Payload = CardMessagesResource.GreetingCardTimeTablePayload
            };

            var viewPromoQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = CardMessagesResource.GreetingCardViewPromoTitle,
                Payload = CardMessagesResource.GreetingCardViewPromoPayload
            };

            var newListQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = CardMessagesResource.GreetingCardNewListTitle,
                Payload = CardMessagesResource.GreetingCardNewListPayload
            };

            var oldListQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = CardMessagesResource.GreetingCardOldListTitle,
                Payload = CardMessagesResource.GreetingCardOldListPayload
            };

            facebookMessage.Quick_replies = new FacebookQuickReply[]
            {
                newListQuickReply,
                oldListQuickReply,
                viewPromoQuickReply,
                timeTableQuickReply,
            };

            facebookMessage.Text = _message;

            return facebookMessage;
        }
    }
}