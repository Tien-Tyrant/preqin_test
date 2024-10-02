using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreqinAPI.Data;
using PreqinAPI.Models;

namespace PreqinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        private readonly DataContext _context;

        public InvestorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investor>>> GetInvestors()
        {
            return await _context.Investors.Include(i => i.Commitments).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Investor>> GetInvestor(int id)
        {
            var investor = await _context.Investors
                .Include(i => i.Commitments)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (investor == null)
            {
                return NotFound();
            }

            return investor;
        }
    }
}
