using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace LuisBot.LuisCustom
{
    public class LuisHelper
    {
        public static async Task<LuisResult> GetIntentAndEntitiesFromLUIS(string query)
        {

            LuisService luisService = new LuisService(
                    new LuisModelAttribute(
                        ConfigurationManager.AppSettings["LuisAppId"],
                        ConfigurationManager.AppSettings["LuisAPIKey"],
                        LuisApiVersion.V2,
                        ConfigurationManager.AppSettings["LuisAPIHostName"],
                        double.Parse(ConfigurationManager.AppSettings["LuisAPIThreshold"], System.Globalization.CultureInfo.InvariantCulture)
                        )
                    );
            LuisResult luisData = await luisService.QueryAsync(query, CancellationToken.None);

            return luisData;
        }
    }
}