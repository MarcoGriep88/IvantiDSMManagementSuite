using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packless.Database;
using Packless.Dtos;
using Packless.Models;

namespace Packless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("DevPolicy")]
    public class PatchDataController : ControllerBase
    {
        private readonly PacklessDbContext _context;

        public PatchDataController(PacklessDbContext context)
        {
            _context = context;
        }

        // GET: api/PatchData
        [HttpGet]
        public IEnumerable<PatchData> GetPatchDataCollection()
        {
            return _context.PatchDataCollection;
        }

        // GET: api/PatchData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatchData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patchData = await _context.PatchDataCollection.FindAsync(id);

            if (patchData == null)
            {
                return NotFound();
            }

            return Ok(patchData);
        }

        [HttpGet("date/{dateString}")]
        public async Task<IActionResult> GetPatchDataByDate([FromRoute] string dateString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patchData = await _context.PatchDataCollection.Where(d => d.CreatedAt.ToString("yyyyMMdd") == dateString).ToListAsync();

            if (patchData == null)
            {
                return NotFound();
            }

            return Ok(patchData);
        }

        [HttpGet("latest/{count}")]
        public async Task<IActionResult> GetLatestPatchDataCollection([FromRoute] int count)
        {
            List<string> latestDates = new List<string>();
            var dates = _context.PatchDataCollection.
                Select(d => d.CreatedAt.ToString("yyyy-MM-dd")).
                Distinct().
                OrderByDescending(x => x).ToList();

            if (count > dates.Count)
                count = dates.Count;

            latestDates.AddRange(dates.GetRange(0,count));

            var patchesFromDaylongbefore = await _context.PatchDataCollection.Where(d => d.CreatedAt.ToString("yyyy-MM-dd") == latestDates.Last()).ToListAsync();
            var patchesFromDayNow = await _context.PatchDataCollection.Where(d => d.CreatedAt.ToString("yyyy-MM-dd") == latestDates.First()).ToListAsync();

            var patchDelta = new List<PatchData>();

            foreach(var lastPatch in patchesFromDayNow)
            {
                if (patchesFromDaylongbefore.Where(n => n.Patch == lastPatch.Patch).Count() == 0)
                {
                    patchDelta.Add(lastPatch);
                }
            }

            return Ok(patchDelta);
        }

        // PUT: api/PatchData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatchData([FromRoute] int id, [FromBody] PatchData patchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patchData.Id)
            {
                return BadRequest();
            }

            _context.Entry(patchData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatchDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        

        // POST: api/PatchData
        [HttpPost]
        public async Task<IActionResult> PostPatchData([FromBody] PatchData patchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            patchData.CreatedAt = DateTime.Now;

            bool found = (_context.PatchDataCollection.Where(n => n.Patch == patchData.Patch).
                Where(n => n.Computer == patchData.Computer).
                Where(n => n.FoundDate == patchData.FoundDate).
                Where(n => n.Compliance == patchData.Compliance).
                Where(n => n.FixDate == patchData.FixDate).
                Where(n => n.CreatedAt.ToString("yyyyMMdd") == patchData.CreatedAt.ToString("yyyyMMdd")).ToList().Count > 0);

            if (found)
                return Ok();

            

            _context.PatchDataCollection.Add(patchData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatchData", new { id = patchData.Id }, patchData);
        }

        // DELETE: api/PatchData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatchData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patchData = await _context.PatchDataCollection.FindAsync(id);
            if (patchData == null)
            {
                return NotFound();
            }

            _context.PatchDataCollection.Remove(patchData);
            await _context.SaveChangesAsync();

            return Ok(patchData);
        }

        private bool PatchDataExists(int id)
        {
            return _context.PatchDataCollection.Any(e => e.Id == id);
        }
    }
}