using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreqinAPI.Data;
using PreqinAPI.DTOs;

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

        [HttpGet("{investorId}")]
        public async Task<ActionResult<IEnumerable<CommitmentDto>>> GetCommitments(int investorId, [FromQuery] string assetClass = null)
        {
            var commitmentsQuery = _context.Commitments
                .Include(c => c.AssetClass)
                .Where(c => c.InvestorId == investorId);

            if (!string.IsNullOrEmpty(assetClass))
            {
                commitmentsQuery = commitmentsQuery.Where(c => c.AssetClass.Name == assetClass);
            }

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
