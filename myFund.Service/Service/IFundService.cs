using System.Threading.Tasks;
using myFund.Common.Model;

namespace myFund.Service.Service
{
    public interface IFundService
    {
        Task<Stock> AddStockAsync(Stock stock);

        Task<Fund> GetFundAsync();
    }
}
