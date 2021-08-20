using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContragentsCheck.Infrastructure;
using ContragentsCheck.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContragentsCheck.Controllers
{
    public class RobotController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ContragentsCheckContext db;
        public RobotController(ILogger<HomeController> logger, ContragentsCheckContext context)
        {
            _logger = logger;
            db = context;
        }

        public async Task<IActionResult> AddReportLink(Report report)
        {
            try
            {
                db.Reports.Add(report);
                await db.SaveChangesAsync();
                return Json(report.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
        public async Task<IActionResult> UpdateRequest(int id, int statusId, int reportId)
        {
            try
            {
                var findedReq = db.Requests.Find(id);
                findedReq.StatusId = statusId;
                findedReq.ReportId = reportId;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
    }
}
