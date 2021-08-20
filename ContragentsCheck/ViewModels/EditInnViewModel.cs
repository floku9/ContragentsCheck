using ContragentsCheck.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContragentsCheck.ViewModels
{
    public class EditInnViewModel
    {
        public Request Request { get; set; }
        public List<Status> Statuses { get; set; } = new List<Status>();
    }
}
