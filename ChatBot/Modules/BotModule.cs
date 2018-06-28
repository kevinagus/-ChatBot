using Autofac;
using LuisBot.Dialogs;
using LuisBot.DialogTasks;
using LuisBot.Factories;
using LuisBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Luis;
using System.Configuration;

namespace LuisBot.Modules
{
    public class BotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            int luisThreshold = 0;
            int.TryParse(ConfigurationManager.AppSettings["LuisAPIThreshold"], out luisThreshold);

            builder.RegisterType<DialogFactory>()
                .Keyed<IDialogFactory>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(c => new LuisModelAttribute(ConfigurationManager.AppSettings["LuisAppId"],
                                                        ConfigurationManager.AppSettings["LuisAPIKey"],
                                                        LuisApiVersion.V2,
                                                        ConfigurationManager.AppSettings["LuisAPIHostName"],
                                                        luisThreshold))
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterType<LuisService>()
                .Keyed<ILuisService>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<RootDialogFactory>().InstancePerLifetimeScope();

            //Register Dialogs 
            builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateNewShoppingListDialog>().InstancePerLifetimeScope();
            builder.RegisterType<OpeningHoursDialog>().InstancePerLifetimeScope();
            builder.RegisterType<GreetingDialog>().InstancePerLifetimeScope();
            builder.RegisterType<OpenLastShoppingListDialog>().InstancePerLifetimeScope();
            builder.RegisterType<PromoProductsByCategoryDialog>().InstancePerLifetimeScope();

            //Register Dialog Tasks
            builder.RegisterType<GetProductsByUserTask>().InstancePerLifetimeScope();
            builder.RegisterType<AskIfWantProductPositionTask>().InstancePerLifetimeScope();
            builder.RegisterType<AskIfAddAnotherProductTask>().InstancePerLifetimeScope();
            builder.RegisterType<AskIfWantSuggestionProductsTask>().InstancePerLifetimeScope();
            builder.RegisterType<GetSuggestionProductByCardTask>().InstancePerLifetimeScope();
            builder.RegisterType<GetPromoProductByCardTask>().InstancePerLifetimeScope();
            builder.RegisterType<AskWhatListMustUsedTask>().InstancePerLifetimeScope();
            builder.RegisterType<GetCategoryPromoProductByCardTask>().InstancePerLifetimeScope();
        }
    }
}