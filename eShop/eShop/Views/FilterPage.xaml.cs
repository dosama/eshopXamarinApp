using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eShop.Constants;
using eShop.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterPage 
	{
		public FilterPage ()
		{
			InitializeComponent ();
		    BindingContext = this;
		  
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        MinPrice = AppPersistenceService.GetValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE) != null ? (double)AppPersistenceService.GetValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE) : 10;
	        MaxPrice = AppPersistenceService.GetValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE) != null ? (double)AppPersistenceService.GetValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE) : 100;

        }
	    protected void setValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
	    {
	        if (EqualityComparer<T>.Default.Equals(backingField, value))
	            return;

	        backingField = value;
	        OnPropertyChanged(propertyName);
	    }
        private double _minPrice;
	    private double _maxPrice;

        public double MinPrice
        {
            get => _minPrice;
            set => setValue(ref _minPrice,value,nameof(MinPrice));
        }
	    public double MaxPrice
	    {
	        get => _maxPrice;
	        set => setValue(ref _maxPrice, value, nameof(MaxPrice));
	    }

        private async void Button_OnClicked(object sender, EventArgs e)
	    {
            AppPersistenceService.SaveValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE,MinPrice);
	        AppPersistenceService.SaveValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE, MaxPrice);
            await Navigation.PopModalAsync();
	    }
	}
}