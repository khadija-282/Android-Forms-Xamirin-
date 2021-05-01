using LoginApp.Service;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;


namespace LoginApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public bool isSuccess {get;set;}
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmitAsync);
        }
        public async void OnSubmitAsync()
        {
            var _client = new HttpClient();
            _client.BaseAddress = new Uri("http://192.168.1.8/api/Login/AuthenticateUser");
            string resource = "?Email=" + Email + "&Password=" + Password;
            var auth = new AuthenticationService(_client);
            isSuccess = await auth.MakeGetRequest(resource);
            if (!isSuccess)
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
