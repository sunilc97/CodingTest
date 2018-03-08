using System;
using System.Threading.Tasks;
using WhetherPOC.Models;

namespace WhetherPOC.ServiceAccess
{
    public class ExploreSL
    {
        public async Task<Wheather> GetWhetherData(string ZipCode)
        {
            return await WebserviceManager<Wheather>.GetWhetherData(ZipCode);
        }
    }
}
