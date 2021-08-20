using ContragentsCheck.Infrastructure;
using ContragentsCheck.Infrastructure.Models;
using ContragentsCheck.Models;
using ContragentsCheck.Uipath;
using ContragentsCheck.Uipath.Models;
using ContragentsCheck.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContragentsCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ContragentsCheckContext db;
        readonly IOrchestratorActivitiy<QueueItem> orchestratorActivitiy;

        public HomeController(ILogger<HomeController> logger, ContragentsCheckContext context, IOrchestratorActivitiy<QueueItem> activitiy)
        {
            _logger = logger;
            db = context;
            orchestratorActivitiy = activitiy;
        }
        
        public async Task<IActionResult> Index()
        {
            var requests = await db.Requests.Include(req=>req.Report).Include(req=>req.Status).ToListAsync();
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return PartialView("CreatePartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]IEnumerable<Request> requests)
        {
            foreach (Request req in requests)
            {
                db.Requests.Add(req);
            }
            await db.SaveChangesAsync();
            return PartialView("RequestsDataTable", await db.Requests.Include(req => req.Report).Include(req => req.Status).ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var req = db.Requests.Find(id);
            if (req.StatusId == 1)
            {
                db.Requests.Remove(req);
                await db.SaveChangesAsync();
                return PartialView("RequestsDataTable", await db.Requests.Include(req => req.Report).Include(req => req.Status).ToListAsync());
            }
            else return BadRequest("Задачи переданные роботу не могут быть удалены");
        }

        [HttpPost]
        public async Task<IActionResult> SendToRobot()
        {
            var workRequests = await db.Requests.Where(req => req.StatusId == 1).ToListAsync();
            foreach(Request req in workRequests)
            {
                var resp = orchestratorActivitiy.SendAsync(new QueueItem()
                {
                    QueueName = "CaCheckerQueue",
                    Priority = "Normal",
                    QueueObject = req
                });
                if (!resp.Result.IsSuccessful)
                {
                    ErrorResponse err = JsonConvert.DeserializeObject<ErrorResponse>(resp.Result.Content);
                    return BadRequest($"{err.errorCode} - {err.message}");
                }
                else
                    req.StatusId = 2;
            }
            await db.SaveChangesAsync();
            return PartialView("RequestsDataTable", await db.Requests.Include(req => req.Report).Include(req => req.Status).OrderBy(req => req.StatusId).ToListAsync());
        }

        //[HttpGet]
        //public async Task<IActionResult> EditInn(int id)
        //{
        //    EditInnViewModel model = new EditInnViewModel
        //    {
        //        Request = db.Requests.Find(id),
        //        Statuses = await db.Statuses.ToListAsync()
        //    };
        //    if (model.Request != null)
        //        return View(model);
        //    else
        //        return NotFound();
        //}
        //[HttpPost]
        ////public async Task<IActionResult> EditInn(int id, string inn)
        ////{
        ////    Request r = await db.Requests.FindAsync(id);
        ////    r.Inn = inn;
        ////    await db.SaveChangesAsync();
        ////    return PartialView("RequestsDataTable", db.Requests.Include(req=>req.Status).ToList());
        ////}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
