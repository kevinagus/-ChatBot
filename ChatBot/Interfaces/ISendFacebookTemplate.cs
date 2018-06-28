using System.Threading.Tasks;

namespace LuisBot.Interfaces
{
    public interface ISendFacebookTemplate
    {
        Task Send(IFacebookMessage facebookMessage);
    }
}