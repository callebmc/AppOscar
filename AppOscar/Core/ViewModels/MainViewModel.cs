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
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            ShowMenuCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuViewModel>());
            //ShowEventsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<>());
            ShowFirstPageCommand = new MvxAsyncCommand(async () => await FirstPage());
        }

        /// <summary>
        /// Command para exibir o Menu.
        /// </summary>
        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        /// <summary>
        /// Command para exibir a lista de Eventos.
        /// </summary>
        public IMvxAsyncCommand ShowEventsCommand { get; private set; }

        /// <summary>
        /// Command para exibir a tela inicial selecionada
        /// </summary>
        public IMvxAsyncCommand ShowFirstPageCommand { get; private set; }

        /// <summary>
        /// Inicializa a raiz do App.
        /// </summary>
        public override async Task Initialize()
        {
            // Caso não esteja logado, encaminhar para o Login.
            //if (!authService.IsAuthenticated)
            //    await NavigationService.Navigate<LoginViewModel>();

            await base.Initialize();
        }

        public async Task FirstPage(CancellationToken ct = default)
        {
            /*switch (_userSettings.LandingPage)
            {
                default:
                    await NavigationService.Navigate<LandingViewModel>();
                    break;
                case Models.Mps.LandingSettings.Internet:
                    await NavigationService.Navigate<TreesViewModel>();
                    break;
                case Models.Mps.LandingSettings.Servidor:
                    await NavigationService.Navigate<HostsViewModel>();
                    break;
            }*/
        }
    }
}
