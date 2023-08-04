

namespace Easymakemoney.Views.Lists;

public partial class ListNewCollectionPage : ContentPage
{
	public ListNewCollectionPage(ListNewCollectionViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();
	}
}
