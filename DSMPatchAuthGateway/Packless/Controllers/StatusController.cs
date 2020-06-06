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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("DevPolicy")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly PacklessDbContext _context;

        public StatusController(PacklessDbContext context)
        {
            _context = context;
        }

        // api/Status
        [HttpGet("{computer?}")]
        public IEnumerable<DateOpenClosedStatDto> GetPatchDataCollection([FromRoute] string computer = "")
        {
            List<DateOpenClosedStatDto> list = new List<DateOpenClosedStatDto>();

            if (computer.Length > 0)
            {
                foreach (var c in _context.PatchDataCollection.
                    Where(n => n.Computer.ToLower() == computer.ToLower()).
                    GroupBy(d => d.CreatedAt.ToString("yyyy-MM-dd")))
                {
                    InsertData(list, c);
                }
            }
            else
            {
                foreach (var c in _context.PatchDataCollection.GroupBy(d => d.CreatedAt.ToString("yyyy-MM-dd")))
                {
                    InsertData(list, c);
                }
            }
            return list;
        }

        private static void InsertData(List<DateOpenClosedStatDto> list, IGrouping<string, PatchData> c)
        {
            int sumFixed = c.Count(x => x.Compliance == "Fixed");
            int sumNotFixed = c.Count(x => x.Compliance == "NotFixed");
            int sumNotApplicable = c.Count(x => x.Compliance == "NotApplicable");

            list.Add(new DateOpenClosedStatDto()
            {
                Date = c.Key.ToString(),
                Fixed = sumFixed,
                NotFixed = sumNotFixed,
                NotApplicable = sumNotApplicable
            });
        }

        [HttpGet("computers")]
        public IEnumerable<String> GetComputers()
        {
            var list = _context.PatchDataCollection.Select(n => n.Computer).Distinct().ToList();
            return list;
        }

        [HttpGet("detailsPercent/{dateString}/{computer?}")]
        public async Task<IActionResult> GetComputerStatusOpenClosed([FromRoute] string dateString, string computer = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patchData = _context.PatchDataCollection.
                Where(d => d.CreatedAt.ToString("yyyy-MM-dd") == dateString).
                ToList();

            var computerList = new List<String>();

            if (computer.Length == 0)
            {
                computerList = GetComputers().ToList();
            }
            else
            {
                computerList.Add(computer);
            }

            if (patchData == null)
            {
                return NotFound();
            }

            List<ComputerOpenClosedStatDto> statList = new List<ComputerOpenClosedStatDto>();
            foreach(var comp in computerList)
            {
                double fix = patchData.Where(c => c.Computer.ToLower() == comp.ToLower()).Where(x => x.Compliance == "Fixed").Count();
                double open = patchData.Where(c => c.Computer.ToLower() == comp.ToLower()).Where(x => x.Compliance != "Fixed").Count();

                if (fix == 0) //Div by 0 Vermeiden
                {
                    open = 100;
                    statList.Add(new ComputerOpenClosedStatDto()
                    {
                        Computer = comp,
                        PercentFixed = 0,
                        PercentNotFixed = 100
                    });
                }
                else
                {
                    double percent = (double)(open / fix) * 100;
                    statList.Add(new ComputerOpenClosedStatDto()
                    {
                        Computer = comp,
                        PercentFixed = 100 - percent,
                        PercentNotFixed = percent
                    });
                }
            }

            return Ok(statList);
        }

        [HttpGet("details/{status}/{computer?}")]
        public async Task<IActionResult> GetPatchDataByStatus([FromRoute] string status, string computer = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getLastestDate = await _context.PatchDataCollection.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

            if (getLastestDate == null)
                return BadRequest(ModelState);

            var patchData = _context.PatchDataCollection.
                Where(p => p.Compliance.ToLower() == status.ToLower()).
                Where(d => d.CreatedAt.ToString("yyyy-MM-dd") == getLastestDate.CreatedAt.ToString("yyyy-MM-dd")).
                ToList();

            if (computer.Length > 0)
            {
                patchData.RemoveAll(n => n.Computer.ToLower() != computer.ToLower());
            }

            if (patchData == null)
            {
                return NotFound();
            }

            return Ok(patchData);
        }

        [HttpGet("mergedDetails/{status}/{computer?}")]
        public async Task<IActionResult> GetPatchDataByStatusMerged([FromRoute] string status, string computer = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getLastestDate = await _context.PatchDataCollection.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

            if (getLastestDate == null)
                return BadRequest(ModelState);

            var patchData = _context.PatchDataCollection.
                Where(p => p.Compliance.ToLower() == status.ToLower()).
                Where(d => d.CreatedAt.ToString("yyyy-MM-dd") == getLastestDate.CreatedAt.ToString("yyyy-MM-dd")).
                ToList();

            List<PatchCountOfComplianceDto> returnData = new List<PatchCountOfComplianceDto>();

            if (computer.Length > 0)
            {
                patchData.RemoveAll(n => n.Computer.ToLower() != computer.ToLower());
            }

            foreach (var p in patchData)
            {
                if (returnData.Count(n => n.Patch == p.Patch) == 0)
                {
                    returnData.Add(new PatchCountOfComplianceDto()
                    {
                        Patch = p.Patch,
                        Compliance = p.Compliance,
                        Count = patchData.Count(x => x.Patch == p.Patch)
                    });
                }
            }

            if (returnData == null)
            {
                return NotFound();
            }

            return Ok(returnData.OrderByDescending(o => o.Count));
        }

        /// <summary>
        /// Prüft auf Redundanz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PatchDataExists(int id)
        {
            return _context.PatchDataCollection.Any(e => e.Id == id);
        }
    }
}