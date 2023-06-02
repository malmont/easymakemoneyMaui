using System;


namespace Easymakemoney.ViewModels.Startup
{
	public class LoadingPageViewModel
	{
		public LoadingPageViewModel()
		{
			checkUserLoginDetails();

        }
		private async void checkUserLoginDetails()
		{
			string userDetaiskStr = Preferences.Get(nameof(App.UserDetails), "");

			if (string.IsNullOrWhiteSpace(userDetaiskStr))
			{
                //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                await Shell.Current.GoToAsync("//LoginPage");
            }
			else
			{
				var tokenDetails = await SecureStorage.GetAsync(nameof(App.Token));

				var handler = new JwtSecurityTokenHandler();
				var jsonToken = handler.ReadJwtToken(tokenDetails) as JwtSecurityToken;


				var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetaiskStr);
				App.UserDetails = userInfo;
				App.Token = tokenDetails;
				AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync("//DashboardPage");
                //await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
		}
	}
}

