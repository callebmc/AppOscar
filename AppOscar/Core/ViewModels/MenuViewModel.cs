using Core.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class MenuViewModel : MvxNavigationViewModel
    {

        private User _appUser;

        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

            ShowRootCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MainViewModel>());

        }

        /// <summary>
        /// Command para Navegar à Root.
        /// </summary>
        public IMvxCommand ShowRootCommand { get; private set; }

        /// <summary>
        /// Task para carga do Cliente.
        /// </summary>
        public MvxNotifyTask LoadClientTask { get; private set; }

        /// <summary>
        /// Task para o processo de Logout.
        /// </summary>
        public MvxNotifyTask LogoutTask { get; private set; }

        public User AppClient
        {
            get { return _appUser; }
            set { SetProperty(ref _appUser, value); }
        }


        public override Task Initialize()
        {
            LoadClientTask = MvxNotifyTask.Create(() => LoadClientAsync());

            return base.Initialize();
        }

        private async Task LoadClientAsync(CancellationToken ct = default)
        {
            //AppClient = await _authService.GetUserAsync(ct);
        }

        private async Task LogoutFromApp(CancellationToken ct = default)
        {
            /*LogoutTask = MvxNotifyTask.Create(() => _authService.LogoutAsync(ct));

            await NavigationService.Navigate<LoginViewModel>();*/
        }
    }
}
