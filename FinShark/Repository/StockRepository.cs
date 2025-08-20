using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Interfaces;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Stock> CreateStock(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }


        public async  Task<List<Stock>> GetAllStocksAsync()
        {
            return  await _context.Stocks.ToListAsync();
        }

        public async  Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }


        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);
            if (existingStock == null) 
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;

        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
           var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

           if(stock == null)
            {
                return null;
            }
           _context.Stocks.Remove(stock);
           await  _context.SaveChangesAsync();
            return stock;
        }
    }
}
