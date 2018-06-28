using System.Collections.Generic;

namespace LuisBot.Interfaces
{
    public interface IDialogFactory
    {
        T Create<T>();

        T Create<T>(IDictionary<string, object> parameters);
    }
}