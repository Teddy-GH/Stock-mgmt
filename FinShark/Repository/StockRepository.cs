using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Helpers;
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


        public async  Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
           var stock =  _context.Stocks.Include(c => c.Comments).AsQueryable();

         if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if ((!string.IsNullOrWhiteSpace(query.Symbol)))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
                if(query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.CompanyName) : stock.OrderBy(s => s.CompanyName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;




            return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();

                
        }

        public async  Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync( s => s.Id == id);
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

        public Task<bool> stockExists(int id)
        {
           return _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}
