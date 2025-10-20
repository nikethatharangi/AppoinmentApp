using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppoinmentApp.Data.Models;

namespace AppoinmentApp.Controllers
{
    [Route("api/appoinment")]
    [ApiController]
    public class AppoinmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppoinmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Appoinment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appoinment>>> GetAppoinments()
        {
            var appoinment = await _context.Appoinments.ToListAsync();

            if (appoinment == null)
            {
                return NotFound("Data not found!");
            }
            return await _context.Appoinments.Where(e => !e.Done || !e.Deleted).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appoinment>>> FilteredAppoinments(Filter filter)
        {
            if(_context.Appoinments == null)
            {
                return NotFound("Data not found");
            }

            List<Appoinment> appoinments = await _context.Appoinments.ToListAsync();

            if (filter.All)
            {
                return appoinments;
            }

            if (filter.LevelOFImportance != null)
            {
                appoinments = appoinments.Where(e => e.LevelOfImportance  == filter.LevelOFImportance).ToList();
            }

            if (filter.SpecifiedDate != null)
            {
                appoinments = appoinments.Where(e => e.Date == filter.SpecifiedDate).ToList();
            }

            if (filter.SpecifiedTime != null)
            {
                appoinments = appoinments.Where(e => e.Time == filter.SpecifiedTime).ToList();
            }

            if (filter.StartDate != null && filter.EndDate != null)
            {
                appoinments = appoinments.Where(e => e.Date >= filter.StartDate && filter.EndDate >= e.Date).ToList();
            }

            appoinments = appoinments.Where(e => e.Done == filter.Done).ToList();
            appoinments = appoinments.Where(e => e.Deleted == filter.Deleted).ToList();

            return appoinments;
        }

        // GET: api/Appoinment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appoinment>> GetAppoinment(int id)
        {
            var appoinment = await _context.Appoinments.FindAsync(id);

            if (appoinment == null)
            {
                return NotFound("Data not found!");
            }

            return appoinment;
        }

        // PUT: api/Appoinment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppoinment(int id, Appoinment appoinment)
        {
            if (id != appoinment.ID)
            {
                return BadRequest("You are trying to modify the wrong appoinment");
            }

            _context.Entry(appoinment).State = EntityState.Modified;

            try
            {
                Appoinment Entry_ = await _context.Appoinments.FirstAsync(e=> e.ID == appoinment.ID);

                if(Entry_.Address != appoinment.Address)
                {
                    Entry_.Address = appoinment.Address;
                }

                if (Entry_.Title != appoinment.Title)
                {
                    Entry_.Title = appoinment.Title;
                }

                if (Entry_.Description != appoinment.Description)
                {
                    Entry_.Description = appoinment.Description;
                }

                if (Entry_.LevelOfImportance != appoinment.LevelOfImportance)
                {
                    Entry_.LevelOfImportance = appoinment.LevelOfImportance;
                }

                if (Entry_.Date != appoinment.Date)
                {
                    Entry_.Date = appoinment.Date;
                }

                if (Entry_.Time != appoinment.Time)
                {
                    Entry_.Time = appoinment.Time;
                }

                if (Entry_.Deleted != appoinment.Deleted)
                {
                    Entry_.Deleted = appoinment.Deleted;
                }

               

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppoinmentExists(id))
                {
                    return NotFound("The appoinment with the ID" + id + "does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Entry updated succesfully");
        }

        // POST: api/Appoinment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appoinment>> PostAppoinment(Appoinment appoinment)
        {
            _context.Appoinments.Add(appoinment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppoinment", new { id = appoinment.ID }, appoinment);
        }

        // DELETE: api/Appoinment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppoinment(int id)
        {
            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment == null)
            {
                return NotFound("Data not found!");
            }

            _context.Appoinments.Remove(appoinment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppoinmentExists(int id)
        {
            return _context.Appoinments.Any(e => e.ID == id);
        }
    }
}
