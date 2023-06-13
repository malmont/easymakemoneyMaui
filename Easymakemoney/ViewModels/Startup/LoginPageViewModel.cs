
namespace Easymakemoney.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        private readonly ILoginService _loginService;
        public LoginPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        #region Commands
        [ICommand]
        async Task LoginAsync()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                
                var response = await _loginService.Authenticate(new LoginRequest
                {
                    username = Email ,
                    password = Password
                });

                if (response != null){

                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    };
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(response.Token) as JwtSecurityToken;

                    var username = jsonToken.Claims.First(claim => claim.Type == "username").Value;
                    var role = jsonToken.Claims.First(claim => claim.Type == "roles").Value;

                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);

                    UserBasicInfo userDetails = new UserBasicInfo()
                    {
                        Email= username,
                        Role= role

                    };

                    string userDetailStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = userDetails;
                    AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                    await Shell.Current.GoToAsync("//DashboardPage");
                    //await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Invalid User", "Invalid User", "OK");
                }

             
            }




        }
        #endregion

    }
}

