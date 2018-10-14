using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using eShop.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ToolBarPage
    {
        public ObservableCollection<string> Items { get; set; }

        public HomePage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };
			
			MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new ProductDetailsPage(e.Item.ToString()));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

     

    }
}
