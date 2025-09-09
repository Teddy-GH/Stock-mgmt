using FinShark.Interfaces;
using FinShark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
        }
    }
}
