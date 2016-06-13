using System.Collections.Generic;
using myFund.Common.Model;

namespace myFund.Service.Repository
{
    public interface IStockNameResolver
    {
        string GetStockName(Stock stock, IEnumerable<Stock> fund);
    }
}
