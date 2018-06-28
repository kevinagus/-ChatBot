using LuisBot.Interfaces;
using LuisBot.Models.FacebookModels;

namespace LuisBot.Cards.FacebookTemplates
{
    public class ListChooseCardFacebookTemplate : IQuickReply
    {
        private readonly string _message;

        public ListChooseCardFacebookTemplate(string message)
        {
            _message = message;
        }

        public FacebookMessageQuickReply GetTemplate()
        {
            var facebookMessage = new FacebookMessageQuickReply();

            var newListQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = "Nuova lista",
                Payload = "Nuova lista"
            };

            var oldListQuickReply = new FacebookQuickReply
            {
                Content_type = "text",
                Title = "Vecchia lista",
                Payload = "Vecchia lista"
            };

            facebookMessage.Quick_replies = new FacebookQuickReply[]
            {
                newListQuickReply,
                oldListQuickReply
            };

            facebookMessage.Text = _message;

            return facebookMessage;
        }
    }
}