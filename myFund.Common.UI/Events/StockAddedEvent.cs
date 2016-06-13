using myFund.Common.Model;
using Prism.Events;

namespace myFund.Common.UI.Events
{
    public sealed class StockAddedEvent : PubSubEvent<Stock>
    {
    }
}
