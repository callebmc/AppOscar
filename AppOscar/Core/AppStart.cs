using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService): base(application, navigationService)
        {

        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            /*await authService.RestoreCredentialsAsync();

            if (authService.IsAuthenticated && termsService.HasAgreed)
                await NavigationService.Navigate<MainViewModel>();
            else
               await NavigationService.Navigate<LoginViewModel>();*/
        }
    }
}
