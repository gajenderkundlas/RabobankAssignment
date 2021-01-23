namespace Rabobank.Training.ClassLibrary
{
    using System.Threading.Tasks;
    public interface IFundsOfMandates
    {
        Task<FundsOfMandatesData> ReadFundOfMandatesFile();
        Task<PortfolioVM> GetPortfolio();
        Task<PositionVM> GetCalculatedMandates(PositionVM posObj, FundOfMandates fundobj);
    }
}
