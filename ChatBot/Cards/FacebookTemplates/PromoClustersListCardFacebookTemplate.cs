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
    public class PromoClustersListCardFacebookTemplate : IFacebookTemplate
    {
        private readonly IList<ClusterDto> _clusters;
        private readonly string _message;
        private readonly int maxElements = 4;

        public PromoClustersListCardFacebookTemplate(IList<ClusterDto> clusters, string message)
        {
            _clusters = clusters.Take(maxElements).ToList();
            _message = message;
        }

        public FacebookMessage GetTemplate()
        {
            var facebookElements = new List<FacebookElement>();

            foreach (var cluster in _clusters)
            {
                var facebookElement = new FacebookElement()
                {
                    Title = cluster.Description,
                    Buttons = new FacebookButton[]
                    {
                        new FacebookButton()
                        {
                            Title = cluster.Description,
                            Type = "postback",
                            Payload = JsonConvert.SerializeObject(new OptionSelectionPayload(){ Action="add", Option= cluster.Id })
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
                                Payload = JsonConvert.SerializeObject(new OptionSelectionPayload()
                                {
                                    Action="cancel"
                                })
                            }
                        }
                    },

                }
            };

            return facebookMessage;
        }
    }
}