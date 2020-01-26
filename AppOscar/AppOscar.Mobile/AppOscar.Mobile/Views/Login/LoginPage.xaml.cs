using Core.ViewModels.Usuario;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOscar.Mobile.Views
{
    [MvxContentPagePresentation(NoHistory = true)]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();

            EntryUsuario.ReturnCommand = new Command(() => EntrySenha.Focus());
            EntrySenha.ReturnCommand = new Command(() => ButtonLogin.Focus());  // TODO: Verificar futuramente se não seria melhor disparar o Login de vez...
        }
    }
}