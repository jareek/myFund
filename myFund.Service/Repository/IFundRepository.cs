using myFund.Common.Model;

namespace myFund.Service.Repository
{
    public interface IFundRepository
    {
        Stock AddStock(Stock stock);

        Fund GetFund();
    }
}
