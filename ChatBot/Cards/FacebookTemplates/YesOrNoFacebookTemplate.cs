using LuisBot.Interfaces;
using LuisBot.Messages;
using LuisBot.Models.FacebookModels;

namespace LuisBot.Cards.FacebookTemplates
{
    public class YesOrNoFacebookTemplate : IQuickReply
    {
        private readonly string _message;

        public YesOrNoFacebookTemplate(string message)
        {
            _message = message;
        }

        public FacebookMessageQuickReply GetTemplate()
        {
            var facebookMessage = new FacebookMessageQuickReply();

            var yesQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = CardMessagesResource.YesOrNoCardYesTitle,
                Payload = CardMessagesResource.YesOrNoCardYesPayload
            };

            var noQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = CardMessagesResource.YesOrNoCardNoTitle,
                Payload = CardMessagesResource.YesOrNoCardNoPayload
            };

            facebookMessage.Quick_replies = new FacebookQuickReply[]
            {
                yesQuickReply,
                noQuickReply
            };

            facebookMessage.Text = _message;

            return facebookMessage;
        }
    }
}