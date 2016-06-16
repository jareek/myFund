using System.Threading.Tasks;
using myFund.Common.Model;
using myFund.Service.Repository;

namespace myFund.Service.Service
{
    // This class is just for a demo purposes. In a real world application proper server side solution should be delivered.
    public class FundService : IFundService
    {
        private readonly IFundRepository fundRepository;

        public FundService(IFundRepository fundRepository)
        {
            this.fundRepository = fundRepository;
        }

        public Task<Stock> AddStockAsync(Stock stock)
        {
            return Task.FromResult(this.fundRepository.AddStock(stock));
        }

        public Task<Fund> GetFundAsync()
        {
            return Task.FromResult(this.fundRepository.GetFund());
        }
    }
}
