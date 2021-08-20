using System;
using System.Collections.Generic;
using System.Text;

namespace ContragentsCheck.Infrastructure.Models
{
    public class Report
    {
        public int Id { get; set; }


        public string ReportLink { get; set; }
        public string ReportNote { get; set; }   
        
        public virtual Request Request { get; set; }
    }
}
