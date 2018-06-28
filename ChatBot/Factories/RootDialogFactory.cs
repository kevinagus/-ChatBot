using LuisBot.Dialogs;
using LuisBot.Interfaces;
using LuisBot.LuisCustom;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace LuisBot.Factories
{
    public class RootDialogFactory
    {
        private readonly IDialogFactory _dialogFactory;

        public RootDialogFactory(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        public IDialog GetDialog(IDialogContext context, LuisResult luisResult)
        {
            switch (luisResult.TopScoringIntent.Intent)
            {
                case LuisIntent.None:
                    {
                        return _dialogFactory.Create<GreetingDialog>();
                    }
                case LuisIntent.Greetings:
                    {
                        return _dialogFactory.Create<GreetingDialog>();
                    }
                case LuisIntent.CreateNewList:
                    {
                        return _dialogFactory.Create<CreateNewShoppingListDialog>();
                    }
                case LuisIntent.OpenLastList:
                    {
                        return _dialogFactory.Create<OpenLastShoppingListDialog>();
                    }
                case LuisIntent.GetTimetables:
                    {
                        return _dialogFactory.Create<OpeningHoursDialog>();
                    }
                case LuisIntent.GetPromo:
                    {
                        return _dialogFactory.Create<PromoProductsByCategoryDialog>();
                    }
                default:
                    {
                        return _dialogFactory.Create<GreetingDialog>();
                    }
            }
        }
    }
}