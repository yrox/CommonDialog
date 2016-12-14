using BusinessLogic.Accounts;
using BusinessLogic.Interfaces;
using Ninject.Modules;

namespace BusinessLogic.NinjectBindings
{
    public class AccountBinding : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccount>().To<VkAccount>().Named("Vk");
            Bind<IAccount>().To<TelegramAccount>().Named("Telegram");
        }
    }
}
