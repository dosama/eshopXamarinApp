using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Webservice.Models;

namespace eShop.Webservice.Services
{
    public class PurshasesService: RestService
    {
        public PurshasesService()
        {
            ServiceName = "Purshase";
        }
        public async Task SubmitPurshase(List<Purshase> items)
        {
            try
            {
                await SaveDataAsync(items, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              
            }
           
        }

    }
}
