using System;
using System.Collections.Generic;
using System.Text;

namespace ContragentsCheck.Uipath.Models
{
    public class SuccessResponse
    {
        public string result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }
}
