using FinShark.Dtos.Stock;
using FinShark.Helpers;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);

        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);

        Task<Stock> CreateStock(Stock stock);

        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteStockAsync(int id);
        Task<bool> stockExists(int id);
    }
}
