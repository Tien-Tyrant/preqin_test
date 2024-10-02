using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreqinAPI.Data;
using PreqinAPI.Models;

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
        public async Task<ActionResult<IEnumerable<Commitment>>> GetCommitments(int investorId, [FromQuery] string assetClass = "")
        {
            var commitments = _context.Commitments
                .Include(c => c.AssetClass)
                .Where(c => c.InvestorId == investorId);

            if (!string.IsNullOrEmpty(assetClass))
            {
                commitments = commitments.Where(c => c.AssetClass.Name == assetClass);
            }

            return await commitments.ToListAsync();
        }
    }
}
