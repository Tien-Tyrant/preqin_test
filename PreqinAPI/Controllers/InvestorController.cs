using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreqinAPI.Data;
using PreqinAPI.DTOs;
using PreqinAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // Get investors and map to DTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestorDto>>> GetInvestors()
        {
            var investors = await _context.Investors
                .Include(i => i.Commitments)
                .ToListAsync(); // Fetch data from the database first

            // Now perform the sum on the client side
            var investorDtos = investors.Select(i => new InvestorDto
            {
                Id = i.Id,
                Name = i.Name,
                Type = i.Type,
                DateAdded = i.DateAdded.ToString("MMMM d, yyyy"),
                TotalCommitments = i.Commitments.Sum(c => c.Amount)
            }).ToList();

            return Ok(investorDtos);
        }
    }
}
