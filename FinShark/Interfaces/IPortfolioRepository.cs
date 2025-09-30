using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        //Task<Portfolio> GetAllPortfolio();
        //Task<Portfolio> GetPortfolio(int id);

        Task<Portfolio> CreatePortfolio(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);

        //Task<Portfolio> UpdatePortfolio(Portfolio portfolio);
    }
}
