using FinShark.Dtos.Stock;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();

        Task<Stock?> GetStockByIdAsync(int id);

        Task<Stock> CreateStock(Stock stock);

        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteStockAsync(int id);
    }
}
