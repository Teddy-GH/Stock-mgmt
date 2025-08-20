using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]

        public async  Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllStocksAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }


        public async Task<IActionResult> Get(int id)
        {
            {
                var stock = await _stockRepository.GetStockByIdAsync(id);
                if (stock == null)
                {
                    return NotFound();
                }

                return Ok(stock.ToStockDto());
            }

        }

        [HttpPost]
        public async  Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepository.CreateStock(stockModel);
            return CreatedAtAction(nameof(Get), new { id = stockModel.Id },stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            var stockModel =await _stockRepository.UpdateStockAsync(id, updateStockDto);

            if(stockModel == null)
            {
                return NotFound();
            }

          

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepository.DeleteStockAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
