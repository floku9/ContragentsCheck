using System;
using System.Collections.Generic;
using System.Text;

namespace ContragentsCheck.Infrastructure.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Request> Requests { get; set; }
    }
}
