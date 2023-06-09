using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Periods.API.Data;
using Periods.API.Models;

namespace Periods.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodsController : Controller
    {
        private readonly PeriodsDbContext periodsDbContext;

        public PeriodsController(PeriodsDbContext periodsDbContext)
        {
            this.periodsDbContext = periodsDbContext;
        }

        //get all periods
        [HttpGet]
        public async Task<IActionResult> GetAllPeriods()
        {
            var periods = await periodsDbContext.Periods.ToListAsync();
            return Ok(periods);
        }

        //get periods by day of week and week number
        [HttpGet("[action]/{dow}/{wn}")]
        [ActionName("GetPeriodsByDOWnWN")]
        public async Task<IActionResult> GetAllPeriods([FromRoute] string dow, string wn)
        {
            var periods = await periodsDbContext.Periods.Where(x => x.day_of_week == dow && x.week_number == wn).ToListAsync();
            return Ok(periods);
        }

        //get single period
        [HttpGet("[action]/{id:int}")]
        //[Route("{id:int}")]
        [ActionName("GetPeriod")]
        public async Task<IActionResult> GetPeriod([FromRoute] int id)
        {
            var period = await periodsDbContext.Periods.FirstOrDefaultAsync(x => x.ID == id);
            if(period != null)
            {
                return Ok(period);
            }

            return NotFound("Period not found");
        }

        //add period
        [HttpPost("[action]")]
        public async Task<IActionResult> AddPeriod([FromBody] Period period)
        {
            await periodsDbContext.Periods.AddAsync(period);
            await periodsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AddPeriod), period);
        }

        //updating a period
        [HttpPut("[action]/{id:int}")]
        //[Route("[action]/{id:int}")]
        public async Task<IActionResult> UpdatePeriod([FromRoute] int id, [FromBody] Period period)
        {
            var existingPeriod = await periodsDbContext.Periods.FirstOrDefaultAsync(x => x.ID == id);
            if (existingPeriod != null)
            {
                existingPeriod.start_time = period.start_time;
                existingPeriod.title = period.title;
                existingPeriod.building = period.building;
                existingPeriod.day_of_week = period.day_of_week;
                existingPeriod.week_number = period.week_number;
                await periodsDbContext.SaveChangesAsync();
                return Ok(existingPeriod);
            }

            return NotFound("Period not found!");
        }

        //delete a period
        [HttpDelete("[action]/{id:int}")]
        //[Route("[action]/{id:int}")]
        public async Task<IActionResult> DeletePeriod([FromRoute] int id)
        {
            var periods = await periodsDbContext.Periods.ToListAsync();
            var existingPeriod = await periodsDbContext.Periods.FirstOrDefaultAsync(x => x.ID == id);
            if (existingPeriod != null)
            {
                periodsDbContext.Remove(existingPeriod);
                await periodsDbContext.SaveChangesAsync();
                return Ok(periods);
            }

            return NotFound("Period not found!");
        }
    }
}
