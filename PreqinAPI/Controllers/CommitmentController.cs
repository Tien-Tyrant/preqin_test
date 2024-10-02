using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreqinAPI.Data;
using PreqinAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreqinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitmentController : ControllerBase
    {
        private readonly DataContext _context;

        public CommitmentController(DataContext context)
        {
            _context = context;
        }

        // Get commitments for an investor, with optional filtering by asset class
        [HttpGet("{investorId}")]
        public async Task<ActionResult<IEnumerable<CommitmentDto>>> GetCommitments(int investorId, [FromQuery] string assetClass = null)
        {
            // Base query to get commitments for the given investor
            var commitmentsQuery = _context.Commitments
                .Include(c => c.AssetClass)
                .Where(c => c.InvestorId == investorId);

            // Apply filtering if assetClass is provided
            if (!string.IsNullOrEmpty(assetClass))
            {
                commitmentsQuery = commitmentsQuery.Where(c => c.AssetClass.Name == assetClass);
            }

            // Select the data into a CommitmentDto
            var commitments = await commitmentsQuery
                .Select(c => new CommitmentDto
                {
                    Id = c.Id,
                    InvestorId = c.InvestorId,
                    AssetClass = c.AssetClass.Name,
                    Amount = c.Amount,
                    Currency = c.Currency
                })
                .ToListAsync();

            return Ok(commitments);
        }
    }
}
