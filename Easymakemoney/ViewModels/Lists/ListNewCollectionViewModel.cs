using System;


namespace Easymakemoney.ViewModels.Lists
{
    public partial class ListNewCollectionViewModel: BaseViewModel
    {
        private readonly IListCollectionService _getListCollection;
        private ObservableCollection<ListCollection> _listCollections;

        public ObservableCollection<ListCollection> ListCollections
        {
            get => _listCollections; 
            set { _listCollections = value; }

        }
        //[ObservableProperty]
        //private ObservableCollection<ListCollection> _listCollections;

        public ListNewCollectionViewModel(IListCollectionService GetListCollection)
		{

            _getListCollection = GetListCollection;
            ListCollections = new ObservableCollection<ListCollection>();
           
        }

        public override void loadViewModel()
        {
            GetListCollection();
        }


        public async Task GetListCollection()
        {
            ListCollections = await _getListCollection.GetCollectionList();

        }

        public async void OnDelete(object sender, EventArgs e) //fomction supprime
        {
            await Shell.Current.DisplayAlert("IMPORTANT", "La suppression de cette collection entraine la perte des donnees de commande et de produits lie a cette collection", "Ok");
            var mi = ((MenuItem)sender);

            ListCollection test = mi.CommandParameter as ListCollection;
            test.del = true;
            for (var i = ListCollections.Count - 1; i >= 0; i--)
            {
                if (ListCollections[i].del == true)
                {
                    //await firebaseCollection.DeletePerson(ListesCollection[i].NomDeLaCollection);
                    //await DisplayAlert("Delete Context Action", ListesCollection[i].NomDeLaCollection + " delete context action", "OK");
                }

            }
            //ListesCollection = await firebaseCollection.GetAllPersons();
            //MaListeViewNewCollection.ItemsSource = ListeCollection;
        }

        public void NewComm(object sender, EventArgs e) //creation d'une nouvelle commande lie a la collection en cours
        {
            var mi = ((MenuItem)sender);
            ListCollection test1 = mi.CommandParameter as ListCollection;
            //NameCollectionCommande = test1.nomCollection;
            //NewCommandePage.modif2 = false;
            //this.Navigation.PushAsync(new NewCommandePage(NameCollectionCommande));
            //Navigation.RemovePage(this);
        }

        public void OnMore(object sender, EventArgs e)//fonction pour naviguer vers la page de modification de la creation en cours.
        {
            var mi = ((MenuItem)sender);
            ListCollection test2 = mi.CommandParameter as ListCollection;
            //NameCollectionCommande1 = test2.nomCollection;
            //this.Navigation.PushAsync(new ModifNewCollectionPage(NameCollectionCommande1));
            //Navigation.RemovePage(this);
        }



    }
}

