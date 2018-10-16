using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using eShop.Constants;
using eShop.DBModels;
using eShop.Services;
using eShop.Views;
using eShop.Webservice.Services;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace eShop.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        private readonly UsersService _usersService;
        public LoginViewModel()
        {
            _usersService = new UsersService();
            LoginCommand = new Command(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(UserName);
        }

        private async void ExecuteLogin()
        {
            try
            {

                if (Regex.IsMatch(UserName, @"^[a-zA-Z0-9 ]*$"))
                {
                    var connection = DependencyService.Get<ISQLiteDb>().GetConnection();

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var result = await _usersService.GetUserByUserName(UserName);
                        if (result != null)
                        {

                            await connection.CreateTableAsync<UserModel>();

                            if(await connection.Table<UserModel>().FirstOrDefaultAsync(x=>x.UserName == UserName) == null)
                            await connection.InsertAsync(new UserModel() { UserId = result.UserId, UserName = result.UserName });

                            AppPersistenceService.SaveValue(AppPropertiesKeys.USER_NAME, result.UserName);
                            await View.Navigation.PushAsync(new HomePage());
                        }
                        else
                        {
                            await View.DisplayAlert("Error", "User Not Found", "OK");
                        }
                    }
                    else
                    {
                        var users = await connection.Table<UserModel>().ToListAsync();
                        var result = users.FirstOrDefault(u => u.UserName == UserName);
                        if (result != null)
                        {
                            AppPersistenceService.SaveValue(AppPropertiesKeys.USER_NAME, result.UserName);
                            await View.Navigation.PushAsync(new HomePage());
                        }
                        else
                        {
                            await View.DisplayAlert("Error", "User Not Found", "OK");
                        }
                    }

                }
                else
                {
                    await View.DisplayAlert("Error", "User Name Should not contain Any spaces or Special Characters", "OK");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await View.DisplayAlert("Error", "Error Occured while trying to login", "OK");
            }
        
       
        }


        public MainPage View { get; set; }
        public ICommand LoginCommand { get; private set; }
        private string _userName;
        public string UserName { get => _userName;
            set { _userName = value; OnPropertyChanged(); ((Command)LoginCommand).ChangeCanExecute(); } }
    }
}
