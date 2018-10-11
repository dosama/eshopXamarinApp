using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Webservice.Models;

namespace eShop.Webservice.Services
{
    public class UsersService: RestService
    {
        public UsersService()
        {
            ServiceName = "Users";
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var path = $"userName/{userName}";
            return  await GetDataAsync<User>(path);

        }
    }
}
