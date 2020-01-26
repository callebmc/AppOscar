using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.ViewModels;
using System;

namespace Core
{
    public class App : MvxApplication
    {

        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);
            RegisterCustomAppStart<AppStart>();
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
