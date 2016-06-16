namespace myFund.Common.Model
{
    public interface ICalculatable<in TContext> where TContext : class
    {
        void Calculate(TContext context);
    }
}
