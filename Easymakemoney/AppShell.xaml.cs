
namespace Easymakemoney;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		this.BindingContext = new AppShellViewModel();
        Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
		Routing.RegisterRoute("LoginPage", typeof(LoginPage));
	}
}


