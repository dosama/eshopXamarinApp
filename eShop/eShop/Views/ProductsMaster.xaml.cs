using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsMaster : ContentPage
    {
        public ListView ListView;

        public ProductsMaster()
        {
            InitializeComponent();

            BindingContext = new ProductsMasterViewModel();
            ListView = MenuItemsListView;
        }

        class ProductsMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ProductsMenuItem> MenuItems { get; set; }
            
            public ProductsMasterViewModel()
            {
                MenuItems = new ObservableCollection<ProductsMenuItem>(new[]
                {
                    new ProductsMenuItem { Id = 0, Title = "Page 1" },
                    new ProductsMenuItem { Id = 1, Title = "Page 2" },
                    new ProductsMenuItem { Id = 2, Title = "Page 3" },
                    new ProductsMenuItem { Id = 3, Title = "Page 4" },
                    new ProductsMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}