using Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ViewModels.Usuario
{
    public class LoginViewModel :MvxNavigationViewModel
    {
        private readonly IAuthService _authService;

        private string _login;
        private string _password;

        public LoginViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService) : base(logProvider, navigationService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

            LoginCommand = new MvxCommand(() => LoginTask = MvxNotifyTask.Create(() => LoginAsync()));
            //ShowRecoverCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<RecoverViewModel>());
        }

        /// <summary>
        /// Login informado pelo usuário.
        /// </summary>
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        /// <summary>
        /// Senha informada pelo usuário.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        /// <summary>
        /// Task para acompanhamento do processo de Login.
        /// </summary>
        public MvxNotifyTask LoginTask { get; set; }

        /// <summary>
        /// Command para realizar o Login.
        /// </summary>
        public IMvxCommand LoginCommand { get; private set; }

        private async Task LoginAsync(CancellationToken ct = default)
        {
            // TODO: Implementar lógica para login.
            await _authService.LoginAsync(ct);

            if (_authService.IsAuthenticated)
            {
                await NavigationService.Navigate<MainViewModel>();
            }
        }
    }
}
