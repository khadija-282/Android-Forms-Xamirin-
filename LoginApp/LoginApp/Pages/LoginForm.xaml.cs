using LoginApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginForm : ContentPage
	{
        LoginViewModel vm = new LoginViewModel();
        public LoginForm ()
		{
            
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += async (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
                if (vm.isSuccess)
                {
                    await Navigation.PushModalAsync(new Welcome());
                }
            };
            
        }
        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            vm.SubmitCommand.Execute(null);
            if (vm.isSuccess)
            {
                await Navigation.PushModalAsync(new Welcome());
            }
        }
    }
}