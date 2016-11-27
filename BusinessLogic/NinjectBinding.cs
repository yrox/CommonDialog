using System;
using BusinessLogic.Accounts;
using BusinessLogic.Interfaces;
using Ninject.Modules;

namespace BusinessLogic
{
    class NinjectBinding : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccount>().To<VkAccount>().Named("Vk");
        }
    }
}
