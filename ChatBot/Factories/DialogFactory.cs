using Autofac;
using LuisBot.Interfaces;
using Microsoft.Bot.Builder.Internals.Fibers;
using System.Collections.Generic;
using System.Linq;

namespace LuisBot.Factories
{
    public class DialogFactory : IDialogFactory
    {
        protected readonly IComponentContext Scope;

        public DialogFactory(IComponentContext scope)
        {
            SetField.NotNull(out this.Scope, nameof(scope), scope);
        }

        public T Create<T>()
        {
            return this.Scope.Resolve<T>();
        }

        public T Create<T>(IDictionary<string, object> parameters)
        {
            return this.Scope.Resolve<T>(parameters.Select(kv => new NamedParameter(kv.Key, kv.Value)));
        }
    }
}